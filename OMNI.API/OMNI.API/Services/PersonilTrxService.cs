using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OMNI.API.Configurations;
using OMNI.API.Services.Interfaces;
using OMNI.Data.Data;
using OMNI.Data.Data.Dao;
using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
using System.Threading.Tasks;
using OMNI.API.Model.OMNI;

namespace OMNI.API.Services
{
    public class PersonilTrxService : BaseService<PersonilTrx>, IPersonilTrx
    {
        private IConfiguration _configuration;
        public PersonilTrxService(OMNIDbContext context, IConfiguration configuration) : base(context)
        {
            _configuration = configuration;
        }

        //public IDbConnection Connection
        //{
        //    get { return new SqlConnection(_configuration.Get<AppSettings>().ConnectionStrings["DevConnectionMode"]); }
        //}

        public IDbConnection Connection
        {
            get
            {
                var appSettings = _configuration.Get<AppSettings>();
                bool isProd = appSettings.IsProduction;
                var prod = appSettings.ConnectionStrings["ProdConnectionMode"] + appSettings.DataBase["OMNIDb"];
                var stag = appSettings.ConnectionStrings["DevConnectionMode"] + appSettings.DataBase["OMNIDb"];
                return new SqlConnection(isProd ? prod : stag);
            }
        }

        public List<GetPersonilTrxByIdSPModel> GetPersonilTrxById(int id)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("PersonilTrxId", id);
                    dbConnection.Open();
                    return dbConnection.Query<GetPersonilTrxByIdSPModel>("GetPersonilTrxById", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }     
        }

        public List<GetPersonilTrxByIdSPModel> GetAllPersonilTrx()
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    return dbConnection.Query<GetPersonilTrxByIdSPModel>("GetAllPersonilTrx", commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
