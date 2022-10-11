using OMNI.Utilities.Base;
using OMNI.Web.Models.Master;
using OMNI.Web.Services.Trx.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Trx
{
    public class LoginService : ILogin
    {
        private readonly IHttpClientFactory _httpClient;

        public LoginService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BaseJson<LoginModel>> SignIn(LoginModel m)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                var r = await c.PostAsJsonAsync("/api/Login", m);

                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsAsync<BaseJson<LoginModel>>();
                }

                throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
