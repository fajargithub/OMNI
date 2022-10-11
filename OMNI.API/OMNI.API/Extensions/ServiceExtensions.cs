using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio.AspNetCore;
using OMNI.API.Configurations;
using OMNI.API.Services.LDAP;
using OMNI.API.Services.LDAP.Interfaces;
using OMNI.Data.Data;
using OMNI.Utilities.Constants;
using OMNI.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.API.Extensions
{
    public static class ConnectionExtensions
    {
        public static void ConfigureDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.Get<AppSettings>();
            GeneralConstants.IsProduction = appSettings.IsProduction;
            var connection = appSettings.IsProduction ? appSettings.ConnectionStrings["ProdConnectionMode"] : appSettings.ConnectionStrings["DevConnectionMode"];

            services.AddDbContext<ApplicationDbContext>(options
                => options.UseSqlServer(connection + appSettings.DataBase[DatabaseEnums.AppUserDb.ToString()]));

            services.AddDbContext<OMNIDbContext>(options
                => options.UseSqlServer(connection + appSettings.DataBase[DatabaseEnums.OMNIDb.ToString()]));

            services.AddDbContext<CorePTKContext>(options
                => options.UseSqlServer(connection + appSettings.DataBase[DatabaseEnums.CorePTKDb.ToString()]));
        }

        //public static void ConfigureDataLayer(this IServiceCollection services)
        //{
        //    services.AddScoped<CustomSignInService>();
        //}

        //public static void ConfigureDomainLayer(this IServiceCollection services)
        //{
        //    services.AddScoped<ICustomSignInService, CustomSignInService>();
        //}

            public static void ConfigureMinio(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.Get<AppSettings>();
            string a = configuration.GetSection("MinioService").GetSection("AccessKey").Value;
            string b = appSettings.IsProduction ? configuration.GetSection("MinioService").GetSection("URL").GetSection("Prod").Value : configuration.GetSection("MinioService").GetSection("URL").GetSection("Dev").Value;
            services.AddMinio(opt =>
            {
                opt.Endpoint = appSettings.IsProduction ? configuration.GetSection("MinioService").GetSection("URL").GetSection("Prod").Value : configuration.GetSection("MinioService").GetSection("URL").GetSection("Dev").Value;
                opt.AccessKey = configuration.GetSection("MinioService").GetSection("AccessKey").Value;
                opt.SecretKey = configuration.GetSection("MinioService").GetSection("SecretKey").Value;
            });
        }
    }
}
