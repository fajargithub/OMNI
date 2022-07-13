using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMNI.Web.Controllers.Base;
using OMNI.Web.Services.Master.Interface;

namespace OMNI.Web.Controllers.Master
{
    [AllowAnonymous]
    public class PeralatanOSRController : PeralatanOSRBaseController
    {
        private static readonly string INDEX = "~/Views/Master/PeralatanOSR/Index.cshtml";
        private static readonly string ADD_EDIT = "~/Views/Master/PeralatanOSR/AddEdit.cshtml";

        public PeralatanOSRController(IPeralatanOSR peralatanOSRService) : base(peralatanOSRService)
        {
            _peralatanOSRService = peralatanOSRService;
        }
        public IActionResult Index()
        {
            return View(INDEX);
        }

        [HttpGet]
        public JsonResult GetAllPeralatan()
        {
            return GetAll();
        }

        [HttpGet("Manage")]
        [HttpGet("Manage/{id:int}")]
        public IActionResult AddEdit(int id)
        {
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
