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
    public class RekomendasiTypeService : IRekomendasiType
    {
        private readonly IHttpClientFactory _httpClient;

        public RekomendasiTypeService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RekomendasiType>> GetAll()
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync("/Api/RekomendasiType");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<RekomendasiType>>();

            throw new Exception();
        }

        public async Task<RekomendasiType> GetById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/RekomendasiType/{id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<RekomendasiType>();

            throw new Exception();
        }

        public async Task<BaseJson<RekomendasiTypeModel>> AddEdit(RekomendasiTypeModel m)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                var r = await c.PostAsJsonAsync("/api/RekomendasiType", m);

                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsAsync<BaseJson<RekomendasiTypeModel>>();
                }

                throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RekomendasiType> Delete(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var r = await client.DeleteAsync($"/api/RekomendasiType/{id}");

            if (r.IsSuccessStatusCode)

                return await r.Content.ReadAsAsync<RekomendasiType>();

            throw new Exception();
        }
    }
}
