﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Models.Master
{
    public class RekomendasiPersonilModel : BaseModel
    {
        public string Port { get; set; }
        public string Personil { get; set; }
        public string RekomendasiType { get; set; }
        public decimal Value { get; set; }
    }
}