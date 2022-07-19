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
    public class LatihanService : ILatihan
    {
        private readonly IHttpClientFactory _httpClient;

        public LatihanService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Latihan>> GetAllByPortId(int portId)
        {
            var id = portId;
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/Latihan/GetAllByPortId?id={portId}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<Latihan>>();

            throw new Exception();
        }

        public async Task<Latihan> GetById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/Latihan/{id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<Latihan>();

            throw new Exception();
        }

        public async Task<BaseJson<LatihanModel>> AddEdit(LatihanModel m)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                var r = await c.PostAsJsonAsync("/api/Latihan", m);

                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsAsync<BaseJson<LatihanModel>>();
                }

                throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Latihan> Delete(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var r = await client.DeleteAsync($"/api/Latihan/{id}");

            if (r.IsSuccessStatusCode)

                return await r.Content.ReadAsAsync<Latihan>();

            throw new Exception();
        }
    }
}
