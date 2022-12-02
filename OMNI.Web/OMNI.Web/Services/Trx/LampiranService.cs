using OMNI.Utilities.Base;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models.Master;
using OMNI.Web.Services.Master.Interface;
using OMNI.Web.Services.Trx.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Trx
{
    public class LampiranService : ILampiran
    {
        private readonly IHttpClientFactory _httpClient;

        public LampiranService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<LampiranModel>> GetAllByPort(string port)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/Lampiran/GetAllByPort?port={port}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<LampiranModel>>();

            throw new Exception();
        }

        public async Task<LampiranModel> GetById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/Lampiran/{id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<LampiranModel>();

            throw new Exception();
        }

        public async Task<BaseJson<LampiranModel>> AddEdit(LampiranModel m)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                MultipartFormDataContent data = new MultipartFormDataContent();

                data.Add(new StringContent(m.Id.ToString()), "Id");
                data.Add(new StringContent(m.Port.ToString()), "Port");
                data.Add(new StringContent(m.LampiranType.ToString()), "LampiranType");
                data.Add(new StringContent(m.StartDate.ToString()), "StartDate");
                if(m.LampiranType == "PENILAIAN")
                {
                    data.Add(new StringContent(m.EndDate.ToString()), "EndDate");
                }

                data.Add(new StringContent(m.Name.ToString()), "Name");
                if (string.IsNullOrEmpty(m.Remark))
                {
                    m.Remark = "";
                }
                data.Add(new StringContent(m.Remark), "Remark");

                if (m.Files != null)
                {
                    if (m.Files.Count() > 0)
                    {
                        for (int i = 0; i < m.Files.Count(); i++)
                        {
                            data.Add(
                            new StreamContent(m.Files[i].OpenReadStream())
                            {
                                Headers =
                                    {
                                    ContentLength = m.Files[i].Length,
                                    ContentType = new MediaTypeHeaderValue(m.Files[i].ContentType)
                                    }
                            }, "Files", m.Files[i].FileName
                        );
                        }
                    }
                }

                var r = await c.PostAsync("/api/Lampiran", data);

                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsAsync<BaseJson<LampiranModel>>();
                }

                throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> Delete(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var r = await client.DeleteAsync($"/api/Lampiran/{id}");


            if (r.IsSuccessStatusCode)


                return "OK";

            throw new Exception();
        }
    }
}
