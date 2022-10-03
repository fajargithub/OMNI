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
    public class StatusService : IStatus
    {
        private readonly IHttpClientFactory _httpClient;

        public StatusService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Status>> GetAll()
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync("/api/Status");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<Status>>();

            throw new Exception();
        }

        public async Task<Status> GetById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/Status/{id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<Status>();

            throw new Exception();
        }

        public async Task<BaseJson<StatusModel>> AddEdit(StatusModel m)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                var r = await c.PostAsJsonAsync("/api/Status", m);

                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsAsync<BaseJson<StatusModel>>();
                }

                throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Status> Delete(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var r = await client.DeleteAsync($"/api/Status/{id}");

            if (r.IsSuccessStatusCode)

                return await r.Content.ReadAsAsync<Status>();

            throw new Exception();
        }
    }
}
