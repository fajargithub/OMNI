﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OMNI.Web.Models.Master
{
    public class LLPTrxModel : BaseModel
    {
        public string PeralatanOSR { get; set; }
        public string Jenis { get; set; }
        public string SatuanJenis { get; set; }
        public string Port { get; set; }
        public string QRCode { get; set; }
        public decimal DetailExisting { get; set; }
        public string Kondisi { get; set; }
        public decimal TotalExistingJenis { get; set; }
        public decimal TotalExistingKeseluruhan { get; set; }
        public decimal TotalKebutuhanHubla { get; set; }
        public decimal SelisihHubla { get; set; }
        public string KesesuaianMP58 { get; set; }
        public decimal TotalKebutuhanOSCP { get; set; }
        public decimal SelisihOSCP { get; set; }
        public string KesesuaianOSCP { get; set; }
        public decimal PersentaseOSCP { get; set; }
        public decimal PersentaseHubla { get; set; }
        public decimal RekomendasiHubla { get; set; }

        [JsonIgnore]
        public List<IFormFile> Files { get; set; }
    }
}