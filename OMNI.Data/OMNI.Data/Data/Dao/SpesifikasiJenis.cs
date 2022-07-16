using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Data.Data.Dao
{
    public class SpesifikasiJenis : BaseDao
    {
        public virtual PeralatanOSR PeralatanOSR { get; set; }
        public int PortId { get; set; }
        public string Name { get; set; }
        public string QRCode { get; set; }
        public float RekomendasiHubla { get; set; }
        public string Desc { get; set; }
    }
}
