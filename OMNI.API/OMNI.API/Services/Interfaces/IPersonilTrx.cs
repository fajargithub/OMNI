using OMNI.API.Model.OMNI;
using OMNI.Data.Data.Dao;
using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.API.Services.Interfaces
{
    public interface IPersonilTrx : IBaseCrud<PersonilTrx>
    {
        public List<GetPersonilTrxByIdSPModel> GetPersonilTrxById(int id);
        public List<GetPersonilTrxByIdSPModel> GetAllPersonilTrx();
    }
}
