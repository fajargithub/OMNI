using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Data.Data.Dao
{
    public class HistoryPersonilTrx : BaseDao
    {
        public virtual Personil Personil { get; set; }
        public string Port { get; set; }
        public string Name { get; set; }
        public float TotalDetailExisting { get; set; }
        public DateTime TanggalPelatihan { get; set; }
        public DateTime TanggalExpired { get; set; }
        public int SisaMasaBerlaku { get; set; }
        public float SelisihHubla { get; set; }
        public string KesesuaianPM58 { get; set; }
        public float PersentasePersonil { get; set; }
    }
}
