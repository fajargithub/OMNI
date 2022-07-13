using OMNI.Data.Data;
using OMNI.Data.Data.Dao;
using OMNI.Data.Services.Master.Interface;
using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Data.Services.Master
{
    public class PeralatanOSRService : BaseService<PeralatanOSR>, IPeralatanOSR
    {
        public PeralatanOSRService(OMNIDbContext context) : base(context)
        {

        }
    }
}
