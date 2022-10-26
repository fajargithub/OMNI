using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.API.Model.OMNI
{
    public class SpesifikasiJenisModel : BaseModel
    {
        public string PeralatanOSR { get; set; }
        public string Jenis { get; set; }
        public string SatuanJenis { get; set; }
        public string KodeInventory { get; set; }
        public string CreateDate { get; set; }
    }
}
