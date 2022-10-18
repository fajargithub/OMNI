using OMNI.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Trx.Interface
{
    public interface IHistoryTrx
    {
        public Task<List<HistoryLLPTrxModel>> GetAllHistoryLLPTrx(int trxId, string port, int year);
    }
}
