using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.API.Model.OMNI
{
    public class RekomendasiLatihanModel : BaseModel
    {
        public string Port { get; set; }
        public string Latihan { get; set; }
        public string RekomendasiType { get; set; }
        public decimal Value { get; set; }
        public int Year { get; set; }
    }
}
