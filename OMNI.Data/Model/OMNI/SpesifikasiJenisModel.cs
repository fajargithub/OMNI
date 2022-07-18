using OMNI.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace OMNI.Data.Model.OMNI
{
    public class SpesifikasiJenisModel : BaseModel
    {
        public string PeralatanOSR { get; set; }
        public int PortId { get; set; }
        public string Name { get; set; }
        public string QRCode { get; set; }
        public float RekomendasiHubla { get; set; }
        public string Desc { get; set; }
    }
}
