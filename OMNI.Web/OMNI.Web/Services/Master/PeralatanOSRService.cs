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
    public class PeralatanOSRService : IPeralatanOSR
    {
        private readonly IHttpClientFactory _httpClient;

        public PeralatanOSRService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PeralatanOSR>> GetAll()
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync("/Api/PeralatanOSR");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<PeralatanOSR>>();

            throw new Exception();
        }

        public async Task<PeralatanOSR> GetById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/PeralatanOSR/{id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<PeralatanOSR>();

            throw new Exception();
        }

        public async Task<BaseJson<PeralatanOSRModel>> AddEdit(PeralatanOSRModel m)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                var r = await c.PostAsJsonAsync("/api/PeralatanOSR", m);

                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsAsync<BaseJson<PeralatanOSRModel>>();
                }

                throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PeralatanOSR> Delete(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var r = await client.DeleteAsync($"/api/PeralatanOSR/{id}");

            if (r.IsSuccessStatusCode)

                return await r.Content.ReadAsAsync<PeralatanOSR>();

            throw new Exception();
        }
    }
}
