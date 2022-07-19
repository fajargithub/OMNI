using Microsoft.AspNetCore.Mvc;
using OMNI.Web.Data.Dao;
using OMNI.Web.Data.Dao.CorePTK;
using OMNI.Web.Services.CorePTK.Interface;
using OMNI.Web.Services.Master.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers
{
    public class OMNIBaseController : BaseController
    {
        protected IPort _portService;
        protected IPeralatanOSR _peralatanOSRService;
        public OMNIBaseController(IPort portService, IPeralatanOSR peralatanOSRService) : base()
        {
            _portService = portService;
            _peralatanOSRService = peralatanOSRService;
        }

        public async Task<List<PeralatanOSR>> GetAllPeralatanOSR()
        {
            List<PeralatanOSR> data = await _peralatanOSRService.GetAll();

            return data;
        }

        public async Task<List<Port>> GetAllPort()
        {
            List<Port> data = await _portService.GetAll();

            return data;
        }

        public async Task<Port> GetPortById(int id)
        {
            Port data = await _portService.GetById(id);

            return data;
        }

    }
}
