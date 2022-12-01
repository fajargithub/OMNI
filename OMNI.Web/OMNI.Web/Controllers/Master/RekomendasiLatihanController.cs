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
    public class RekomendasiLatihanController : OMNIBaseController
    {
        private static readonly string INDEX = "~/Views/Master/RekomendasILatihan/Index.cshtml";
        private static readonly string ADD_EDIT = "~/Views/Master/RekomendasILatihan/AddEdit.cshtml";

        protected IRekomendasiLatihan _rekomendasiLatihanModel;
        protected ILatihan _latihanService;
        public RekomendasiLatihanController(IAdminLocation adminLocationService, IGuestLocation guestLocationService, ILatihan latihanService, IRekomendasiType rekomendasiTypeService, IRekomendasiLatihan RekomendasiLatihanModel, IPort portService, IPeralatanOSR peralatanOSRService, IJenis jenisService) : base(adminLocationService, guestLocationService, rekomendasiTypeService, portService, peralatanOSRService, jenisService)
        {
            _latihanService = latihanService;
            _rekomendasiTypeService = rekomendasiTypeService;
            _rekomendasiLatihanModel = RekomendasiLatihanModel;
            _portService = portService;
        }

        public async Task<JsonResult> GetAll(string port, int year)
        {
            List<RekomendasiLatihanModel> data = await _rekomendasiLatihanModel.GetAll(port, year);

            int count = data.Count();

            return Json(new
            {
                success = true,
                recordsTotal = count,
                recordsFiltered = count,
                data
            });
        }

        public async Task<IActionResult> Index(string port, int year, string role, int userId)
        {
            List<Port> portList = await GetPorts(role, userId);

            ViewBag.PortList = portList;
            ViewBag.RegionTxt = GetRegionTxt(role);

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
            var latihanList = await _latihanService.GetAll();
            var rekomendasiTypeList = await GetAllRekomendasiType();

            ViewBag.RekomendasiTypeList = rekomendasiTypeList.FindAll(b => b.Id == 1).ToList();
            ViewBag.Year = year;
            RekomendasiLatihanModel model = new RekomendasiLatihanModel();

            model.Port = port;
            if (id > 0)
            {
                model = await _rekomendasiLatihanModel.GetById(id);
                latihanList = latihanList.FindAll(b => b.Id == int.Parse(model.Latihan));
            }

            ViewBag.LatihanList = latihanList;

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
