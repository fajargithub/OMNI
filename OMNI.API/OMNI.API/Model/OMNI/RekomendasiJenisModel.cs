using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.API.Model.OMNI
{
    public class RekomendasiJenisModel : BaseModel
    {
        public string Port { get; set; }
        public string PeralatanOSR { get; set; }
        public string Jenis { get; set; }
        public string RekomendasiType { get; set; }
        public float Value { get; set; }
        public string CreateDate { get; set; }
    }
}
