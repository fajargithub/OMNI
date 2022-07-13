using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Data.Data.Dao
{
    public class PersonilTrx : BaseDao
    {
        public virtual Latihan Latihan { get; set; }
        public int PortId { get; set; }
        public string Satuan { get; set; }
        public float TotalDetailExisting { get; set; }
        public float TanggalPelatihan { get; set; }
        public float TanggalExpired { get; set; }
        public int SisaMasaBerlaki { get; set; }
        public float SelisihHubla { get; set; }
        public string KesesuaianPM58 { get; set; }
        public float PersentasePersonil { get; set; }
    }
}
