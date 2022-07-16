using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Data.Data.Dao.CorePTK
{
    public class Port : BaseDao
    {
        [Required]
        public string Name { get; set; }
        public string Alias { get; set; }
        public virtual BranchOld Branch { get; set; }
        public virtual PAreaSub PAreaSub { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Pilotage { get; set; }
        public string RegionTime { get; set; }
        public TimeSpan? OpenTime { get; set; }
        public TimeSpan? CloseTime { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
    }
}
