using OMNI.Data.Data.Dao;
using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Migrations.Data.Dao
{
    public class Lampiran : BaseDao
    {
        public virtual LLPTrx LLPTrx { get; set; }
        public string LampiranType { get; set; } //PENILAIAN, PENGESAHAN, VERIFIKASI1, VERIFIKASI2
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
