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
    [CheckRole(Roles = GeneralConstants.OSMOSYS_SUPER_ADMIN + "," + GeneralConstants.OSMOSYS_MANAGEMENT + "," + GeneralConstants.OSMOSYS_ADMIN_LOKASI + "," + GeneralConstants.OSMOSYS_ADMIN_REGION1 + "," + GeneralConstants.OSMOSYS_ADMIN_REGION2 + "," + GeneralConstants.OSMOSYS_ADMIN_REGION3)]
    public class RekomendasiPersonilController : OMNIBaseController
    {
        private static readonly string INDEX = "~/Views/Master/RekomendasiPersonil/Index.cshtml";
        private static readonly string ADD_EDIT = "~/Views/Master/RekomendasiPersonil/AddEdit.cshtml";

        protected IRekomendasiPersonil _rekomendasiPersonilService;
        protected IPersonil _personilService;
        public RekomendasiPersonilController(IAdminLocation adminLocationService, IGuestLocation guestLocationService, IPersonil personilService, IRekomendasiType rekomendasiTypeService, IRekomendasiPersonil RekomendasiPersonilService, IPort portService, IPeralatanOSR peralatanOSRService, IJenis jenisService) : base(adminLocationService, guestLocationService, rekomendasiTypeService, portService, peralatanOSRService, jenisService)
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
            var dateNow = DateTime.Now;
            var thisYear = dateNow.Year;

            ViewBag.SelectedPort = "";
            ViewBag.YearList = GetYearList(2010, 2030);

            if (year > 0)
            {
                ViewBag.SelectedYear = year;
                SetSelectedYear(year);
            }
            else
            {
                int? getSelectedYear = GetSelectedYear();
                if (getSelectedYear.HasValue)
                {
                    ViewBag.SelectedYear = getSelectedYear.Value;
                }
                else
                {
                    ViewBag.SelectedYear = thisYear;
                }
            }

            List<Port> portList = await GetPorts();

            ViewBag.PortList = portList;
            ViewBag.RegionTxt = GetRegionTxt();

            if (!string.IsNullOrEmpty(port))
            {
                ViewBag.SelectedPort = port;
                SetSelectedPort(port);
            }
            else
            {
                string getSelectedPort = GetSelectedPort();
                if (!string.IsNullOrEmpty(getSelectedPort))
                {
                    ViewBag.SelectedPort = getSelectedPort;
                }
                else
                {
                    ViewBag.SelectedPort = "Senipah";
                    SetSelectedPort("Senipah");
                }
            }

            return View(INDEX);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id, string port, int year)
        {
            var personilList = await _personilService.GetAll();
            var rekomendasiTypeList = await GetAllRekomendasiType();

            ViewBag.RekomendasiTypeList = rekomendasiTypeList.FindAll(b => b.Id == 1).ToList();
            ViewBag.Year = year;
            RekomendasiPersonilModel model = new RekomendasiPersonilModel();

            model.Port = port;
            if (id > 0)
            {
                model = await _rekomendasiPersonilService.GetById(id);
                personilList = personilList.FindAll(b => b.Id == int.Parse(model.Personil));
            }

            ViewBag.PersonilList = personilList;

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
