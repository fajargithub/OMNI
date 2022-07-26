using OMNI.Web.Services.Master.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Master
{
    public class RekomendasiJenisService : IRekomendasiJenis
    {
        private readonly IHttpClientFactory _httpClient;

        public RekomendasiJenisService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
