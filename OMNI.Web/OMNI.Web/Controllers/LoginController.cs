﻿using Microsoft.AspNetCore.Authorization;
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

        protected ILogin _loginService;

        public LoginController(ILogger<LoginController> logger, ILogin loginService, IRekomendasiType rekomendasiTypeService, IPort portService, IPeralatanOSR peralatanOSRService, IJenis jenisService) : base(rekomendasiTypeService, portService, peralatanOSRService, jenisService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return PartialView(LOGIN_URL);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginModel model)
        {
            var r = await _loginService.SignIn(model);

            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            } else
            {
                return Ok(new JsonResponse());
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return PartialView(LOGIN_URL);
        }
    }
}
