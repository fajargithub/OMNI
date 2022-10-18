using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Models.Master
{
    public class HistoryPersonilTrxModel : BaseModel
    {
        public int PersonilTrxId { get; set; }
        public string Personil { get; set; }
        public string Port { get; set; }
        public string Name { get; set; }
        public decimal TotalDetailExisting { get; set; }
        public string TanggalPelatihan { get; set; }
        public string TanggalExpired { get; set; }
        public int SisaMasaBerlaku { get; set; }
        public decimal SelisihHubla { get; set; }
        public string KesesuaianPM58 { get; set; }
        public decimal PersentasePersonil { get; set; }
        public int Year { get; set; }
    }
}
