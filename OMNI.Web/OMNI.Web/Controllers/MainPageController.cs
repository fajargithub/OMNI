using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using OMNI.Utilities.Constants;
using OMNI.Web.Services.CorePTK.Interface;
using OMNI.Web.Services.Master.Interface;
using OMNI.Web.Services.Trx.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers
{
    [AllowAnonymous]
    [CheckRole(GeneralConstants.OSMOSYS_SUPER_ADMIN + "," + GeneralConstants.OSMOSYS_MANAGEMENT + "," + GeneralConstants.OSMOSYS_ADMIN_LOKASI + "," + GeneralConstants.OSMOSYS_ADMIN_LOKASI + "," + GeneralConstants.OSMOSYS_ADMIN_REGION1 + "," + GeneralConstants.OSMOSYS_ADMIN_REGION2 + "," + GeneralConstants.OSMOSYS_ADMIN_REGION3 + "," + GeneralConstants.OSMOSYS_GUEST_LOKASI + "," + GeneralConstants.OSMOSYS_GUEST_NON_LOKASI)]
    public class MainPageController : OMNIBaseController
    {
        private static readonly string MAIN_PAGE_URL = "~/Views/MainPage.cshtml";

        public MainPageController(IAdminLocation adminLocationService, IRekomendasiType rekomendasiTypeService, IGuestLocation guestLocationService, IPort portService, IPeralatanOSR peralatanOSRService, IJenis jenisService) : base(adminLocationService, guestLocationService, rekomendasiTypeService, portService, peralatanOSRService, jenisService)
        {
        }

        public IActionResult Index()
        {
            ViewBag.Username = UserData.Username;
            return View(MAIN_PAGE_URL);
        }

        public async Task<IActionResult> Logout()
        {
            ICollection<string> myCookies = Request.Cookies.Keys;
            foreach (string cookie in myCookies)
                Response.Cookies.Delete(cookie);
            await HttpContext.SignOutAsync();
           SignOut("Cookies", "oidc");
            return RedirectToAction("Logout", "Login");
        }
    }
}
