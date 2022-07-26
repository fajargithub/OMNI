using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Data.Dao
{
    public class RekomendasiPersonil : BaseDao
    {
        public string Port { get; set; }
        public virtual Personil Personil { get; set; }
        public virtual RekomendasiType RekomendasiType { get; set; }
        public float Value { get; set; }
    }
}
