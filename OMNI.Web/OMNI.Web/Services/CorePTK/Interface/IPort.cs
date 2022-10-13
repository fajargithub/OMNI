using OMNI.Utilities.Base;
using OMNI.Web.Data.Dao.CorePTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.CorePTK.Interface
{
    public interface IPort
    {
        public Task<List<Port>> GetAll();
        public Task<List<Port>> GetPortByRegion(int start);
        public Task<Port> GetById(int id);

        public Task<string> GetPortRegion(string port);
    }
}
