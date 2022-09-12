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
    public class RekomendasiJenisController : OMNIBaseController
    {
        private static readonly string INDEX = "~/Views/Master/RekomendasiJenis/Index.cshtml";
        private static readonly string ADD_EDIT = "~/Views/Master/RekomendasiJenis/AddEdit.cshtml";

        protected ISpesifikasiJenis _spesifikasiJenisService;
        protected IRekomendasiJenis _rekomendasiJenisService;
        public RekomendasiJenisController(IRekomendasiType rekomendasiTypeService, IRekomendasiJenis rekomendasiJenisService, ISpesifikasiJenis spesifikasiJenisService, IPort portService, IPeralatanOSR peralatanOSRService, IJenis jenisService) : base(rekomendasiTypeService, portService, peralatanOSRService, jenisService)
        {
            _rekomendasiTypeService = rekomendasiTypeService;
            _rekomendasiJenisService = rekomendasiJenisService;
            _peralatanOSRService = peralatanOSRService;
            _spesifikasiJenisService = spesifikasiJenisService;
            _portService = portService;
        }

        public async Task<JsonResult> GetAll(string port, string typeId, int year)
        {
            List<RekomendasiJenisModel> data = await _rekomendasiJenisService.GetAll(port, typeId, year);

            int count = data.Count();

            return Json(new
            {
                success = true,
                recordsTotal = count,
                recordsFiltered = count,
                data
            });
        }

        public async Task<IActionResult> Index(string port, int typeId, int year)
        {
            List<Port> portList = await GetAllPort();
            ViewBag.PortList = portList;

            var thisYear = DateTime.Now.Year;

            ViewBag.YearList = GetYearList(2010, 2030);

            ViewBag.ThisYear = thisYear;
            if (year > 0)
            {
                ViewBag.ThisYear = year;
            }

            if (!string.IsNullOrEmpty(port))
            {
                ViewBag.SelectedPort = portList.Where(b => b.Name == port).FirstOrDefault();
            }
            else
            {
                ViewBag.SelectedPort = portList.OrderBy(b => b.Id).FirstOrDefault();
            }

            List<RekomendasiType> rekomendasiTypeList = await _rekomendasiTypeService.GetAll();
            ViewBag.RekomendasiTypeList = rekomendasiTypeList;

            if (typeId > 0)
            {
                ViewBag.SelectedRekomendasiType = rekomendasiTypeList.Where(b => b.Id == typeId).FirstOrDefault();
            }
            else
            {
                ViewBag.SelectedRekomendasiType = rekomendasiTypeList.OrderBy(b => b.Id).FirstOrDefault();
            }

            return View(INDEX);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id, string port, string typeId, int year)
        {
            RekomendasiJenisModel model = new RekomendasiJenisModel();
            ViewBag.Year = year;
            model.Port = port;
            if (id > 0)
            {
                model = await _rekomendasiJenisService.GetById(id.ToString(), port, typeId, year);
                ViewBag.JenisId = model.Jenis;
            }

            return PartialView(ADD_EDIT, model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(RekomendasiJenisModel model)
        {
            var r = await _rekomendasiJenisService.AddEdit(model);

            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            }

            return Ok(new JsonResponse());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateValue(int id, string port, int typeId, decimal value, int year)
        {
            var r = await _rekomendasiJenisService.UpdateValue(id, port, typeId, value, year);

            return Ok(new JsonResponse());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRekomendasiJenis(int id)
        {
            var r = await _rekomendasiJenisService.Delete(id);

            return Ok(new JsonResponse());
        }
    }
}
