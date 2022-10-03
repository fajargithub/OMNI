using OMNI.Utilities.Base;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Master.Interface
{
    public interface IStatus
    {
        public Task<List<Status>> GetAll();
        public Task<Status> GetById(int id);
        public Task<BaseJson<StatusModel>> AddEdit(StatusModel model);
        public Task<Status> Delete(int id);
    }
}
