using OMNI.Utilities.Base;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Master.Interface
{
    public interface IKondisi
    {
        public Task<List<Kondisi>> GetAll();
        public Task<Kondisi> GetById(int id);
        public Task<BaseJson<KondisiModel>> AddEdit(KondisiModel model);
        public Task<Kondisi> Delete(int id);
    }
}
