using OMNI.Utilities.Base;
using OMNI.Web.Data.Dao;
using OMNI.Web.Data.Dao.CorePTK;
using OMNI.Web.Services.CorePTK.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.CorePTK
{
    public class PortService : BaseService<Port>, IPort
    {
        public PortService(CorePTKContext context) : base(context)
        {
        }
    }
}
