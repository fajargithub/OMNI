using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Migrations.Data.Dao.CorePTK
{
    public class ShipEmployee : BaseDao
    {
        public virtual Employee Employee { get; set; }
        public virtual Ship Ship { get; set; }
    }
}
