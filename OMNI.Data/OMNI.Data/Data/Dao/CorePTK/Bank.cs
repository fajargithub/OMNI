using OMNI.Utilities.Base;
using OMNI.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Migrations.Data.Dao.CorePTK
{
    public class Bank : BaseDao
    {
        [StringLength(10, ErrorMessage = GeneralConstants.ErrorMessageFieldLength)]
        public string Code { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = GeneralConstants.ErrorMessageFieldLength)]
        public string Name { get; set; }

        [StringLength(20, ErrorMessage = GeneralConstants.ErrorMessageFieldLength)]
        public string Alias { get; set; }
    }
}
