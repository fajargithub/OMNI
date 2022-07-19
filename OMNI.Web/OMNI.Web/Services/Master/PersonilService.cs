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
    public class PersonilService : IPersonil
    {
        private readonly IHttpClientFactory _httpClient;

        public PersonilService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Personil>> GetAllByPortId(int portId)
        {
            var id = portId;
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/Personil/GetAllByPortId?id={portId}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<Personil>>();

            throw new Exception();
        }

        public async Task<Personil> GetById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/Personil/{id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<Personil>();

            throw new Exception();
        }

        public async Task<BaseJson<PersonilModel>> AddEdit(PersonilModel m)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                var r = await c.PostAsJsonAsync("/api/Personil", m);

                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsAsync<BaseJson<PersonilModel>>();
                }

                throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Personil> Delete(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var r = await client.DeleteAsync($"/api/Personil/{id}");

            if (r.IsSuccessStatusCode)

                return await r.Content.ReadAsAsync<Personil>();

            throw new Exception();
        }
    }
}
