using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Data.Dao
{
    public class AdminLocation : BaseDao
    {
        public int UserId { get; set; }
        public string Port { get; set; }
    }
}
