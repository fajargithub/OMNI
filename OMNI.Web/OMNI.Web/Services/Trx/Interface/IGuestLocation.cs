using OMNI.Utilities.Base;
using OMNI.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Trx.Interface
{
    public interface IGuestLocation
    {
        public Task<List<GuestLocationModel>> GetAll();
        public Task<GuestLocationModel> GetByUserId(int id);
        public Task<GuestLocationModel> GetById(int id);
        public Task<BaseJson<GuestLocationModel>> AddEdit(GuestLocationModel model);
        public Task<string> Delete(int id);
    }
}
