using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMNI.Utilities.Constants;
using OMNI.Web.Controllers.Base;
using OMNI.Web.Models.Master;
using OMNI.Web.Services.CorePTK.Interface;
using OMNI.Web.Services.Master.Interface;

namespace OMNI.Web.Controllers.Master
{
    [AllowAnonymous]
    public class SpesifikasiJenisController : SpesifikasiJenisBaseController
    {
        private static readonly string INDEX = "~/Views/Master/SpesifikasiJenis/Index.cshtml";
        private static readonly string ADD_EDIT = "~/Views/Master/SpesifikasiJenis/AddEdit.cshtml";

        public SpesifikasiJenisController(IPort portService, IPeralatanOSR peralatanOSRService, ISpesifikasiJenis spesifikasiJenisService, IDetailSpesifikasi detailSpesifikasiService) : base(portService, peralatanOSRService, spesifikasiJenisService, detailSpesifikasiService)
        {
            _portService = portService;
            _peralatanOSRService = peralatanOSRService;
            _spesifikasiJenisService = spesifikasiJenisService;
            _detailSpesifikasiService = detailSpesifikasiService;
        }
        public IActionResult Index()
        {
            ViewBag.PortList = _portService.GetAllWithFilter(b => b.IsDeleted == GeneralConstants.NO);
            return View(INDEX);
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            return GetAllSpesifikasiJenis();
        }

        [HttpGet]
        public IActionResult AddEdit(int id)
        {
            ViewBag.PeralatanOSRList = _peralatanOSRService.GetAllWithFilter(b => b.IsDeleted == GeneralConstants.NO);
            SpesifikasiJenisModel model = GetSpesifikasiJenisById(id);
            return PartialView(ADD_EDIT, model);
        }

        [HttpPost]
        public JsonResult AddEdit(SpesifikasiJenisModel model)
        {
            return AddEditSpesifikasiJenisFunction(model);
        }

        [HttpPost]
        public IActionResult DeleteSpesifikasiJenis(int id)
        {
            return DeleteSpesifikasiJenisFunction(id);
        }
    }
}
