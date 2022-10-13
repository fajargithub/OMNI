using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Migrations.Data.Dao
{
    public class GuestLocation : BaseDao
    {
        public int UserId { get; set; }
        public string GuestCategory { get; set; }
        public string Port { get; set; }
    }
}
