using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Data.Dao
{
    public class PersonilDetail : BaseDao
    {
        public virtual Personil Personil { get; set; }
        public int PortId { get; set; }
        public float Value { get; set; }
    }
}

