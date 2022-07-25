using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Data.Data.Dao
{
    public class HistoryLatihanTrx : BaseDao
    {
        public virtual Latihan Latihan { get; set; }
        public string Port { get; set; }
        public DateTime TanggalPelaksanaan { get; set; }
        public float SelisihHubla { get; set; }
        public string KesesuaianPM58 { get; set; }
        public float PersentaseLatihan { get; set; }
    }
}
