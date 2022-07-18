using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.API.Model.OMNI
{
    public class LLPModel : BaseModel
    {
        public string Port { get; set; }
        public string SpesifikasiJenis { get; set; }
        public string Satuan { get; set; }
        public float DetailExisting { get; set; }
        public string Kondisi { get; set; }
        public float RekomendasiOSCP { get; set; }
        public float TotalExistingJenis { get; set; }
        public float TotalExistingKeseluruhan { get; set; }
        public float TotalKebutuhanHubla { get; set; }
        public float SelisihHubla { get; set; }
        public string KesesuaianPM58 { get; set; }
        public float TotalKebutuhanOSCP { get; set; }
        public float SelisihOSCP { get; set; }
        public string KesesuaianOSCP { get; set; }
        public float PersentaseOSCP { get; set; }
    }
}
