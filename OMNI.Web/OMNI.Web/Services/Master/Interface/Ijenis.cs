using OMNI.Utilities.Base;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Master.Interface
{
    public interface IJenis
    {
        public Task<List<Jenis>> GetAll();
        public Task<Jenis> GetById(int id);
        public Task<BaseJson<JenisModel>> AddEdit(JenisModel model);
        public Task<Jenis> Delete(int id);
    }
}
