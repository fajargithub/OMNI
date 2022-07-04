using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OMNI.Data.Configurations;
using OMNI.Data.Data;
using OMNI.Utilities.Constants;
using OMNI.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMNI.Data.Extensions
{
    public static class ConnectionExtensions
    {
        public static void ConfigureDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.Get<AppSettings>();
            GeneralConstants.IsProduction = appSettings.IsProduction;
            var connection = appSettings.IsProduction ? appSettings.ConnectionStrings["ProdConnectionMode"] : appSettings.ConnectionStrings["DevConnectionMode"];

            services.AddDbContext<ApplicationDbContext>(options
                => options.UseSqlServer(connection + appSettings.DataBase[DatabaseEnums.ApplicationDb.ToString()]));
        }
    }
}
