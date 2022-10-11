using OMNI.Utilities.Base;
using OMNI.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Migrations.Data.Dao.CorePTK
{
    public class PortEmployee : BaseDao
    {
        public virtual Employee Employee { get; set; }

        [ForeignKey("EmployeePositionId")]
        [Column("EmployeePositionId")]
        public virtual EmployeePosition EmployeePosition { get; set; }
    }
}
