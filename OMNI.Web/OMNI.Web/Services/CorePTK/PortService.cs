using OMNI.Utilities.Base;
using OMNI.Web.Data.Dao;
using OMNI.Web.Data.Dao.CorePTK;
using OMNI.Web.Services.CorePTK.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace OMNI.Web.Services.CorePTK
{
    public class PortService : IPort
    {
        private readonly IHttpClientFactory _httpClient;

        public PortService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Port>> GetAll()
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync("/Api/Port");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<Port>>();

            throw new Exception();
        }

        public async Task<List<Port>> GetPortByRegion(int regionId)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/Api/Port/GetPortByRegion?regionId={regionId}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<Port>>();

            throw new Exception();
        }

        public async Task<Port> GetById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/Port/{id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<Port>();

            throw new Exception();
        }

        public async Task<string> GetPortRegion(string port)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/Port/GetPortRegion?port={port}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<string>();

            throw new Exception();
        }
    }
}
