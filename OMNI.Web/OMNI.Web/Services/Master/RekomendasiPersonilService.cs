using OMNI.Web.Services.Master.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Master
{
    public class RekomendasiPersonilService : IRekomendasiPersonil
    {
        private readonly IHttpClientFactory _httpClient;

        public RekomendasiPersonilService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
