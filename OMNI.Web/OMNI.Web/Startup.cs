using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OMNI.Web.Extensions;
using OMNI.Web.Models;
using OMNI.Web.Services.CorePTK;
using OMNI.Web.Services.CorePTK.Interface;
using OMNI.Web.Services.Master;
using OMNI.Web.Services.Master.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OMNI.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //This method gets called by the runtime.Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.ConfigureDatabaseConnection(Configuration);

            services.ConfigureIdentity(Configuration);

            services.ConfigureSession();

            services.ConfigureDataLayer();

            services.ConfigureDomainLayer();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            services.ConfigureHTTPClientFactory(Configuration);

            services.AddResponseCaching();


            services.AddControllersWithViews(opt =>
            {
                AuthorizationPolicy policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
                opt.Filters.Add<ViewBagFilter>();
            }).AddRazorRuntimeCompilation();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("osmosys.user.read", policy => policy.RequireClaim("scope", "osmosys.user.read"));
                options.AddPolicy("osmosys.user.delete", policy => policy.RequireClaim("scope", "osmosys.user.delete"));
            });

            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = "oidc";
            }).AddCookie(opt =>
            {
                opt.Cookie.Name = "TesSSO";
                opt.ExpireTimeSpan = TimeSpan.FromDays(1);
            })
            .AddOpenIdConnect("oidc", opt =>
            {
                opt.Authority = "https://login.qa.idaman.pertamina.com";
                opt.RequireHttpsMetadata = false;

                opt.ClientId = "3c46bba8-7dbd-4649-be13-bc823db6b423";
                opt.ClientSecret = "6d4b83b5-0d08-41a9-9f5b-152268fb3b12";
                opt.ResponseType = "code id_token";

                opt.SaveTokens = true;
                opt.GetClaimsFromUserInfoEndpoint = true;

                opt.Scope.Add("api.auth");
                opt.Scope.Add("email");
                opt.Scope.Add("user.role");
                opt.Scope.Add("user.read");
                opt.Scope.Add("user.readAll");
                opt.Scope.Add("user.whiteList.readAll");
                opt.Scope.Add("position.whiteList.readAll");
                opt.Scope.Add("unit.whiteList.readAll");
                opt.Scope.Add("position.read");
                opt.Scope.Add("position.readAll");
                opt.Scope.Add("unit.readAll");
                opt.Scope.Add("unit.read");
                opt.Scope.Add("offline_access");
                opt.Scope.Add("osmosys.user.read");
                opt.ClaimActions.MapJsonKey("website", "website");

                opt.Events = new OpenIdConnectEvents
                {
                    OnTokenResponseReceived = async e =>
                    {
                        EmpModel emp = new EmpModel();

                        var handler = new JwtSecurityTokenHandler();
                        var jsonToken = handler.ReadJwtToken(e.TokenEndpointResponse.AccessToken);
                        e.Principal.Identities.First().AddClaims(jsonToken.Claims.Where(b => b.Type.Equals(Models.Oid.Scope)).Select(c => new Claim("scope", c.Value)));

                        var url = $"https://rest.qa.idaman.pertamina.com/v1/users/{jsonToken.Claims.FirstOrDefault(c => c.Type.Equals(Models.Oid.Email)).Value}";
                        var client = new HttpClient();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", e.TokenEndpointResponse.AccessToken);
                        HttpResponseMessage response = await client.GetAsync(url);
                        if (response.IsSuccessStatusCode)
                        {
                            client.Dispose();
                            emp = await response.Content.ReadAsAsync<EmpModel>();
                            e.Principal.Identities.First().AddClaim(new Claim("position_id", emp.position.id));
                            e.Principal.Identities.First().AddClaim(new Claim("organization_id", emp.position.organization.id));
                            e.Principal.Identities.First().AddClaim(new Claim("company_name", emp.companyName));
                            e.Principal.Identities.First().AddClaim(new Claim("position_name", emp.position.name));
                            e.Principal.Identities.First().AddClaim(new Claim("organization_name", emp.position.organization.name));
                            e.Principal.Identities.First().AddClaim(new Claim("is_head_pos", emp.position.isHead.ToString()));
                            e.Principal.Identities.First().AddClaim(new Claim("is_chief_pos", emp.position.isChief.ToString()));
                        }

                        client.Dispose();

                        var urlRole = $"https://rest.qa.idaman.pertamina.com/v1/roles/{emp.id}";
                        var clientRole = new HttpClient();
                        clientRole.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", e.TokenEndpointResponse.AccessToken);
                        HttpResponseMessage responseRole = await clientRole.GetAsync(urlRole);

                        if (responseRole.IsSuccessStatusCode)
                        {
                            clientRole.Dispose();
                            List<RoleModel> role = new List<RoleModel>();
                            try
                            {
                                role = await responseRole.Content.ReadAsAsync<List<RoleModel>>();
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                            var roleProve = role.First().roles.Where(a => a.application.name == "OSMOSYS").FirstOrDefault();

                            e.Principal.Identities.First().AddClaim(new Claim("role", roleProve.name));
                        }
                        clientRole.Dispose();

                        var ishead = false;
                        var isOwner = false;
                        var posId = emp.position.id;

                        while (ishead == false && isOwner == false)
                        {
                            var urlPos = $"https://rest.qa.idaman.pertamina.com/v1/positions/parent/{posId}";
                            var clientPos = new HttpClient();
                            clientPos.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", e.TokenEndpointResponse.AccessToken);
                            HttpResponseMessage responsePos = await clientPos.GetAsync(urlPos);

                            if (responsePos.IsSuccessStatusCode)
                            {
                                clientPos.Dispose();
                                Position pos = await responsePos.Content.ReadAsAsync<Position>();

                                ishead = pos.isHead;
                                isOwner = pos.isOwner;
                                posId = pos.id;

                                if (ishead == true && isOwner == true)
                                {
                                    e.Principal.Identities.First().AddClaim(new Claim("division_id", pos.id));
                                    e.Principal.Identities.First().AddClaim(new Claim("division_org_id", pos.organization.id));
                                }
                            }
                            clientPos.Dispose();
                        }

                        return;
                    },
                    OnRemoteFailure = ctx =>
                    {
                        ctx.HandleResponse();
                        ctx.Response.Redirect("/");
                        return Task.FromResult(0);
                    }
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.Use(async (context, next) =>
            {
                context.Request.Headers.Add("authorization", $"Bearer {context.Request.Cookies["JWT"]}");
                await next();
            });
            app.UseStatusCodePages(async context =>
            {
                var request = context.HttpContext.Request;
                var response = context.HttpContext.Response;

                if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
                // you may also check requests path to do this only for specific methods       
                // && request.Path.Value.StartsWith("/specificPath")

                {
                    response.Redirect("/authentication/login");
                }
            });

            app.UseStaticFiles();
            //app.UseHeaderPropagation();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            //app.UseHeaderPropagation();

            //app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=MainPage}/{action=Index}");
            });
        }
    }
}
