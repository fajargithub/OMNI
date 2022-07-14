using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Models.Master
{
    public class DetaiLatihanModel : BaseModel
    {
        public string Latihan { get; set; }
        public string Port { get; set; }
        public float Value { get; set; }
    }
}
