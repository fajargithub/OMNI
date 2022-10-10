using OMNI.Web.Models.Master;
using OMNI.Web.Services.Trx.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Trx
{
    public class LLPHistoryStatusService : ILLPHistoryStatus
    {
        private readonly IHttpClientFactory _httpClient;

        public LLPHistoryStatusService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LLPHistoryStatusModel> GetById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/LLPHistoryStatus/{id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<LLPHistoryStatusModel>();

            throw new Exception();
        }
        public async Task<List<LLPHistoryStatusModel>> GetAll(string port, int year)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/LLPHistoryStatus/GetAll?port={port}&year={year}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<LLPHistoryStatusModel>>();

            throw new Exception();
        }
    }
}
