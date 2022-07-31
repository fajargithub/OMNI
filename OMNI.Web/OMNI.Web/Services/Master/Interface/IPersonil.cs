using OMNI.Utilities.Base;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Master.Interface
{
    public interface IPersonil
    {
        public Task<List<Personil>> GetAll();
        public Task<Personil> GetById(int id);
        public Task<BaseJson<PersonilModel>> AddEdit(PersonilModel model);
        public Task<Personil> Delete(int id);
    }
}
