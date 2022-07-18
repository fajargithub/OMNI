using OMNI.Utilities.Base;
using OMNI.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Data.Data.Dao
{
    public class OMNIActivityLog : BaseDao
    {
        public string TrxId { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(40)]
        public string Controller { get; set; }

        [StringLength(40)]
        public string Method { get; set; }

        [Required]
        [StringLength(10)]
        public string Status { get; set; }

        [StringLength(400)]
        public string Info { get; set; } = GeneralConstants.GetLocalIPAddress();

        public string ErrorMessage { get; set; }

        [StringLength(400)]
        public string Remark { get; set; }
    }
}
