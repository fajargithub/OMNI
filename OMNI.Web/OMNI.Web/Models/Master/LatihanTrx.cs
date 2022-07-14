using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Models.Master
{
    public class LatihanTrx : BaseModel
    {
        public string Latihan { get; set; }
        public string Port { get; set; }
        public string Satuan { get; set; }
        public DateTime TanggalPelaksana { get; set; }
        public float SelisihHubla { get; set; }
        public string KesesuaianPM58 { get; set; }
        public float PersentaseLatihan { get; set; }
    }
}
