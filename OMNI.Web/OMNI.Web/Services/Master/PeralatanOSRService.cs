using OMNI.Utilities.Base;
using OMNI.Web.Data;
using OMNI.Web.Data.Dao;
using OMNI.Web.Services.Master.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Master
{
    public class PeralatanOSRService : IPeralatanOSR
    {
        private readonly IHttpClientFactory _httpClient;

        public PeralatanOSRService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
