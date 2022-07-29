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
    public class SpesifikasiJenisService : ISpesifikasiJenis
    {
        private readonly IHttpClientFactory _httpClient;

        public SpesifikasiJenisService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SpesifikasiJenisModel>> GetAll()
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync("/api/SpesifikasiJenis/GetAll");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<SpesifikasiJenisModel>>();

            throw new Exception();
        }

        public async Task<SpesifikasiJenisModel> GetById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/SpesifikasiJenis/{id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<SpesifikasiJenisModel>();

            throw new Exception();
        }

        public async Task<BaseJson<SpesifikasiJenisModel>> AddEdit(SpesifikasiJenisModel m)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                var r = await c.PostAsJsonAsync("/api/SpesifikasiJenis", m);

                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsAsync<BaseJson<SpesifikasiJenisModel>>();
                }

                throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SpesifikasiJenis> Delete(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var r = await client.DeleteAsync($"/api/SpesifikasiJenis/{id}");

            if (r.IsSuccessStatusCode)

                return await r.Content.ReadAsAsync<SpesifikasiJenis>();

            throw new Exception();
        }
    }
}
