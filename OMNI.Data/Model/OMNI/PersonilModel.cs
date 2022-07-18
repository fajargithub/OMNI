using OMNI.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace OMNI.Data.Model.OMNI
{
    public class PersonilModel : BaseModel
    {
        public int PortId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public float Value { get; set; }
    }
}
