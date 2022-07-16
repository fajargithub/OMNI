using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OMNI.Data.Services.OMNIAPI
{
    public class TestService
    {
        private readonly IHttpClientFactory _http;

        public TestService(IHttpClientFactory http)
        {
            _http = http;
        }

        public async Task<List<string>> GetPortName()
        {
            HttpClient client = _http.CreateClient("OMNI");
            var result = await client.GetAsync("/Api/Test");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<string>>();

            throw new Exception();
        }
    }
}
