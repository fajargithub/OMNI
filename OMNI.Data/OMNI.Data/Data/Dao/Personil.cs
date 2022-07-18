using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Data.Data.Dao
{
    public class Personil : BaseDao
    {
        public int PortId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public float Value { get; set; }
    }
}
