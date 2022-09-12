using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Models.Master
{
    public class RekomendasiJenisModel : BaseModel
    {
        public string Port { get; set; }
        public string PeralatanOSR { get; set; }
        public string Jenis { get; set; }
        public string RekomendasiType { get; set; }
        public string TypeName { get; set; }
        public decimal Value { get; set; }
        public string Satuan { get; set; }
        public int RekomendasiJenisId { get; set; }
        public int Year { get; set; }
    }
}
