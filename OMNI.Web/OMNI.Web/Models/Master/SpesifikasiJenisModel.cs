using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Models.Master
{
    public class SpesifikasiJenisModel : BaseModel
    {
        public string PeralatanOSR { get; set; }
        public string Port { get; set; }
        public string Name { get; set; }
        public string QRCode { get; set; }
        public float RekomendasiHubla { get; set; }
        public string Desc { get; set; }
    }
}
