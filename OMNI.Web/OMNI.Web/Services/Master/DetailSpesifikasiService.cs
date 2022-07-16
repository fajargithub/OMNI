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
    public class DetailSpesifikasiService : BaseService<DetailSpesifikasi>, IDetailSpesifikasi
    {
        public DetailSpesifikasiService(OMNIDbContext context) : base(context)
        {
        }
    }
}