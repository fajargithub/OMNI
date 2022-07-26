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
    public class KondisiService : IKondisi
    {
        private readonly IHttpClientFactory _httpClient;

        public KondisiService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Kondisi>> GetAll()
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync("/Api/Kondisi");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<Kondisi>>();

            throw new Exception();
        }

        public async Task<Kondisi> GetById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/Kondisi/{id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<Kondisi>();

            throw new Exception();
        }

        public async Task<BaseJson<KondisiModel>> AddEdit(KondisiModel m)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                var r = await c.PostAsJsonAsync("/api/Kondisi", m);

                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsAsync<BaseJson<KondisiModel>>();
                }

                throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Kondisi> Delete(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var r = await client.DeleteAsync($"/api/Kondisi/{id}");

            if (r.IsSuccessStatusCode)

                return await r.Content.ReadAsAsync<Kondisi>();

            throw new Exception();
        }
    }
}
