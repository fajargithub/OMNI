using Microsoft.AspNetCore.Mvc;
using OMNI.Domain.AppLogRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace OMNI.API.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IAppLogRepo _appLog;

        public BaseController(IAppLogRepo appLog)
        {
            _appLog = appLog;
        }

        //public void DecodeToken(string token)
        //{
        //    var handler = new JwtSecurityTokenHandler().ValidateToken(token,new TokenValidationParameters {
        //    IssuerSigningKey = 
        //    });
        //}
        protected string GetCurrentMethod([CallerMemberName] string methodName = "")
            => methodName.ToUpper();

        //protected async Task SaveAppLog(string methodName, string trxId, string status, CancellationToken cancellationToken, string remark = null, string errorMessage = null, string info = null)
        //    => await _appLog.SaveAppLog(
        //        controllerName: ControllerContext.RouteData.Values["controller"].ToString().ToUpper(),
        //        methodName: methodName,
        //        userName: User?.Identity?.Name ?? null,
        //        trxId: trxId,
        //        status: status,
        //        cancellationToken: cancellationToken,
        //        remark: remark,
        //        errorMessage: errorMessage,
        //        info: info);

        //protected string GetUserName() => User.Claims.FirstOrDefault(b => b.Type == ClaimTypes.Name).Value;
        //protected string GetEmail() => User.Claims.FirstOrDefault(b => b.Type == ClaimTypes.Email).Value;

    }
}
