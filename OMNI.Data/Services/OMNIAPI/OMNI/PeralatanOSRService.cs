using OMNI.Data.Data.Dao.OMNI;
using OMNI.Data.Model.OMNI;
using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OMNI.Data.Services.OMNIAPI.OMNI
{
    public class PeralatanOSRService
    {
        private readonly IHttpClientFactory _http;

        public PeralatanOSRService(IHttpClientFactory http)
        {
            _http = http;
        }

        public async Task<List<PeralatanOSR>> GetAll()
        {
            HttpClient client = _http.CreateClient("OMNI");
            var result = await client.GetAsync("/Api/PeralatanOSR");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<PeralatanOSR>>();

            throw new Exception();
        }

        public async Task<PeralatanOSR> GetById(int id)
        {
            HttpClient client = _http.CreateClient("OMNI");
            var result = await client.GetAsync("/Api/PeralatanOSR");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<PeralatanOSR>();

            throw new Exception();
        }

        public async Task<BaseJson<PeralatanOSRModel>> AddEdit(PeralatanOSRModel m)
        {
            HttpClient c = _http.CreateClient("OMNI");

            try
            {
                var r = await c.PutAsJsonAsync($"/api/PeralatanOSR/{m.Id}", m);
                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsAsync<BaseJson<PeralatanOSRModel>>();
                }
                throw new Exception();
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
