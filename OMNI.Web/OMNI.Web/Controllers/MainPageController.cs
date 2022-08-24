using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers
{
    public class MainPageController : Controller
    {
        private static readonly string MAIN_PAGE_URL = "~/Views/MainPage.cshtml";
        public IActionResult Index()
        {
            return View(MAIN_PAGE_URL);
        }
    }
}
