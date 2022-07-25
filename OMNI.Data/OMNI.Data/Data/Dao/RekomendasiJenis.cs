using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Data.Data.Dao
{
    public class RekomendasiJenis : BaseDao
    {
        public string Port { get; set; }
        public virtual SpesifikasiJenis SpesifikasiJenis { get; set; }
        public virtual RekomendasiType RekomendasiType { get; set; }
        public float Value { get; set; }
    }
}
