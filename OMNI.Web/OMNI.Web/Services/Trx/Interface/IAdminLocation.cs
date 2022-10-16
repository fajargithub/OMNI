using OMNI.Utilities.Base;
using OMNI.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Trx.Interface
{
    public interface IAdminLocation
    {
        public Task<List<AdminLocationModel>> GetAll();
        public Task<AdminLocationModel> GetByUserId(int id);
        public Task<AdminLocationModel> GetById(int id);
        public Task<BaseJson<AdminLocationModel>> AddEdit(AdminLocationModel model);
        public Task<string> Delete(int id);
    }
}
