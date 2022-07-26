﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Models.Master
{
    public class HistoryLatihanTrxModel : BaseModel
    {
        public string Latihan { get; set; }
        public string Port { get; set; }
        public string TanggalPelaksanaan { get; set; }
        public float SelisihHubla { get; set; }
        public string KesesuaianPM58 { get; set; }
        public float PersentaseLatihan { get; set; }
    }
}
