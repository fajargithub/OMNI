using OMNI.Web.Services.Trx.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Trx
{
    public class HistoryLLPTrxService : IHistoryLLPTrx
    {
        private readonly IHttpClientFactory _httpClient;

        public HistoryLLPTrxService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
