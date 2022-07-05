using OMNI.Data.ViewModel.OMNI;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OMNI.Data.Services.OMNIAPI
{
    public class UWReportService
    {
        private readonly IHttpClientFactory _httpClient;

        public UWReportService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateUWReportAsync(UWReport m)
        {
            HttpClient c = _httpClient.CreateClient("Simontana");

            var r = await c.PostAsJsonAsync("/api/UWReport", m);

            if (r.IsSuccessStatusCode)
            {
                return true;
            }

            throw new Exception();
        }

        public async Task<UWReport> GetUWReportAsync(int id)
        {
            HttpClient c = _httpClient.CreateClient("Simontana");

            var r = await c.GetAsync($"/api/UWReport/{id}");

            if (r.IsSuccessStatusCode)
            {
                return await r.Content.ReadAsAsync<UWReport>();
            }

            throw new Exception();
        }

        public async Task<bool> EditUWReportAsync(UWReport m)
        {
            HttpClient c = _httpClient.CreateClient("Simontana");

            var r = await c.PutAsJsonAsync($"/api/UWReport/{m.Id}", m);

            if (r.IsSuccessStatusCode)
            {
                return true;
            }

            throw new Exception();
        }

        public async Task<bool> DeleteUWReportAsync(int id)
        {
            HttpClient c = _httpClient.CreateClient("Simontana");

            var r = await c.DeleteAsync($"/api/UWReport/{id}");

            if (r.IsSuccessStatusCode)
            {
                return true;
            }

            throw new Exception();
        }
    }
}
