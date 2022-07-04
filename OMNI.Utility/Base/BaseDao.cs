using System;
using System.Collections.Generic;
using System.Text;

namespace OMNI.Utilities.Base
{
    public class BaseDao
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdateDt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdateBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
}
