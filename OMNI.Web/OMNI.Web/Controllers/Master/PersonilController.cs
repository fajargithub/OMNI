using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OMNI.Web.Controllers.Master
{
    [AllowAnonymous]
    public class PersonilController : BaseController
    {
        private static readonly string INDEX = "~/Views/Master/Personil/Index.cshtml";
        private static readonly string ADD_EDIT = "~/Views/Master/Personil/AddEdit.cshtml";

        private static readonly string DETAIL_INDEX = "~/Views/Master/Personil/DetailIndex.cshtml";
        private static readonly string DETAIL_ADD_EDIT = "~/Views/Master/Personil/AddEditDetail.cshtml";

        public IActionResult Index()
        {
            return View(INDEX);
        }

        [HttpPost("List")]
        public IActionResult GetAllAccount()
        {
            return Ok();
        }

        [HttpGet("ManagePersonil")]
        [HttpGet("ManagePersonil/{id:int}")]
        public IActionResult AddEdit(int id)
        {
            return PartialView(ADD_EDIT);
        }

        [HttpPost("ManagePersonil")]
        [HttpPost("ManagePersonil/{id:int}")]
        public IActionResult AddEdit([FromBody] object account, [FromRoute] int id)
        {
            return Ok();
        }

        public IActionResult DetailIndex()
        {
            return View(DETAIL_INDEX);
        }

        [HttpGet("ManageDetailPersonil")]
        [HttpGet("ManageDetailPersonil/{id:int}")]
        public IActionResult DetailAddEdit(int id)
        {
            return PartialView(DETAIL_ADD_EDIT);
        }

        [HttpPost("ManageDetailPersonil")]
        [HttpPost("ManageDetailPersonil/{id:int}")]
        public IActionResult DetailAddEdit([FromBody] object account, [FromRoute] int id)
        {
            return Ok();
        }
    }
}
