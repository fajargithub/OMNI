using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers
{
    [Authorize(Policy = "osmosys.user.read")]
    public class MainPageController : Controller
    {
        private static readonly string MAIN_PAGE_URL = "~/Views/MainPage.cshtml";
        public async Task<IActionResult> Index()
        {
            var userData = User.Claims.ToList();
            ViewBag.Claims = userData;
            string token = await HttpContext.GetTokenAsync("access_token");
            string email = User.Claims.FirstOrDefault(c => c.Type.Equals(Models.Oid.Email)).Value;
            return View(MAIN_PAGE_URL);
        }

        public async Task<IActionResult> Logout()
        {
            ICollection<string> myCookies = Request.Cookies.Keys;
            foreach (string cookie in myCookies)
                Response.Cookies.Delete(cookie);
            await HttpContext.SignOutAsync();
           SignOut("Cookies", "oidc");
            return RedirectToAction("Index", "MainPage");
        }
    }
}
