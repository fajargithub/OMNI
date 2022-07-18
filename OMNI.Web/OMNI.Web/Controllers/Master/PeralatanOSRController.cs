using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMNI.Web.Controllers.Base;
using System.Net.Http;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers.Master
{
    [AllowAnonymous]
    public class PeralatanOSRController : PeralatanOSRBaseController
    {
        private static readonly string INDEX = "~/Views/Master/PeralatanOSR/Index.cshtml";
        private static readonly string ADD_EDIT = "~/Views/Master/PeralatanOSR/AddEdit.cshtml";


        public PeralatanOSRController(IHttpClientFactory http) : base(http)
        {
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

        //[HttpGet]
        //public IActionResult AddEdit(int id)
        //{
        //    PeralatanOSRModel model = GetDataById(id);
        //    return PartialView(ADD_EDIT, model);
        //}

        //[HttpPost]
        //public JsonResult AddEdit(PeralatanOSRModel model)
        //{
        //    return AddEditFunction(model);
        //}

        //[HttpPost]
        //public IActionResult DeletePeralatanOSR(int id)
        //{
        //    return DeleteFunction(id);
        //}
    }
}
