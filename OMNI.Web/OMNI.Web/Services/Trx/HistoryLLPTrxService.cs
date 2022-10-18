using OMNI.Web.Models.Master;
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

        public async Task<HistoryLLPTrxModel> GetHistoryLLPTrxById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/HistoryTrx/GetHistoryLLPTrx?id={id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<HistoryLLPTrxModel>();

            throw new Exception();
        }

        public async Task<List<HistoryLLPTrxModel>> GetAllHistoryLLPTrx(int trxId, string port, int year)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/HistoryTrx/GetAllHistoryLLPTrx?trxId={trxId}&port={port}&year={year}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<HistoryLLPTrxModel>>();

            throw new Exception();
        }
    }
}
