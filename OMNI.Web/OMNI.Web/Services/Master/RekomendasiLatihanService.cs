using Microsoft.AspNetCore.Mvc;
using OMNI.Utilities.Base;
using OMNI.Web.Data;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models.Master;
using OMNI.Web.Services.Master.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Master
{
    public class RekomendasiLatihanService : IRekomendasiLatihan
    {
        private readonly IHttpClientFactory _httpClient;

        public RekomendasiLatihanService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RekomendasiLatihanModel>> GetAll(string port, int year)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/RekomendasiLatihan/GetAll?port={port}&year={year}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<RekomendasiLatihanModel>>();

            throw new Exception();
        }

        public async Task<RekomendasiLatihanModel> GetById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/RekomendasiLatihan/{id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<RekomendasiLatihanModel>();

            throw new Exception();
        }

        public async Task<BaseJson<RekomendasiLatihanModel>> AddEdit(RekomendasiLatihanModel m)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                var r = await c.PostAsJsonAsync("/api/RekomendasiLatihan", m);

                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsAsync<BaseJson<RekomendasiLatihanModel>>();
                }

                throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RekomendasiLatihan> Delete(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var r = await client.DeleteAsync($"/api/RekomendasiLatihan/{id}");

            if (r.IsSuccessStatusCode)

                return await r.Content.ReadAsAsync<RekomendasiLatihan>();

            throw new Exception();
        }
    }
}
