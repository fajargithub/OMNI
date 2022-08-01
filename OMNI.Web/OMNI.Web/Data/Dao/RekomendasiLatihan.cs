using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Data.Dao
{
    public class RekomendasiLatihan : BaseDao
    {
        public string Port { get; set; }
        public virtual Latihan Latihan { get; set; }
        public virtual RekomendasiType RekomendasiType { get; set; }
        public decimal Value { get; set; }
    }
}
