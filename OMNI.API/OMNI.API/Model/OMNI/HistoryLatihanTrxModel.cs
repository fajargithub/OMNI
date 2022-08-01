﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.API.Model.OMNI
{
   public class HistoryLatihanTrxModel : BaseModel
    {
        public string Latihan { get; set; }
        public string Port { get; set; }
        public string TanggalPelaksanaan { get; set; }
        public decimal SelisihHubla { get; set; }
        public string KesesuaianPM58 { get; set; }
        public decimal PersentaseLatihan { get; set; }
    }
}
