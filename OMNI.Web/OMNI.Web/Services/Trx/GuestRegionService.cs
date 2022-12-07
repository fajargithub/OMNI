using OMNI.Utilities.Base;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models.Master;
using OMNI.Web.Services.Master.Interface;
using OMNI.Web.Services.Trx.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Trx
{
    public class GuestRegionService : IGuestRegion
    {
        private readonly IHttpClientFactory _httpClient;

        public GuestRegionService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GuestRegionModel>> GetAll()
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/GuestRegion/GetAll");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<GuestRegionModel>>();

            throw new Exception();
        }

        public async Task<GuestRegionModel> GetByUserId(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/GuestRegion/GetByUserId?id={id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<GuestRegionModel>();

            throw new Exception();
        }

        public async Task<GuestRegionModel> GetById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/GuestRegion/{id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<GuestRegionModel>();

            throw new Exception();
        }

        public async Task<BaseJson<GuestRegionModel>> AddEdit(GuestRegionModel m)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                var r = await c.PostAsJsonAsync("/api/GuestRegion", m);

                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsAsync<BaseJson<GuestRegionModel>>();
                }

                throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> Delete(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var r = await client.DeleteAsync($"/api/GuestRegion/{id}");


            if (r.IsSuccessStatusCode)


                return "OK";

            throw new Exception();
        }
    }
}
