﻿using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Data.Dao
{
    public class Personil : BaseDao
    {
        public string Name { get; set; }
        public string Satuan { get; set; }
        public string Desc { get; set; }
    }
}