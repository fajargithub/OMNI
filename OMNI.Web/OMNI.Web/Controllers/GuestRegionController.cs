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
    public class GuestRegionController : OMNIBaseController
    {
        private static readonly string INDEX = "~/Views/GuestRegion/Index.cshtml";

        IGuestRegion _guestRegionService;

        public GuestRegionController(IAdminLocation adminLocationService, IGuestRegion guestRegionService, IGuestLocation guestLocationService, IJenis jenisService, IRekomendasiType rekomendasiTypeService, IPort portService, IPeralatanOSR peralatanOSRService) : base(adminLocationService, guestLocationService, rekomendasiTypeService, portService, peralatanOSRService, jenisService)
        {
            _guestRegionService = guestRegionService;
        }

        public async Task<JsonResult> GetAll()
        {
            List<GuestRegionModel> data = await _guestRegionService.GetAll();

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
    }
}
