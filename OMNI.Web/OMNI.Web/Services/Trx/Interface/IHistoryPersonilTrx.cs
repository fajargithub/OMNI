using OMNI.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Trx.Interface
{
    public interface IHistoryPersonilTrx
    {
        public Task<HistoryPersonilTrxModel> GetHistoryPersonilTrxById(int id);
        public Task<List<HistoryPersonilTrxModel>> GetAllHistoryPersonilTrx(int trxId, string port, int year);
    }
}
