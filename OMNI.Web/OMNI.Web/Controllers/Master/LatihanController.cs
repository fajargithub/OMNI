using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OMNI.Web.Controllers.Master
{
    [AllowAnonymous]
    public class LatihanController : BaseController
    {
        private static readonly string INDEX = "~/Views/Master/Latihan/Index.cshtml";
        private static readonly string ADD_EDIT = "~/Views/Master/Latihan/AddEdit.cshtml";

        private static readonly string DETAIL_INDEX = "~/Views/Master/Latihan/DetailIndex.cshtml";
        private static readonly string DETAIL_ADD_EDIT = "~/Views/Master/Latihan/AddEditDetail.cshtml";

        public IActionResult Index()
        {
            return View(INDEX);
        }

        [HttpPost("List")]
        public IActionResult GetAllAccount()
        {
            return Ok();
        }

        [HttpGet("ManageLatihan")]
        [HttpGet("ManageLatihan/{id:int}")]
        public IActionResult AddEdit(int id)
        {
            return PartialView(ADD_EDIT);
        }

        [HttpPost("ManageLatihan")]
        [HttpPost("ManageLatihan/{id:int}")]
        public IActionResult AddEdit([FromBody] object account, [FromRoute] int id)
        {
            return Ok();
        }

        public IActionResult DetailIndex()
        {
            return View(DETAIL_INDEX);
        }

        [HttpGet("ManageDetailLatihan")]
        [HttpGet("ManageDetailLatihan/{id:int}")]
        public IActionResult DetailAddEdit(int id)
        {
            return PartialView(DETAIL_ADD_EDIT);
        }

        [HttpPost("ManageDetailLatihan")]
        [HttpPost("ManageDetailLatihan/{id:int}")]
        public IActionResult DetailAddEdit([FromBody] object account, [FromRoute] int id)
        {
            return Ok();
        }
    }
}
