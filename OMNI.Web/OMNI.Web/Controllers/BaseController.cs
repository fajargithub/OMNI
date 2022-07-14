using Microsoft.AspNetCore.Mvc;
using OMNI.Utilities.Base;
using OMNI.Utilities.Constants;
using System.Linq;

namespace OMNI.Web.Controllers
{
    public class BaseController : Controller
    {
        protected string JWTSession()
            => User.Claims.FirstOrDefault(b => b.Type == SessionClaimsKey.JWT.ToString())?.Value;

        protected JsonResult JsonReturn(string status, string errorMsg = null, dynamic response = null)
        {
            return Json(new ReturnJsonModel()
            {
                Status = status,
                ErrorMsg = errorMsg,
                Response = response
            });
        }
    }
}
