using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.API.Model.OMNI
{
    public class PersonilModel : BaseModel
    {
        public int PortId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public float Value { get; set; }
    }
}
