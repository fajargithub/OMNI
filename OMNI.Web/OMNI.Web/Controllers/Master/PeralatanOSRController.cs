using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMNI.Web.Controllers.Base;
using OMNI.Web.Models.Master;
using OMNI.Web.Services.Master.Interface;
using System.Net.Http;
using System.Threading.Tasks;

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
        public Task<JsonResult> GetAllPeralatan()
        {
            return GetAll();
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            PeralatanOSRModel model = await GetDataById(id);
            return PartialView(ADD_EDIT, model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(PeralatanOSRModel model)
        {
            return await AddEditFunction(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePeralatanOSR(int id)
        {
            return await DeleteFunction(id);
        }
    }
}
