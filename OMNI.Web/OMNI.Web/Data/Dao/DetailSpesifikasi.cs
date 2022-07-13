using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Data.Dao
{
    public class DetailSpesifikasi : BaseDao
    {
        public virtual SpesifikasiJenis SpesifikasiJenis { get; set; }
        public int PortId { get; set; }
        public string QRCode { get; set; }
        public float RekomendasiHubla { get; set; }
    }
}
