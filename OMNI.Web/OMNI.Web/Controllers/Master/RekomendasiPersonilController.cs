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
    public class RekomendasiPersonilController : OMNIBaseController
    {
        private static readonly string INDEX = "~/Views/Master/RekomendasiPersonil/Index.cshtml";
        private static readonly string ADD_EDIT = "~/Views/Master/RekomendasiPersonil/AddEdit.cshtml";

        protected IRekomendasiPersonil _rekomendasiPersonilService;
        protected IPersonil _personilService;
        public RekomendasiPersonilController(IPersonil personilService, IRekomendasiType rekomendasiTypeService, IRekomendasiPersonil RekomendasiPersonilService, ISpesifikasiJenis spesifikasiJenisService, IPort portService, IPeralatanOSR peralatanOSRService, IJenis jenisService) : base(rekomendasiTypeService, portService, peralatanOSRService, jenisService)
        {
            _personilService = personilService;
            _rekomendasiTypeService = rekomendasiTypeService;
            _rekomendasiPersonilService = RekomendasiPersonilService;
            _portService = portService;
        }

        public async Task<JsonResult> GetAll(string port, int year)
        {
            List<RekomendasiPersonilModel> data = await _rekomendasiPersonilService.GetAll(port, year);

            int count = data.Count();

            return Json(new
            {
                success = true,
                recordsTotal = count,
                recordsFiltered = count,
                data
            });
        }

        public async Task<IActionResult> Index(string port, int year)
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

            return View(INDEX);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id, string port, int year)
        {
            ViewBag.PersonilList = await _personilService.GetAll();
            //ViewBag.RekomendasiTypeList = await GetAllRekomendasiType();
            var rekomendasiTypeList = await GetAllRekomendasiType();
            ViewBag.RekomendasiTypeList = rekomendasiTypeList.FindAll(b => b.Id == 1).ToList();
            ViewBag.Year = year;
            RekomendasiPersonilModel model = new RekomendasiPersonilModel();

            model.Port = port;
            if (id > 0)
            {
                model = await _rekomendasiPersonilService.GetById(id);
            }

            return PartialView(ADD_EDIT, model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(RekomendasiPersonilModel model)
        {
            var r = await _rekomendasiPersonilService.AddEdit(model);

            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            }

            return Ok(new JsonResponse());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRekomendasiPersonil(int id)
        {
            var r = await _rekomendasiPersonilService.Delete(id);

            return Ok(new JsonResponse());
        }
    }
}
