using OMNI.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace OMNI.Data.Model.OMNI
{
    public class LatihanTrxModel : BaseModel
    {
        public string Latihan { get; set; }
        public int PortId { get; set; }
        public string Satuan { get; set; }
        public string TanggalPelaksana { get; set; }
        public float SelisihHubla { get; set; }
        public string KesesuaianPM58 { get; set; }
        public float PersentasiLatihan { get; set; }
    }
}
