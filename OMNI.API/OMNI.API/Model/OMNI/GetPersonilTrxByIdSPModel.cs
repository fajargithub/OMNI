using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.API.Model.OMNI
{
    public class GetPersonilTrxByIdSPModel
    {
        public string PersonilName { get; set; }
        public string PersonilSatuan { get; set; }
        public string Name { get; set; }
        public string TotalDetailExisting { get; set; }
        public string KesesuaianPM58 { get; set; }
        public string PersentasePersonil { get; set; }
        public string Port { get; set; }
        public string SisaMasaBerlaku { get; set; }
        public string Year { get; set; }
    }
}
