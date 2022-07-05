using OMNI.Data.ViewModel.OMNI.Account;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OMNI.Data.Services.SimontanaAPI
{
    public class AccountService
    {
        private readonly IHttpClientFactory _httpClient;

        public AccountService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateAccountAsync(AccountCreate m)
        {
            HttpClient c = _httpClient.CreateClient("Simontana");

            var r = await c.PostAsJsonAsync("/api/Account", m);

            if (r.IsSuccessStatusCode)
            {
                return true;
            }

            throw new Exception();
        }

        public async Task<bool> EditAccountAsync(AccountCreate m)
        {
            HttpClient c = _httpClient.CreateClient("Simontana");

            var r = await c.PutAsJsonAsync("/api/Account", m);

            if (r.IsSuccessStatusCode)
            {
                return true;
            }

            throw new Exception();
        }

        public async Task<Account> DeleteAccountAsync(int id)
        {
            HttpClient c = _httpClient.CreateClient("Simontana");

            var r = await c.DeleteAsync($"/api/Account/{id}");

            if (r.IsSuccessStatusCode)
            {
                return await r.Content.ReadAsAsync<Account>();
            }

            throw new Exception();
        }

        public async Task<Account> GetAccountAsync(int id)
        {
            HttpClient c = _httpClient.CreateClient("Simontana");

            var r = await c.GetAsync($"/api/Account/{id}");

            if (r.IsSuccessStatusCode)
            {
                return await r.Content.ReadAsAsync<Account>();
            }

            throw new Exception();
        }
    }
}
