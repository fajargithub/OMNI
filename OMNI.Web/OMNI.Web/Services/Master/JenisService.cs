using OMNI.Web.Services.Master.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Master
{
    public class JenisService : IJenis
    {
        private readonly IHttpClientFactory _httpClient;

        public JenisService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
