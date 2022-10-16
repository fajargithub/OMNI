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
    public class AdminLocationService : IAdminLocation
    {
        private readonly IHttpClientFactory _httpClient;

        public AdminLocationService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AdminLocationModel>> GetAll()
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/AdminLocation/GetAll");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<AdminLocationModel>>();

            throw new Exception();
        }

        public async Task<AdminLocationModel> GetByUserId(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/AdminLocation/GetByUserId?id={id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<AdminLocationModel>();

            throw new Exception();
        }

        public async Task<AdminLocationModel> GetById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/AdminLocation/{id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<AdminLocationModel>();

            throw new Exception();
        }

        public async Task<BaseJson<AdminLocationModel>> AddEdit(AdminLocationModel m)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                var r = await c.PostAsJsonAsync("/api/AdminLocation", m);

                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsAsync<BaseJson<AdminLocationModel>>();
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
            var r = await client.DeleteAsync($"/api/AdminLocation/{id}");


            if (r.IsSuccessStatusCode)


                return "OK";

            throw new Exception();
        }
    }
}
