using Microsoft.AspNetCore.Mvc;
using OMNI.Utility.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers
{
    public class BaseController : Controller
    {
        protected string JWTSession()
            => User.Claims.FirstOrDefault(b => b.Type == SessionClaimsKey.JWT.ToString())?.Value;
    }
}
