using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Data.Dao
{
    public class LLPTrx : BaseDao
    {
        public virtual SpesifikasiJenis SpesifikasiJenis { get; set; }
        public int PortId { get; set; }
        public string Satuan { get; set; }
        public float DetailExisting { get; set; }
        public string Kondisi { get; set; }
        public float RekomendasiOSCP { get; set; }
        public float TotalExistingJenis { get; set; }
        public float TOtalKebutuhanHubla { get; set; }
        public float SelisihHubla { get; set; }
        public string KesesuaianPM58 { get; set; }
        public float PersentaseHubla { get; set; }
        public float TotalKebutuhanOSCP { get; set; }
        public float SelisihOSCP { get; set; }
        public string KesesuaianOSCP { get; set; }
        public float PersentaseOSCP { get; set; }
    }
}
