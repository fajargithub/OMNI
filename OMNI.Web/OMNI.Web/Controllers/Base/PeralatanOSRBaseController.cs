using Microsoft.AspNetCore.Mvc;
using OMNI.Utilities.Constants;
using OMNI.Web.Data.Dao;
using OMNI.Web.Services.Master.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers.Base
{
    public class PeralatanOSRBaseController : BaseController
    {
        protected IPeralatanOSR _peralatanOSRService;
        public PeralatanOSRBaseController(IPeralatanOSR peralatanOSRService) : base()
        {
            _peralatanOSRService = peralatanOSRService;
        }

        public JsonResult GetAll()
        {
            List<PeralatanOSR> data = _peralatanOSRService.GetAllWithFilter(b => b.IsDeleted == GeneralConstants.NO).ToList();

            int count = data.Count();

            return Json(new
            {
                success = true,
                recordsTotal = count,
                recordsFiltered = count,
                data
            });
        }
    }
}
