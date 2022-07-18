using Microsoft.AspNetCore.Mvc;
using OMNI.Utilities.Base;
using OMNI.Utilities.Constants;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models;
using OMNI.Web.Models.Master;
using OMNI.Web.Services.Master.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        public async Task<JsonResult> GetAll()
        {
            List<PeralatanOSR> data = await _peralatanOSRService.GetAllFromHttp(); 

            int count = data.Count();

            return Json(new
            {
                success = true,
                recordsTotal = count,
                recordsFiltered = count,
                data
            });
        }

        public async Task<PeralatanOSRModel> GetDataById(int id)
        {
            PeralatanOSRModel model = new PeralatanOSRModel();
            if (id > 0)
            {
                PeralatanOSR data = await _peralatanOSRService.GetById(id);
                if (data != null)
                {
                    model.Id = data.Id;
                    model.Name = data.Name;
                    model.Desc = data.Desc;
                }
            }

            return model;
        }

        [HttpPost]
        public async Task<IActionResult> AddEditFunction(PeralatanOSRModel model)
        {
            var r = await _peralatanOSRService.AddEdit(model);

            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            }

            return Ok(new JsonResponse());
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFunction(int id)
        {
            var r = await _peralatanOSRService.Delete(id);

            return Ok(new JsonResponse());
        }
    }
}
