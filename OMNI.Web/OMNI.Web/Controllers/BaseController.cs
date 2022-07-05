using Microsoft.AspNetCore.Mvc;
using OMNI.Utilities.Constants;
using System.Linq;

namespace OMNI.Web.Controllers
{
    public class BaseController : Controller
    {
        protected string JWTSession()
            => User.Claims.FirstOrDefault(b => b.Type == SessionClaimsKey.JWT.ToString())?.Value;
    }
}
