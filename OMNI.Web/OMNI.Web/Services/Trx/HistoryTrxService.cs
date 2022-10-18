using Microsoft.AspNetCore.Mvc;
using OMNI.Utilities.Base;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models;
using OMNI.Web.Models.Master;
using OMNI.Web.Services.Trx.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Trx
{
    public class HistoryTrxService : IHistoryTrx
    {
        private readonly IHttpClientFactory _httpClient;

        public HistoryTrxService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<HistoryLLPTrxModel>> GetAllHistoryLLPTrx(int trxId, string port, int year) { 
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/HistoryTrx/GetAllHistoryLLPTrx?trxId={trxId}&port={port}&year={year}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<HistoryLLPTrxModel>>();

            throw new Exception();
        }
    }
}
