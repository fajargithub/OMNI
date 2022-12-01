using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OMNI.Web.Extensions;
using System.Net;

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

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            services.ConfigureHTTPClientFactory(Configuration);

            services.AddResponseCaching();

            ConnectionConfiguration.GetService(
                services: services,
                configuration: Configuration,
                IsProduction: Configuration.GetValue<bool>("IsProduction")
                );

            services.AddControllersWithViews(opt =>
            {
                AuthorizationPolicy policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
                opt.Filters.Add<ViewBagFilter>();
            }).AddRazorRuntimeCompilation();

            //services.AddScoped<IPeralatanOSR, PeralatanOSRService>();
            //services.AddScoped<ISpesifikasiJenis, SpesifikasiJenisService>();
            //services.AddScoped<IDetailSpesifikasi, DetailSpesifikasiService>();
            //services.AddScoped<ILatihan, LatihanService>();
            //services.AddScoped<IDetailLatihan, DetailLatihanService>();
            //services.AddScoped<IPersonil, PersonilService>();
            //services.AddScoped<IDetailPersonil, DetailPersonilService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
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
            app.UseHeaderPropagation();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            //app.UseHeaderPropagation();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
