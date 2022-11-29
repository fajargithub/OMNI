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
        //private readonly ILogger<LoginController> _logger;
        private static readonly string LOGIN_URL = "~/Views/Authentication/Login.cshtml";
        private static readonly string LOGOUT_URL = "~/Views/Authentication/Logout.cshtml";
        private static readonly string NO_ACCESS_URL = "~/Views/Authentication/NoAccess.cshtml";

        protected ILogin _loginService;

        public LoginController(ILogger<LoginController> logger, IAdminLocation adminLocationService, ILogin loginService, IGuestLocation guestLocationService, IRekomendasiType rekomendasiTypeService, IPort portService, IPeralatanOSR peralatanOSRService, IJenis jenisService) : base(adminLocationService, guestLocationService, rekomendasiTypeService, portService, peralatanOSRService, jenisService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return PartialView(LOGIN_URL);
        }

        [HttpGet]
        public IActionResult NoAccess()
        {
            return PartialView(NO_ACCESS_URL);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginModel model)
        {
            var r = await _loginService.SignIn(model);

            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            }
            else
            {
                ParamUser newUser = new ParamUser();
                newUser.IPAddress = GetIPAddress();
                newUser.UserId = r.UserId;
                newUser.Username = r.Username;
                newUser.RoleList = r.Roles;
                newUser.Email = r.Email;

                var getUserData = DataUser.UserList;

                if(getUserData != null)
                {
                    getUserData.Add(newUser);
                    DataUser.UserList = getUserData;
                } else
                {
                    List<ParamUser> newUserList = new List<ParamUser>();
                    newUserList.Add(newUser);
                    DataUser.UserList = newUserList;
                }

                //UserData.UserId = r.UserId;
                //UserData.RoleList = r.Roles;
                //UserData.Username = r.Username;
                //UserData.Email = r.Email;
                return Ok(new JsonResponse());
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            if(DataUser.UserList.Count() > 0)
            {
                var findUser = DataUser.UserList.RemoveAll(b => b.IPAddress.Contains(GetIPAddress()));
            }
            return PartialView(LOGOUT_URL);
        }
    }
}
