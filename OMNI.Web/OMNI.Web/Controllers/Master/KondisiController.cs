using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMNI.Utilities.Constants;
using OMNI.Web.Data.Dao;
using OMNI.Web.Data.Dao.CorePTK;
using OMNI.Web.Models;
using OMNI.Web.Models.Master;
using OMNI.Web.Services.CorePTK.Interface;
using OMNI.Web.Services.Master.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers.Master
{
    [AllowAnonymous]
    public class KondisiController : BaseController
    {
        private static readonly string INDEX = "~/Views/Master/Kondisi/Index.cshtml";
        private static readonly string ADD_EDIT = "~/Views/Master/Kondisi/AddEdit.cshtml";

        protected IKondisi _kondisiService;
        public KondisiController(IKondisi KondisiService) : base()
        {
            _kondisiService = KondisiService;
        }

        public async Task<JsonResult> GetAll()
        {
            List<Kondisi> data = await _kondisiService.GetAll();

            int count = data.Count();

            return Json(new
            {
                success = true,
                recordsTotal = count,
                recordsFiltered = count,
                data
            });
        }

        public async Task<IActionResult> Index()
        {
            return View(INDEX);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id, int portId)
        {
            KondisiModel model = new KondisiModel();

            if (id > 0)
            {
                Kondisi data = await _kondisiService.GetById(id);
                if (data != null)
                {
                    model.Id = data.Id;
                    model.Name = data.Name;
                    model.Desc = data.Desc;
                }
            }

            return PartialView(ADD_EDIT, model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(KondisiModel model)
        {
            var r = await _kondisiService.AddEdit(model);

            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            }

            return Ok(new JsonResponse());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteKondisi(int id)
        {
            var r = await _kondisiService.Delete(id);

            return Ok(new JsonResponse());
        }
    }
}
