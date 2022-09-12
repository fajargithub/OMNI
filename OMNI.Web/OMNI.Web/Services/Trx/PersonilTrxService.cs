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
    public class PersonilTrxService : IPersonilTrx
    {
        private readonly IHttpClientFactory _httpClient;

        public PersonilTrxService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetContentType(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/PersonilTrx/GetContentType?id={id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<string>();

            throw new Exception();
        }

        public async Task<Stream> ReadFile(int id, string flag)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                var r = await c.GetAsync($"/api/PersonilTrx/ReadFile?id={id}&flag={flag}");
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
            var result = await client.GetAsync($"/api/PersonilTrx/GetAllFiles?trxId={trxId}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<FilesModel>>();

            throw new Exception();
        }
        public async Task<string> DeleteFile(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var r = await client.DeleteAsync($"/api/PersonilTrx/DeleteFile?id={id}");

            if (r.IsSuccessStatusCode)

                return await r.Content.ReadAsAsync<string>();

            throw new Exception();
        }

        public async Task<List<PersonilTrxModel>> GetAllPersonilTrx(string port, int year)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/PersonilTrx/GetAll?port={port}&year={year}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<PersonilTrxModel>>();

            throw new Exception();
        }

        public async Task<PersonilTrxModel> GetById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/PersonilTrx/{id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<PersonilTrxModel>();

            throw new Exception();
        }

        public async Task<BaseJson<PersonilTrxModel>> AddEdit(PersonilTrxModel m)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                MultipartFormDataContent data = new MultipartFormDataContent();

                data.Add(new StringContent(m.Id.ToString()), "Id");
                data.Add(new StringContent(m.Personil.ToString()), "Personil");
                data.Add(new StringContent(m.Name.ToString()), "Name");
                data.Add(new StringContent(m.Year.ToString()), "Year");
                data.Add(new StringContent(m.Port.ToString()), "Port");
                data.Add(new StringContent(m.TanggalPelatihan.ToString()), "TanggalPelatihan");
                data.Add(new StringContent(m.TanggalExpired.ToString()), "TanggalExpired");

                if(m.Files != null)
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

                var r = await c.PostAsync("/api/PersonilTrx", data);

                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsAsync<BaseJson<PersonilTrxModel>>();
                }

                throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RekomendasiPersonil> GetRekomendasiPersonilByPersonilId(int id, string port, int year)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/PersonilTrx/GetRekomendasiPersonilByPersonilId?id={id}&port={port}&year={year}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<RekomendasiPersonil>();

            throw new Exception();
        }

        public async Task<PersonilTrx> Delete(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var r = await client.DeleteAsync($"/api/PersonilTrx/{id}");

            if (r.IsSuccessStatusCode)

                return await r.Content.ReadAsAsync<PersonilTrx>();

            throw new Exception();
        }
    }
}
