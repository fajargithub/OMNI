using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OMNI.Web.Controllers.Master
{
    [AllowAnonymous]
    public class SpesifikasiJenisController : BaseController
    {
        private static readonly string INDEX = "~/Views/Master/SpesifikasiJenis/Index.cshtml";
        private static readonly string ADD_EDIT = "~/Views/Master/SpesifikasiJenis/AddEdit.cshtml";

        public IActionResult Index()
        {
            return View(INDEX);
        }

        [HttpPost("List")]
        public IActionResult GetAllAccount()
        {
            return Ok();
        }

        [HttpGet("ManageSpesifikasi")]
        [HttpGet("ManageSpesifikasi/{id:int}")]
        public IActionResult AddEdit(int id)
        {
            ViewBag.FormTitle = "Add & Edit Spesifikasi / Jenis";
            return PartialView(ADD_EDIT);
        }

        [HttpPost("ManageSpesifikasi")]
        [HttpPost("ManageSpesifikasi/{id:int}")]
        public IActionResult AddEdit([FromBody] object account, [FromRoute] int id)
        {
            return Ok();
        }
    }
}
