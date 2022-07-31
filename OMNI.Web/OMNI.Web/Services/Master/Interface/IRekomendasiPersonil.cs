using OMNI.Utilities.Base;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Master.Interface
{
    public interface IRekomendasiPersonil
    {
        public Task<List<RekomendasiPersonilModel>> GetAll(string port);
        public Task<RekomendasiPersonilModel> GetById(int id);
        public Task<BaseJson<RekomendasiPersonilModel>> AddEdit(RekomendasiPersonilModel model);
        public Task<RekomendasiPersonil> Delete(int id);
    }
}
