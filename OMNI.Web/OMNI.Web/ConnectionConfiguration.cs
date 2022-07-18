using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OMNI.Web.Configurations;
using OMNI.Web.Data;
using OMNI.Utilities.Constants;
using OMNI.Web.Data.Dao;

namespace OMNI.Web
{
    public static class ConnectionConfiguration
    {
        public static void GetService(IServiceCollection services, IConfiguration configuration, bool IsProduction)
        {
            var appSettings = configuration.Get<AppSettings>();
            var connection = IsProduction ? appSettings.ConnectionStrings[GeneralConstants.ProdConnectionMode] : appSettings.ConnectionStrings[GeneralConstants.DevConnectionMode];

            //services.AddDbContext<ApplicationDbContext>(options
            //    => options.UseSqlServer(connection + appSettings.DataBase[DbConstant.AppUserDb.ToString()]));

            //services.AddDbContext<OMNIDbContext>(options
            //    => options.UseSqlServer(connection + appSettings.DataBase[DbConstant.OMNIDb.ToString()]));

            //services.AddDbContext<CorePTKContext>(options
            //    => options.UseSqlServer(connection + appSettings.ConnectionStrings[DbConstant.CorePTKDb.ToString()]));
        }

        public enum DbConstant
        {
           AppUserDb,
           OMNIDb,
           CorePTKDb
        }
    }
}
