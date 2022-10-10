using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OMNI.API.Model.OMNI
{
    public class LLPTrxModel : BaseModel
    {
        public int PeralatanOSRId { get; set; }
        public int SpesifikasiJenisId { get; set; }
        public string PeralatanOSR { get; set; }
        public string PeralatanOSRName { get; set; }
        public string Jenis { get; set; }
        public string JenisName { get; set; }
        public string SatuanJenis { get; set; }
        public string Port { get; set; }
        public string KodeInventory { get; set; }
        public string Region { get; set; }
        public string QRCode { get; set; }
        public string QRCodeText { get; set; }
        public decimal DetailExisting { get; set; }
        public string Kondisi { get; set; }
        public decimal TotalExistingJenis { get; set; }
        public decimal TotalExistingKeseluruhan { get; set; }
        public decimal TotalKebutuhanHubla { get; set; }
        public decimal SelisihHubla { get; set; }
        public string KesesuaianPM58 { get; set; }
        public decimal TotalKebutuhanOSCP { get; set; }
        public decimal SelisihOSCP { get; set; }
        public string KesesuaianOSCP { get; set; }
        public decimal PersentaseOSCP { get; set; }
        public decimal PersentaseHubla { get; set; }
        public decimal RekomendasiHubla { get; set; }
        public decimal RekomendasiOSCP { get; set; }
        public int Year { get; set; }
        public string Brand { get; set; }
        public string SerialNumber { get; set; }
        public string NoAsset { get; set; }
        public string Status { get; set; }

        [JsonIgnore]
        public IFormFileCollection Files { get; set; }
    }
}
