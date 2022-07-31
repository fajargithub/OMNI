using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMNI.Utilities.Constants;
using OMNI.Web.Data.Dao;
using OMNI.Web.Data.Dao.CorePTK;
using OMNI.Web.Models;
using OMNI.Web.Models.Master;
using OMNI.Web.Services.CorePTK.Interface;
using OMNI.Web.Services.Master.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers.Master
{
    [AllowAnonymous]
    public class RekomendasiLatihanController : OMNIBaseController
    {
        private static readonly string INDEX = "~/Views/Master/RekomendasILatihan/Index.cshtml";
        private static readonly string ADD_EDIT = "~/Views/Master/RekomendasILatihan/AddEdit.cshtml";

        protected IRekomendasiLatihan _rekomendasiLatihanModel;
        protected ILatihan _latihanService;
        public RekomendasiLatihanController(ILatihan latihanService, IRekomendasiType rekomendasiTypeService, IRekomendasiLatihan RekomendasiLatihanModel, ISpesifikasiJenis spesifikasiJenisService, IPort portService, IPeralatanOSR peralatanOSRService, IJenis jenisService) : base(rekomendasiTypeService, portService, peralatanOSRService, jenisService)
        {
            _latihanService = latihanService;
            _rekomendasiTypeService = rekomendasiTypeService;
            _rekomendasiLatihanModel = RekomendasiLatihanModel;
            _portService = portService;
        }

        public async Task<JsonResult> GetAll(string port)
        {
            List<RekomendasiLatihanModel> data = await _rekomendasiLatihanModel.GetAll(port);

            int count = data.Count();

            return Json(new
            {
                success = true,
                recordsTotal = count,
                recordsFiltered = count,
                data
            });
        }

        public async Task<IActionResult> Index(string port)
        {
            List<Port> portList = await GetAllPort();
            ViewBag.PortList = portList;

            if (!string.IsNullOrEmpty(port))
            {
                ViewBag.SelectedPort = portList.Where(b => b.Name == port).FirstOrDefault();
            }
            else
            {
                ViewBag.SelectedPort = portList.OrderBy(b => b.Id).FirstOrDefault();
            }

            return View(INDEX);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id, string port)
        {
            ViewBag.LatihanList = await _latihanService.GetAll();
            ViewBag.RekomendasiTypeList = await GetAllRekomendasiType();
            RekomendasiLatihanModel model = new RekomendasiLatihanModel();

            model.Port = port;
            if (id > 0)
            {
                model = await _rekomendasiLatihanModel.GetById(id);
            }

            return PartialView(ADD_EDIT, model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(RekomendasiLatihanModel model)
        {
            var r = await _rekomendasiLatihanModel.AddEdit(model);

            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            }

            return Ok(new JsonResponse());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRekomendasILatihan(int id)
        {
            var r = await _rekomendasiLatihanModel.Delete(id);

            return Ok(new JsonResponse());
        }
    }
}
