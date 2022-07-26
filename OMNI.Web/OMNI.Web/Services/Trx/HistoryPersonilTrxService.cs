using OMNI.Web.Services.Trx.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Trx
{
    public class HistoryPersonilTrxService : IHistoryPersonilTrx
    {
        private readonly IHttpClientFactory _httpClient;

        public HistoryPersonilTrxService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
