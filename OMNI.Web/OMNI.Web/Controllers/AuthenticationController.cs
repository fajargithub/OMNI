using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMNI.Web.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers
{
    [AllowAnonymous]
    [Layout("~/Views/Shared/Authentication/_Layout.cshtml")]
    public class AuthenticationController : BaseController
    {
        private static readonly string HOME_INDEX = "~/Views/Home/Index.cshtml";
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View(HOME_INDEX);
        }
    }
}
