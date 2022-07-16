using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Data.Dao.CorePTK
{
    public class PAreaSub : BaseDao
    {
        [Required]
        public virtual PArea PArea { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }
    }
}
