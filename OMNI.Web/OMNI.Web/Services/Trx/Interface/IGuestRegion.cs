using OMNI.Utilities.Base;
using OMNI.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Trx.Interface
{
    public interface IGuestRegion
    {
        public Task<List<GuestRegionModel>> GetAll();
        public Task<GuestRegionModel> GetByUserId(int id);
        public Task<GuestRegionModel> GetById(int id);
        public Task<BaseJson<GuestRegionModel>> AddEdit(GuestRegionModel model);
        public Task<string> Delete(int id);
    }
}
