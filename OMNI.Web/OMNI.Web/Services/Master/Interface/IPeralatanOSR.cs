using Microsoft.AspNetCore.Mvc;
using OMNI.Utilities.Base;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Master.Interface
{
    public interface IPeralatanOSR 
    {
        public Task<List<PeralatanOSR>> GetAllFromHttp();
        public Task<PeralatanOSR> GetById(int id);
        public Task<BaseJson<PeralatanOSRModel>> AddEdit(PeralatanOSRModel model);

        public Task<PeralatanOSR> Delete(int id);
    }
}
