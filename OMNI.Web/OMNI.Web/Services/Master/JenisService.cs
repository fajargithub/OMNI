using OMNI.Utilities.Base;
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
    public class JenisService : IJenis
    {
        private readonly IHttpClientFactory _httpClient;

        public JenisService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Jenis>> GetAll()
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync("/Api/Jenis");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<Jenis>>();

            throw new Exception();
        }

        public async Task<Jenis> GetById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/Jenis/{id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<Jenis>();

            throw new Exception();
        }

        public async Task<BaseJson<JenisModel>> AddEdit(JenisModel m)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                var r = await c.PostAsJsonAsync("/api/Jenis", m);

                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsAsync<BaseJson<JenisModel>>();
                }

                throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Jenis> Delete(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var r = await client.DeleteAsync($"/api/Jenis/{id}");

            if (r.IsSuccessStatusCode)

                return await r.Content.ReadAsAsync<Jenis>();

            throw new Exception();
        }
    }
}
