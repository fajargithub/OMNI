using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OMNI.Utilities.Constants;
using OMNI.Web.Configurations;
using Simontana.Data.Data;
using System;

namespace OMNI.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.Get<AppSettings>();
            GeneralConstans.IsProduction = appSettings.IsProduction;
            var connection = appSettings.IsProduction ? appSettings.ConnectionStrings["ProdConnectionMode"] : appSettings.ConnectionStrings["DevConnectionMode"];

            services.AddDbContext<ApplicationDbContext>(options
                => options.UseSqlServer(connection + appSettings.DataBase[DatabaseEnums.AppUserDb.ToString()]));
        }

        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.SignIn.RequireConfirmedEmail = false;
                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
            //options =>
            //{

            //    options.LoginPath = "/Authentication/Login";
            //    options.LogoutPath = "/Authentication/Logout";
            //    options.AccessDeniedPath = "/Authentication/AccessDenied";
            //    options.SlidingExpiration = true;
            //    //options.ExpireTimeSpan = TimeSpan.FromSeconds(3);

            //});

            services.AddAuthentication();
        }

        public static void ConfigureDataLayer(this IServiceCollection services)
        {
            //services.AddScoped<AccountService>();
            //services.AddScoped<PicService>();
        }

        public static void ConfigureDomainLayer(this IServiceCollection services)
        {
            //services.AddScoped<OrderBL>();
        }

        public static void ConfigureSession(this IServiceCollection services)
        {
            //for auth etc if this cookie delete/unavailabe use will log out
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromSeconds(31556952); // 1 YEAR
                options.Cookie.Name = WebConstants.COOKIES_NAME;

                // If the LoginPath isn't set, ASP.NET Core defaults 
                // the path to /Account/Login.
                options.LoginPath = "/Authentication/Login";

                // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                // the path to /Account/AccessDenied.
                options.AccessDeniedPath = "/Authentication/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {

                options.Cookie.Name = WebConstants.SESSION_NAME;
                options.IdleTimeout = TimeSpan.FromSeconds(31556952);// 1 YEAR
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }
    }
}
