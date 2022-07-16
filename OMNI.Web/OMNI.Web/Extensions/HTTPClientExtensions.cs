using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using OMNI.Web.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMNI.Web.Extensions
{
    public static class HTTPClientExtensions
    {
        public static void ConfigureHTTPClientFactory(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHeaderPropagation(options =>
            {
                options.Headers.Add("JWT",
                    context =>
                    context.HttpContext.User.HasClaim(b => b.Type == "JWT") ? new StringValues(context.HttpContext.User.Claims.FirstOrDefault(b => b.Type == "JWT").Value) : new StringValues()
                    );
            });

            services.AddHttpClient("OMNI", o =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var jwt = serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext?.Request?.Headers["Authorization"];

                var appSettings = configuration.Get<AppSettings>();
                o.BaseAddress =
                    appSettings.IsLocalDevelopment ? new Uri(appSettings.BaseURL["Local"]) :
                    appSettings.IsProduction ? new Uri(appSettings.BaseURL["OMNIProd"]) : new Uri(appSettings.BaseURL["OMNISIT"]);

                o.DefaultRequestHeaders.Add("Authorization", $"{jwt.Value}");
            });

            services.AddHttpClient("AUTH", o =>
            {
                o.BaseAddress = new Uri(configuration.GetSection("BaseURL").GetSection("Authentication").Value);
            });
        }
    }
}
