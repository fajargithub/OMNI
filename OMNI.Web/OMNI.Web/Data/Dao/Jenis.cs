using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Data.Dao
{
    public class Jenis : BaseDao
    {
        public string Name { get; set; }
        public string Satuan { get; set; }
        public string Kode { get; set; }
        public string InventoryKode { get; set; }
        public string Desc { get; set; }
    }
}
