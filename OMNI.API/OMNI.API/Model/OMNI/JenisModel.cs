using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.API.Model.OMNI
{
    public class JenisModel : BaseModel
    {
        public string Name { get; set; }
        public string Satuan { get; set; }
        public string Kode { get; set; }
        public string KodeInventory { get; set; }
        public string Desc { get; set; }
    }
}
