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
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers
{
    [AllowAnonymous]
    [CheckRole(Roles = GeneralConstants.OSMOSYS_SUPER_ADMIN + "," + GeneralConstants.OSMOSYS_MANAGEMENT)]
    public class AdminLocationController : OMNIBaseController
    {
        private static readonly string INDEX = "~/Views/AdminLocation/Index.cshtml";
        private static readonly string INDEX_ADMIN_REGION = "~/Views/AdminLocation/IndexAdminRegion.cshtml";
        private static readonly string ADD_EDIT = "~/Views/AdminLocation/AddEdit.cshtml";

        public AdminLocationController(IAdminLocation adminLocationService, IGuestLocation guestLocationService, IJenis jenisService, IRekomendasiType rekomendasiTypeService, IPort portService, IPeralatanOSR peralatanOSRService) : base(adminLocationService, guestLocationService, rekomendasiTypeService, portService, peralatanOSRService, jenisService)
        {
            _jenisService = jenisService;
        }

        public async Task<JsonResult> GetAll()
        {
            List<AdminLocationModel> data = await _adminLocationService.GetAll();

            int count = data.Count();

            return Json(new
            {
                success = true,
                recordsTotal = count,
                recordsFiltered = count,
                data
            });
        }

        public IActionResult AdminRegionIndex()
        {
            return View(INDEX_ADMIN_REGION);
        }

        public async Task<JsonResult> GetAllAdminRegion()
        {
            List<AdminLocationModel> data = await _adminLocationService.GetAllAdminRegion();

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
        public async Task<IActionResult> AddEdit(int id)
        {
            AdminLocationModel model = new AdminLocationModel();
            ViewBag.PortList = await GetAllPort();
            ViewBag.PrimaryKey = 0;
            if (id > 0)
            {
                AdminLocationModel data = await _adminLocationService.GetByUserId(id);
                if (data != null)
                {
                    ViewBag.PrimaryKey = data.Id;
                    //model.Id = data.Id;
                    model.UserId = data.UserId;
                    model.PortList = data.PortList;
                }
            }

            return PartialView(ADD_EDIT, model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(AdminLocationModel model)
        {
            var r = await _adminLocationService.AddEdit(model);

            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            }

            return Ok(new JsonResponse());
        }
    }
}
