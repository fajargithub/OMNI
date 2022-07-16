using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Data.Data.Dao.CorePTK
{
    public class Region : BaseDao
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(10)]
        public string Code { get; set; }
    }
}
