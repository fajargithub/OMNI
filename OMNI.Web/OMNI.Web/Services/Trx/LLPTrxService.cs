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
    public class LLPTrxService : ILLPTrx
    {
        private readonly IHttpClientFactory _httpClient;

        public LLPTrxService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetContentType(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/LLPTrx/GetContentType?id={id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<string>();

            throw new Exception();
        }

        public async Task<Stream> ReadFile(int id, string flag)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                var r = await c.GetAsync($"/api/LLPTrx/ReadFile?id={id}&flag={flag}");
                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsStreamAsync();
                }
                throw new Exception();
            }
            catch (Exception e) { throw e; }
        }

        public async Task<List<FilesModel>> GetAllFiles(int trxId, string flag)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/LLPTrx/GetAllFiles?trxId={trxId}&flag={flag}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<FilesModel>>();

            throw new Exception();
        }
        public async Task<string> DeleteFile(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var r = await client.DeleteAsync($"/api/LLPTrx/DeleteFile?id={id}");

            if (r.IsSuccessStatusCode)

                return await r.Content.ReadAsAsync<string>();

            throw new Exception();
        }

        public async Task<List<LLPTrxModel>> GetAllLLPTrx(string port, int year)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/LLPTrx/GetAll?port={port}&year={year}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<List<LLPTrxModel>>();

            throw new Exception();
        }

        public async Task<LLPTrxModel> GetById(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/LLPTrx/{id}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<LLPTrxModel>();

            throw new Exception();
        }

        public async Task<string> GetLastNoAsset(AssetDataModel param)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var result = await client.GetAsync($"/api/LLPTrx/GetLastNoAsset?inventoryNumber={param.InventoryNumber}&primaryId={param.PrimaryId}");

            if (result.IsSuccessStatusCode)

                return await result.Content.ReadAsAsync<string>();

            throw new Exception();
        }

        public async Task<BaseJson<LLPTrxModel>> AddEdit(LLPTrxModel m)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                MultipartFormDataContent data = new MultipartFormDataContent();

                data.Add(new StringContent(m.Id.ToString()), "Id");
                data.Add(new StringContent(m.Port.ToString()), "Port");
                data.Add(new StringContent(m.Status.ToString()), "Status");
                data.Add(new StringContent(m.Year.ToString()), "Year");
                data.Add(new StringContent(m.Jenis.ToString()), "Jenis");
                data.Add(new StringContent(m.Kondisi.ToString()), "Kondisi");
                data.Add(new StringContent(m.Brand), "Brand");
                data.Add(new StringContent(m.SerialNumber), "SerialNumber");
                if (string.IsNullOrEmpty(m.Remark))
                {
                    m.Remark = "";
                }
                data.Add(new StringContent(m.Remark), "Remark");
                data.Add(new StringContent(m.QRCode.ToString()), "QRCode");
                data.Add(new StringContent(m.QRCodeText.ToString()), "QRCodeText");
                data.Add(new StringContent(m.DetailExisting.ToString()), "DetailExisting");

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

                var r = await c.PostAsync("/api/LLPTrx", data);

                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsAsync<BaseJson<LLPTrxModel>>();
                }

                throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LLPTrx> Delete(int id)
        {
            HttpClient client = _httpClient.CreateClient("OMNI");
            var r = await client.DeleteAsync($"/api/LLPTrx/{id}");

            if (r.IsSuccessStatusCode)

                return await r.Content.ReadAsAsync<LLPTrx>();

            throw new Exception();
        }

        public async Task<BaseJson<FilesModel>> AddEditFiles(FilesModel m)
        {
            HttpClient c = _httpClient.CreateClient("OMNI");

            try
            {
                MultipartFormDataContent data = new MultipartFormDataContent();

                data.Add(new StringContent(m.TrxId.ToString()), "TrxId");
                data.Add(new StringContent(m.Flag.ToString()), "Flag");

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

                var r = await c.PostAsync("/api/LLPTrx/AddEditFiles", data);

                if (r.IsSuccessStatusCode)
                {
                    return await r.Content.ReadAsAsync<BaseJson<FilesModel>>();
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
