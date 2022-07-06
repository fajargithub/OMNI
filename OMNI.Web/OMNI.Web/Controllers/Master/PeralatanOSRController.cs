using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OMNI.Web.Controllers.Master
{
    [AllowAnonymous]
    public class PeralatanOSRController : BaseController
    {
        private static readonly string INDEX = "~/Views/Master/PeralatanOSR/Index.cshtml";
        private static readonly string ADD_EDIT = "~/Views/Master/PeralatanOSR/AddEdit.cshtml";

        public IActionResult Index()
        {
            return View(INDEX);
        }

        [HttpPost("List")]
        public IActionResult GetAllAccount()
        {
            return Ok();
        }

        [HttpGet("Manage")]
        [HttpGet("Manage/{id:int}")]
        public IActionResult AddEdit(int id)
        {
            ViewBag.FormTitle = "Add / Edit Peralatan OSR";
            return PartialView(ADD_EDIT);
        }

        [HttpPost("Manage")]
        [HttpPost("Manage/{id:int}")]
        public IActionResult AddEdit([FromBody] object account, [FromRoute] int id)
        {
            return Ok();
        }
    }
}
