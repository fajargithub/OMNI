using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Models.Master
{
    public class RekomendasiJenisModel : BaseModel
    {
        public string Port { get; set; }
        public string SpesifikasiJenis { get; set; }
        public string RekomendasiType { get; set; }
        public float Value { get; set; }
    }
}
