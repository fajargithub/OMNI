using OMNI.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Trx.Interface
{
    public interface ILLPHistoryStatus
    {
        public Task<List<LLPHistoryStatusModel>> GetAll(string port, int year);
    }
}
