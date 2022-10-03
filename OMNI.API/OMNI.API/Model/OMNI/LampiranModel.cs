﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.API.Model.OMNI
{
    public class LampiranModel : BaseModel
    {
        public int LLPTrxId { get; set; }
        public string LampiranType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}