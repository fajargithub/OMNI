using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Models.Master
{
    public class DetailSpesifikasiModel : BaseModel
    {
        public string SpesifikasiJenis { get; set; }
        public string Port { get; set; }
        public string QRCode { get; set; }
        public float RekomendasiHubla { get; set; }
    }
}
