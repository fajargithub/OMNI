using OMNI.Utilities.Base;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Master.Interface
{
    public interface IRekomendasiLatihan
    {
        public Task<List<RekomendasiLatihanModel>> GetAll(string port, int years);
        public Task<RekomendasiLatihanModel> GetById(int id);
        public Task<BaseJson<RekomendasiLatihanModel>> AddEdit(RekomendasiLatihanModel model);
        public Task<RekomendasiLatihan> Delete(int id);
    }
}
