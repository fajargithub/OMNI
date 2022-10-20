using OMNI.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Trx.Interface
{
    public interface IHistoryLatihanTrx
    {
        public Task<HistoryLatihanTrxModel> GetHistoryLatihanTrxById(int id);
        public Task<List<HistoryLatihanTrxModel>> GetAllHistoryLatihanTrx(int trxId, string port, int year);
    }
}
