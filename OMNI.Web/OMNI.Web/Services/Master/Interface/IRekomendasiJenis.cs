using OMNI.Utilities.Base;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Master.Interface
{
    public interface IRekomendasiJenis
    {
        public Task<List<RekomendasiJenisModel>> GetAll(string port);
        public Task<RekomendasiJenisModel> GetById(int id);
        public Task<BaseJson<RekomendasiJenisModel>> AddEdit(RekomendasiJenisModel model);
        public Task<RekomendasiJenis> Delete(int id);
    }
}
