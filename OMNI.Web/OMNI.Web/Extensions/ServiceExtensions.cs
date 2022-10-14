using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OMNI.Utilities.Constants;
using OMNI.Web.Configurations;
using OMNI.Web.Services.CorePTK;
using OMNI.Web.Services.CorePTK.Interface;
using OMNI.Web.Services.Master;
using OMNI.Web.Services.Master.Interface;
using OMNI.Web.Services.Trx;
using OMNI.Web.Services.Trx.Interface;
using System;

namespace OMNI.Web.Extensions
{
    public static class ServiceExtensions
    {
        //public static void ConfigureDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var appSettings = configuration.Get<AppSettings>();
        //    GeneralConstants.IsProduction = appSettings.IsProduction;
        //    var connection = appSettings.IsProduction ? appSettings.ConnectionStrings["ProdConnectionMode"] : appSettings.ConnectionStrings["DevConnectionMode"];
        //}



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

            //services.AddIdentity<IdentityUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

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
            services.AddScoped<PortService>();

            services.AddScoped<HistoryLatihanTrxService>();
            services.AddScoped<HistoryLLPTrxService>();
            services.AddScoped<HistoryPersonilTrxService>();
            services.AddScoped<JenisService>();
            services.AddScoped<KondisiService>();
            services.AddScoped<LatihanService>();
            services.AddScoped<LatihanTrxService>();
            services.AddScoped<LLPTrxService>();
            services.AddScoped<PeralatanOSRService>();
            services.AddScoped<PersonilService>();
            services.AddScoped<PersonilTrxService>();
            services.AddScoped<RekomendasiJenisService>();
            services.AddScoped<RekomendasiLatihanService>();
            services.AddScoped<RekomendasiPersonilService>();
            services.AddScoped<RekomendasiTypeService>();
            services.AddScoped<SpesifikasiJenisService>();
            services.AddScoped<LampiranService>();
            services.AddScoped<LLPHistoryStatusService>();
            services.AddScoped<LoginService>();
            services.AddScoped<GuestLocationService>();

        }

        public static void ConfigureDomainLayer(this IServiceCollection services)
        {
            services.AddScoped<IPort, PortService>();

            services.AddScoped<IHistoryLatihanTrx, HistoryLatihanTrxService>();
            services.AddScoped<IHistoryLLPTrx, HistoryLLPTrxService>();
            services.AddScoped<IHistoryPersonilTrx, HistoryPersonilTrxService>();
            services.AddScoped<ILatihanTrx, LatihanTrxService>();
            services.AddScoped<ILLPTrx, LLPTrxService>();
            services.AddScoped<IPersonilTrx, PersonilTrxService>();

            services.AddScoped<IJenis, JenisService>();
            services.AddScoped<IKondisi, KondisiService>();
            services.AddScoped<IRekomendasiJenis, RekomendasiJenisService>();
            services.AddScoped<IRekomendasiLatihan, RekomendasiLatihanService>();
            services.AddScoped<IRekomendasiPersonil, RekomendasiPersonilService>();
            services.AddScoped<IRekomendasiType, RekomendasiTypeService>();
            services.AddScoped<IKondisi, KondisiService>();
            services.AddScoped<IKondisi, KondisiService>();
            services.AddScoped<IPeralatanOSR, PeralatanOSRService>();
            services.AddScoped<ISpesifikasiJenis, SpesifikasiJenisService>();
            services.AddScoped<ILatihan, LatihanService>();
            services.AddScoped<IPersonil, PersonilService>();
            services.AddScoped<ILampiran, LampiranService>();
            services.AddScoped<IGuestLocation, GuestLocationService>();
            services.AddScoped<ILLPHistoryStatus, LLPHistoryStatusService>();
            services.AddScoped<ILogin, LoginService>();
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
