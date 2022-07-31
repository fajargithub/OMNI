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
    public class RekomendasiPersonilService : IRekomendasiPersonil
    {
        private readonly IHttpClientFactory _httpClient;

        public RekomendasiPersonilService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RekomendasiPersonilModel>> GetAll(string port)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/RekomendasiPersonil/GetAll?port={port}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<RekomendasiPersonilModel>>();

            throw new Exception();
        }

        public async Task<RekomendasiPersonilModel> GetById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/RekomendasiPersonil/{id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<RekomendasiPersonilModel>();

            throw new Exception();
        }

        public async Task<BaseJson<RekomendasiPersonilModel>> AddEdit(RekomendasiPersonilModel m)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                var r = await c.PostAsJsonAsync("/api/RekomendasiPersonil", m);

                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsAsync<BaseJson<RekomendasiPersonilModel>>();
                }

                throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RekomendasiPersonil> Delete(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var r = await client.DeleteAsync($"/api/RekomendasiPersonil/{id}");

            if (r.IsSuccessStatusCode)

                return await r.Content.ReadAsAsync<RekomendasiPersonil>();

            throw new Exception();
        }
    }
}
