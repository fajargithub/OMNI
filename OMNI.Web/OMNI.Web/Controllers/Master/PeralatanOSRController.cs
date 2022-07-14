using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMNI.Web.Controllers.Base;
using OMNI.Web.Models.Master;
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

        [HttpGet]
        public IActionResult AddEdit(int id)
        {
            PeralatanOSRModel model = GetDataById(id);
            return PartialView(ADD_EDIT, model);
        }

        [HttpPost]
        public JsonResult AddEdit(PeralatanOSRModel model)
        {
            return AddEditFunction(model);
        }

        [HttpPost]
        public IActionResult DeletePeralatanOSR(int id)
        {
            return DeleteFunction(id);
        }
    }
}
