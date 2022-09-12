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
        public Task<List<RekomendasiJenisModel>> GetAll(string port, string typeId, int year);
        public Task<RekomendasiJenisModel> GetById(string id, string port, string typeId, int year);
        public Task<BaseJson<RekomendasiJenisModel>> AddEdit(RekomendasiJenisModel model);
        public Task<RekomendasiJenis> Delete(int id);
        public Task<RekomendasiJenis> UpdateValue(int id, string port, int typeId, decimal value, int year);
    }
}
