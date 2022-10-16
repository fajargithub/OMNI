using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMNI.Utilities.Constants;
using OMNI.Web.Data.Dao;
using OMNI.Web.Data.Dao.CorePTK;
using OMNI.Web.Models;
using OMNI.Web.Models.Master;
using OMNI.Web.Services.CorePTK.Interface;
using OMNI.Web.Services.Master.Interface;
using OMNI.Web.Services.Trx.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers.Master
{
    [AllowAnonymous]
    [CheckRole(GeneralConstants.OSMOSYS_SUPER_ADMIN + "," + GeneralConstants.OSMOSYS_MANAGEMENT + "," + GeneralConstants.OSMOSYS_ADMIN_LOKASI + "," + GeneralConstants.OSMOSYS_ADMIN_LOKASI + "," + GeneralConstants.OSMOSYS_ADMIN_REGION1 + "," + GeneralConstants.OSMOSYS_ADMIN_REGION2 + "," + GeneralConstants.OSMOSYS_ADMIN_REGION3 + "," + GeneralConstants.OSMOSYS_GUEST_LOKASI + "," + GeneralConstants.OSMOSYS_GUEST_NON_LOKASI)]
    public class SpesifikasiJenisController : OMNIBaseController
    {
        private static readonly string INDEX = "~/Views/Master/SpesifikasiJenis/Index.cshtml";
        private static readonly string ADD_EDIT = "~/Views/Master/SpesifikasiJenis/AddEdit.cshtml";

        protected ISpesifikasiJenis _spesifikasiJenisService;
        public SpesifikasiJenisController(IAdminLocation adminLocationService, IGuestLocation guestLocationService, IRekomendasiType rekomendasiTypeService, ISpesifikasiJenis spesifikasiJenisService, IPort portService, IPeralatanOSR peralatanOSRService, IJenis jenisService) : base(adminLocationService, guestLocationService, rekomendasiTypeService, portService, peralatanOSRService, jenisService)
        {
            _spesifikasiJenisService = spesifikasiJenisService;
        }

        public async Task<JsonResult> GetById(int id)
        {
            SpesifikasiJenisModel data = await _spesifikasiJenisService.GetById(id);

            string kodeInventory = "";
            if (data != null)
            {
                kodeInventory = data.KodeInventory;
            }

            return Json(new
            {
                kodeInventory
            });
        }

        public async Task<JsonResult> GetAll()
        {
            List<SpesifikasiJenisModel> data = await _spesifikasiJenisService.GetAll();

            int count = data.Count();

            return Json(new
            {
                success = true,
                recordsTotal = count,
                recordsFiltered = count,
                data
            });
        }

        public IActionResult Index(int? portId)
        {   
            return View(INDEX);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            ViewBag.PeralatanOSRList = await GetAllPeralatanOSR();
            ViewBag.JenisList = await GetAllJenis();
            SpesifikasiJenisModel model = new SpesifikasiJenisModel();

            if (id > 0)
            {
                model = await _spesifikasiJenisService.GetById(id);
            }

            return PartialView(ADD_EDIT, model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(SpesifikasiJenisModel model)
        {
            var r = await _spesifikasiJenisService.AddEdit(model);

            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            }

            return Ok(new JsonResponse());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSpesifikasiJenis(int id)
        {
            var r = await _spesifikasiJenisService.Delete(id);

            return Ok(new JsonResponse());
        }
    }
}
