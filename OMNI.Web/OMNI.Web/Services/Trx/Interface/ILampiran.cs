using OMNI.Utilities.Base;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Trx.Interface
{
    public interface ILampiran
    {
        public Task<List<Lampiran>> GetAll();
        public Task<Lampiran> GetById(int id);
        public Task<BaseJson<LampiranModel>> AddEdit(LampiranModel model);
        public Task<Lampiran> Delete(int id);
    }
}
