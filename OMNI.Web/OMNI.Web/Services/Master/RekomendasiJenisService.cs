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
    public class RekomendasiJenisService : IRekomendasiJenis
    {
        private readonly IHttpClientFactory _httpClient;

        public RekomendasiJenisService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RekomendasiJenisModel>> GetAll(string port, string typeId, int year)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/RekomendasiJenis/GetAll?port={port}&typeId={typeId}&year={year}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<RekomendasiJenisModel>>();

            throw new Exception();
        }

        public async Task<RekomendasiJenisModel> GetById(string id, string port, string typeId, int year)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/RekomendasiJenis/GetById?id={id}&port={port}&typeId={typeId}&year={year}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<RekomendasiJenisModel>();

            throw new Exception();
        }

        public async Task<BaseJson<RekomendasiJenisModel>> AddEdit(RekomendasiJenisModel m)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                var r = await c.PostAsJsonAsync("/api/RekomendasiJenis", m);

                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsAsync<BaseJson<RekomendasiJenisModel>>();
                }

                throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RekomendasiJenis> Delete(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var r = await client.DeleteAsync($"/api/RekomendasiJenis/{id}");

            if (r.IsSuccessStatusCode)

                return await r.Content.ReadAsAsync<RekomendasiJenis>();

            throw new Exception();
        }

        public async Task<RekomendasiJenis> UpdateValue(int id, string port, int typeId, decimal value, int year)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var r = await client.GetAsync($"/api/RekomendasiJenis/UpdateValue?id={id}&port={port}&typeId={typeId}&value={value}&year={year}");

            if (r.IsSuccessStatusCode)

                return await r.Content.ReadAsAsync<RekomendasiJenis>();

            throw new Exception();
        }
    }
}
