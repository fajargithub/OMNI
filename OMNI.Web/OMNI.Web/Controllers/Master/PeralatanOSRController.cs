using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMNI.Utilities.Constants;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models;
using OMNI.Web.Models.Master;
using OMNI.Web.Services.Master.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers.Master
{
    [AllowAnonymous]
    public class PeralatanOSRController : BaseController
    {
        private static readonly string INDEX = "~/Views/Master/PeralatanOSR/Index.cshtml";
        private static readonly string ADD_EDIT = "~/Views/Master/PeralatanOSR/AddEdit.cshtml";

        protected IPeralatanOSR _peralatanOSRService;
        public PeralatanOSRController(IPeralatanOSR peralatanOSRService) : base()
        {
            _peralatanOSRService = peralatanOSRService;
        }

        public async Task<JsonResult> GetAll()
        {
            List<PeralatanOSR> data = await _peralatanOSRService.GetAll();

            int count = data.Count();

            return Json(new
            {
                success = true,
                recordsTotal = count,
                recordsFiltered = count,
                data
            });
        }

        public IActionResult Index()
        {
            return View(INDEX);
        }

        [HttpGet]
        public Task<JsonResult> GetAllPeralatan()
        {
            return GetAll();
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
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

            return PartialView(ADD_EDIT, model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(PeralatanOSRModel model)
        {
            var r = await _peralatanOSRService.AddEdit(model);

            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            }

            return Ok(new JsonResponse());
        }

        [HttpPost]
        public async Task<IActionResult> DeletePeralatanOSR(int id)
        {
            var r = await _peralatanOSRService.Delete(id);

            return Ok(new JsonResponse());
        }
    }
}
