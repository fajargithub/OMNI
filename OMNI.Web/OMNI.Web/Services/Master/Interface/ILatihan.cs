using OMNI.Utilities.Base;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Master.Interface
{
    public interface ILatihan 
    {
        public Task<List<Latihan>> GetAllByPortId(int portId);
        public Task<Latihan> GetById(int id);
        public Task<BaseJson<LatihanModel>> AddEdit(LatihanModel model);
        public Task<Latihan> Delete(int id);
    }
}
