using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Data.Dao
{
    public class LatihanTrx : BaseDao
    {
        public virtual Latihan Latihan { get; set; }
        public int PortId { get; set; }
        public string Satuan { get; set; }
        public DateTime TanggalPelaksana { get; set; }
        public float SelisihHubla { get; set; }
        public string KesesuaianPM58 { get; set; }
        public float PersentaseLatihan { get; set; }
    }
}
