using OMNI.Utilities.Base;
using OMNI.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Migrations.Data.Dao.CorePTK
{
    public class Religion : BaseDao
    {
        [Required]
        [StringLength(50, ErrorMessage = GeneralConstants.ErrorMessageFieldLength)]
        public string Name { get; set; }
    }
}
