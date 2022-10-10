using OMNI.Data.Data.Dao;
using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Migrations.Data.Dao
{
    public class LLPHistoryStatus : BaseDao
    {
        public virtual LLPTrx LLPTrx { get; set; }
        public string Port { get; set; }
        public string Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
