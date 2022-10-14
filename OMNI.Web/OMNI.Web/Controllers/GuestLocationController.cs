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
    [CheckRole(GeneralConstants.OSMOSYS_SUPER_ADMIN + "," + GeneralConstants.OSMOSYS_MANAGEMENT + "," + GeneralConstants.OSMOSYS_ADMIN_LOKASI + "," + GeneralConstants.OSMOSYS_ADMIN_REGION1 + "," + GeneralConstants.OSMOSYS_ADMIN_REGION2 + "," + GeneralConstants.OSMOSYS_ADMIN_REGION3 + "," + GeneralConstants.OSMOSYS_GUEST_LOKASI + "," + GeneralConstants.OSMOSYS_GUEST_NON_LOKASI)]
    public class GuestLocationController : OMNIBaseController
    {
        private static readonly string INDEX = "~/Views/GuestLocation/Index.cshtml";
        private static readonly string ADD_EDIT = "~/Views/GuestLocation/AddEdit.cshtml";

        public GuestLocationController(IGuestLocation guestLocationService, IJenis jenisService, IRekomendasiType rekomendasiTypeService, IPort portService, IPeralatanOSR peralatanOSRService) : base(guestLocationService, rekomendasiTypeService, portService, peralatanOSRService, jenisService)
        {
            _jenisService = jenisService;
        }

        public async Task<JsonResult> GetAll()
        {
            List<GuestLocationModel> data = await _guestLocationService.GetAll();

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
            GuestLocationModel model = new GuestLocationModel();
            ViewBag.PortList = await GetAllPort();
            ViewBag.PrimaryKey = 0;
            if (id > 0)
            {
                GuestLocationModel data = await _guestLocationService.GetByUserId(id);
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
        public async Task<IActionResult> AddEdit(GuestLocationModel model)
        {
            var r = await _guestLocationService.AddEdit(model);

            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            }

            return Ok(new JsonResponse());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteJenis(int id)
        {
            var r = await _guestLocationService.Delete(id);

            return Ok(new JsonResponse());
        }
    }
}
