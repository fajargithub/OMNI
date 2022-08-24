using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMNI.Web.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers
{
    [Layout("~/Views/Shared/Authentication/_Layout.cshtml")]
    public class AuthenticationController : BaseController
    {
        //private static readonly string LOGIN_URL = "~/Views/Authentication/Login.cshtml";
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
