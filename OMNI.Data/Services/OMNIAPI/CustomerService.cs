using OMNI.Data.ViewModel.OMNI;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OMNI.Data.Services.SimontanaAPI
{
    public class CustomerService
    {
        private readonly IHttpClientFactory _httpClient;

        public CustomerService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateCustomerAsync(Customer m)
        {
            HttpClient c = _httpClient.CreateClient("Simontana");

            var r = await c.PostAsJsonAsync("/api/Customer", m);

            if (r.IsSuccessStatusCode)
            {
                return true;
            }

            throw new Exception();
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            HttpClient c = _httpClient.CreateClient("Simontana");

            var r = await c.GetAsync($"/api/Customer/{id}");

            if (r.IsSuccessStatusCode)
            {
                return await r.Content.ReadAsAsync<Customer>();
            }

            throw new Exception();
        }

        public async Task<bool> EditCustomerAsync(Customer m)
        {
            HttpClient c = _httpClient.CreateClient("Simontana");

            var r = await c.PutAsJsonAsync($"/api/Customer/{m.Id}", m);

            if (r.IsSuccessStatusCode)
            {
                return true;
            }

            throw new Exception();
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            HttpClient c = _httpClient.CreateClient("Simontana");

            var r = await c.DeleteAsync($"/api/Customer/{id}");

            if (r.IsSuccessStatusCode)
            {
                return true;
            }

            throw new Exception();
        }
    }
}
