using OMNI.Web.Models.Master;
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

        public async Task<HistoryLatihanTrxModel> GetHistoryLatihanTrxById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/HistoryTrx/GetHistoryLatihanTrx?id={id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<HistoryLatihanTrxModel>();

            throw new Exception();
        }

        public async Task<List<HistoryLatihanTrxModel>> GetAllHistoryLatihanTrx(int trxId, string port, int year)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/HistoryTrx/GetAllHistoryLatihanTrx?trxId={trxId}&port={port}&year={year}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<HistoryLatihanTrxModel>>();

            throw new Exception();
        }
    }
}
