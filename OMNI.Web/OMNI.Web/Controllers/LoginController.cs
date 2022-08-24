using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers
{
    [AllowAnonymous]
    public class LoginController : OMNIBaseController
    {
        private readonly ILogger<LoginController> _logger;
        private static readonly string LOGIN_URL = "~/Views/Authentication/Login.cshtml";

        public LoginController(ILogger<LoginController> logger, IRekomendasiType rekomendasiTypeService, IPort portService, IPeralatanOSR peralatanOSRService, IJenis jenisService) : base(rekomendasiTypeService, portService, peralatanOSRService, jenisService)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(LOGIN_URL);
        }
    }
}
