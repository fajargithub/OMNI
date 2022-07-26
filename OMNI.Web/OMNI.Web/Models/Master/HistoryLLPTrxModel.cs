using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Models.Master
{
    public class HistoryLLPTrxModel : BaseModel
    {
        public string SpesifikasiJenis { get; set; }
        public string Port { get; set; }
        public string QRCode { get; set; }
        public float DetailExisting { get; set; }
        public string Kondisi { get; set; }
        public float TotalExistingJenis { get; set; }
        public float TotalExistingKeseluruhan { get; set; }
        public float TotalKebutuhanHubla { get; set; }
        public float SelisihHubla { get; set; }
        public string KesesuaianMP58 { get; set; }
        public float TotalKebutuhanOSCP { get; set; }
        public float SelisihOSCP { get; set; }
        public string KesesuaianOSCP { get; set; }
        public float PersentaseOSCP { get; set; }
    }
}
