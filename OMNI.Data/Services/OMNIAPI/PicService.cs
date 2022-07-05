using OMNI.Data.ViewModel.OMNI;
using OMNI.Data.ViewModel.OMNI.PIC;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OMNI.Data.Services.SimontanaAPI
{
    public class PicService
    {
        private readonly IHttpClientFactory _httpClient;

        public PicService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Pic> CreatePicAsync(PicCreate m)
        {
            HttpClient c = _httpClient.CreateClient("Simontana");

            var r = await c.PostAsJsonAsync("/api/PIC", m);

            if (r.IsSuccessStatusCode)
            {
                return await r.Content.ReadAsAsync<Pic>();
            }

            throw new Exception();
        }

        public async Task<Pic> GetPicAsync(int id)
        {
            HttpClient c = _httpClient.CreateClient("Simontana");

            var r = await c.GetAsync($"/api/PIC/{id}");

            if (r.IsSuccessStatusCode)
            {
                return await r.Content.ReadAsAsync<Pic>();
            }

            throw new Exception();
        }

        public async Task<Pic> EditPicAsync(PicCreate m)
        {
            HttpClient c = _httpClient.CreateClient("Simontana");

            var r = await c.PutAsJsonAsync($"/api/PIC/{m.Id}", m);

            if (r.IsSuccessStatusCode)
            {
                return await r.Content.ReadAsAsync<Pic>();
            }

            throw new Exception();
        }

        public async Task<Pic> DeletePicAsync(int id)
        {
            HttpClient c = _httpClient.CreateClient("Simontana");

            var r = await c.DeleteAsync($"/api/PIC/{id}");

            if (r.IsSuccessStatusCode)
            {
                return await r.Content.ReadAsAsync<Pic>();
            }

            throw new Exception();
        }
    }
}
