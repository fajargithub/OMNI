using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.API.Model.OMNI
{
    public class LatihanModel : BaseModel 
    {
        public string Name { get; set; }
        public string Port { get; set; }
        public string Satuan { get; set; }
        public string TanggalPelaksana { get; set; }
        public string Value { get; set; }
        public string Desc { get; set; }
    }
}
