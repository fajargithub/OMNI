using Microsoft.AspNetCore.Mvc;
using OMNI.Utilities.Base;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models;
using OMNI.Web.Models.Master;
using OMNI.Web.Services.Trx.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Trx
{
    public class LatihanTrxService : ILatihanTrx
    {
        private readonly IHttpClientFactory _httpClient;

        public LatihanTrxService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetContentType(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/LatihanTrx/GetContentType?id={id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<string>();

            throw new Exception();
        }

        public async Task<Stream> ReadFile(int id, string flag)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                var r = await c.GetAsync($"/api/LatihanTrx/ReadFile?id={id}&flag={flag}");
                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsStreamAsync();
                }
                throw new Exception();
            }
            catch (Exception e) { throw e; }
        }

        public async Task<List<FilesModel>> GetAllFiles(int trxId)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/LatihanTrx/GetAllFiles?trxId={trxId}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<FilesModel>>();

            throw new Exception();
        }
        public async Task<string> DeleteFile(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var r = await client.DeleteAsync($"/api/LatihanTrx/DeleteFile?id={id}");

            if (r.IsSuccessStatusCode)

                return await r.Content.ReadAsAsync<string>();

            throw new Exception();
        }

        public async Task<List<LatihanTrxModel>> GetAllLatihanTrx(string port)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/LatihanTrx/GetAll?port={port}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<LatihanTrxModel>>();

            throw new Exception();
        }

        public async Task<LatihanTrxModel> GetById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/LatihanTrx/{id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<LatihanTrxModel>();

            throw new Exception();
        }

        public async Task<BaseJson<LatihanTrxModel>> AddEdit(LatihanTrxModel m)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                MultipartFormDataContent data = new MultipartFormDataContent();

                data.Add(new StringContent(m.Id.ToString()), "Id");
                data.Add(new StringContent(m.Port.ToString()), "Port");
                data.Add(new StringContent(m.Latihan.ToString()), "Latihan");
                data.Add(new StringContent(m.Satuan.ToString()), "Satuan");
                data.Add(new StringContent(m.TanggalPelaksanaan.ToString()), "TanggalPelaksanaan");

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

                var r = await c.PostAsync("/api/LatihanTrx", data);

                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsAsync<BaseJson<LatihanTrxModel>>();
                }

                throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RekomendasiLatihan> GetRekomendasiLatihanByLatihanId(int id, string port)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/LatihanTrx/GetRekomendasiLatihanByLatihanId?id={id}&port={port}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<RekomendasiLatihan>();

            throw new Exception();
        }

        public async Task<LatihanTrx> Delete(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var r = await client.DeleteAsync($"/api/LatihanTrx/{id}");

            if (r.IsSuccessStatusCode)

                return await r.Content.ReadAsAsync<LatihanTrx>();

            throw new Exception();
        }
    }
}
