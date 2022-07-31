using OMNI.Utilities.Base;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Master.Interface
{
    public interface IRekomendasiType
    {
        public Task<List<RekomendasiType>> GetAll();
        public Task<RekomendasiType> GetById(int id);
        public Task<BaseJson<RekomendasiTypeModel>> AddEdit(RekomendasiTypeModel model);
        public Task<RekomendasiType> Delete(int id);
    }
}
