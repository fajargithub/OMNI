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
    public class GuestLocationService : IGuestLocation
    {
        private readonly IHttpClientFactory _httpClient;

        public GuestLocationService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GuestLocationModel>> GetAll()
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/GuestLocation/GetAll");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<GuestLocationModel>>();

            throw new Exception();
        }

        public async Task<GuestLocationModel> GetByUserId(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/GuestLocation/GetByUserId?id={id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<GuestLocationModel>();

            throw new Exception();
        }

        public async Task<GuestLocationModel> GetById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/GuestLocation/{id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<GuestLocationModel>();

            throw new Exception();
        }

        public async Task<BaseJson<GuestLocationModel>> AddEdit(GuestLocationModel m)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                var r = await c.PostAsJsonAsync("/api/GuestLocation", m);

                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsAsync<BaseJson<GuestLocationModel>>();
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
            var r = await client.DeleteAsync($"/api/GuestLocation/{id}");


            if (r.IsSuccessStatusCode)


                return "OK";

            throw new Exception();
        }
    }
}
