using OMNI.Data.ViewModel.OMNI;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OMNI.Data.Services.SimontanaAPI
{
    public class AreaService
    {
        private readonly IHttpClientFactory _httpClient;

        public AreaService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateAreaAsync(Area m)
        {
            HttpClient c = _httpClient.CreateClient("Simontana");

            var r = await c.PostAsJsonAsync("/api/Area", m);

            if (r.IsSuccessStatusCode)
            {
                return true;
            }

            throw new Exception();
        }

        public async Task<Area> GetAreaAsync(int id)
        {
            HttpClient c = _httpClient.CreateClient("Simontana");

            var r = await c.GetAsync($"/api/area/{id}");

            if (r.IsSuccessStatusCode)
            {
                return await r.Content.ReadAsAsync<Area>();
            }

            throw new Exception();
        }

        public async Task<bool> EditAreaAsync(Area m)
        {
            HttpClient c = _httpClient.CreateClient("Simontana");

            var r = await c.PutAsJsonAsync($"/api/Area/{m.Id}", m);

            if (r.IsSuccessStatusCode)
            {
                return true;
            }

            throw new Exception();
        }

        public async Task<bool> DeleteAreaAsync(int id)
        {
            HttpClient c = _httpClient.CreateClient("Simontana");

            var r = await c.DeleteAsync($"/api/Area/{id}");

            if (r.IsSuccessStatusCode)
            {
                return true;
            }

            throw new Exception();
        }
    }
}
