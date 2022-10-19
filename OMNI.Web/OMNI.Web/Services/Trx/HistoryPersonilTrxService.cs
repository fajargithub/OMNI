using OMNI.Web.Models.Master;
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

        public async Task<HistoryPersonilTrxModel> GetHistoryPersonilTrxById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/HistoryTrx/GetPersonilTrx?id={id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<HistoryPersonilTrxModel>();

            throw new Exception();
        }

        public async Task<List<HistoryPersonilTrxModel>> GetAllHistoryPersonilTrx(int trxId, string port, int year)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/HistoryTrx/GetAllHistoryPersonilTrx?trxId={trxId}&port={port}&year={year}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<HistoryPersonilTrxModel>>();

            throw new Exception();
        }
    }
}
