using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
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

            services.AddHttpClient("Simontana", o =>
            {
                o.BaseAddress = new Uri(configuration.GetSection("BaseURL").GetSection("Simontana").Value);
                //o.DefaultRequestHeaders.Add("X-API-KEY", configuration.GetSection("APIKey").GetSection("Simontana").Value);
            }).AddHeaderPropagation(options =>
            {
                options.Headers.Add("JWT");
            });

            services.AddHttpClient("AUTH", o =>
            {
                o.BaseAddress = new Uri(configuration.GetSection("BaseURL").GetSection("Authentication").Value);
            });
        }
    }
}
