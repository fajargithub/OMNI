using OMNI.Utilities.Base;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models.Master;
using OMNI.Web.Services.Master.Interface;
using OMNI.Web.Services.Trx.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Trx
{
    public class LampiranService : ILampiran
    {
        private readonly IHttpClientFactory _httpClient;

        public LampiranService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Lampiran>> GetAll()
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync("/Api/Lampiran");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<Lampiran>>();

            throw new Exception();
        }

        public async Task<Lampiran> GetById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/Lampiran/{id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<Lampiran>();

            throw new Exception();
        }

        public async Task<BaseJson<LampiranModel>> AddEdit(LampiranModel m)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                var r = await c.PostAsJsonAsync("/api/Lampiran", m);

                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsAsync<BaseJson<LampiranModel>>();
                }

                throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Lampiran> Delete(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var r = await client.DeleteAsync($"/api/Lampiran/{id}");

            if (r.IsSuccessStatusCode)

                return await r.Content.ReadAsAsync<Lampiran>();

            throw new Exception();
        }
    }
}
