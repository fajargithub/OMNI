using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Models.Master
{
    public class HistoryLatihanTrxModel : BaseModel
    {
        public int LatihanTrxId { get; set; }
        public string Latihan { get; set; }
        public string Port { get; set; }
        public string TanggalPelaksanaan { get; set; }
        public decimal SelisihHubla { get; set; }
        public string KesesuaianPM58 { get; set; }
        public decimal PersentaseLatihan { get; set; }
        public int Year { get; set; }
        public string TrxStatus { get; set; }
        public string Satuan { get; set; }
    }
}
