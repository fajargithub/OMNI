using OMNI.Web.Services.Trx.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Trx
{
    public class HistoryLatihanTrxService : IHistoryLatihanTrx
    {
        private readonly IHttpClientFactory _httpClient;

        public HistoryLatihanTrxService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
