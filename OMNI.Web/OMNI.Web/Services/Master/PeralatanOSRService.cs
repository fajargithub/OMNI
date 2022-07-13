﻿using OMNI.Utilities.Base;
using OMNI.Web.Data;
using OMNI.Web.Data.Dao;
using OMNI.Web.Services.Master.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Master
{
    public class PeralatanOSRService : BaseService<PeralatanOSR>, IPeralatanOSR
    {
        public PeralatanOSRService(OMNIDbContext context) : base(context)
        {
        }
    }
}
