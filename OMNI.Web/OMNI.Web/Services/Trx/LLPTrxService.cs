﻿using OMNI.Utilities.Base;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models.Master;
using OMNI.Web.Services.Trx.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Trx
{
    public class LLPTrxService : ILLPTrx
    {
        private readonly IHttpClientFactory _httpClient;

        public LLPTrxService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<LLPTrxModel>> GetAllLLPTrx(string port)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/LLPTrx/GetAll?port={port}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<LLPTrxModel>>();

            throw new Exception();
        }

        public async Task<LLPTrxModel> GetById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/LLPTrx/{id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<LLPTrxModel>();

            throw new Exception();
        }

        public async Task<BaseJson<LLPTrxModel>> AddEdit(LLPTrxModel m)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                var r = await c.PostAsJsonAsync("/api/LLPTrx", m);

                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsAsync<BaseJson<LLPTrxModel>>();
                }

                throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LLPTrx> Delete(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var r = await client.DeleteAsync($"/api/LLPTrx/{id}");

            if (r.IsSuccessStatusCode)

                return await r.Content.ReadAsAsync<LLPTrx>();

            throw new Exception();
        }
    }
}