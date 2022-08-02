using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Models.Master
{
    public class JenisModel : BaseModel
    {
        public string Name { get; set; }
        public string Satuan { get; set; }
        public string Kode { get; set; }
        public string Desc { get; set; }
    }
}
