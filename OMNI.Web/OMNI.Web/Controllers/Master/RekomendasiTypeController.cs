using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMNI.Utilities.Constants;
using OMNI.Web.Data.Dao;
using OMNI.Web.Data.Dao.CorePTK;
using OMNI.Web.Models;
using OMNI.Web.Models.Master;
using OMNI.Web.Services.CorePTK.Interface;
using OMNI.Web.Services.Master.Interface;
using OMNI.Web.Services.Trx.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers.Master
{
    [AllowAnonymous]
    [CheckRole(Roles = GeneralConstants.OSMOSYS_SUPER_ADMIN + "," + GeneralConstants.OSMOSYS_MANAGEMENT + "," + GeneralConstants.OSMOSYS_ADMIN_LOKASI + "," + GeneralConstants.OSMOSYS_ADMIN_REGION1 + "," + GeneralConstants.OSMOSYS_ADMIN_REGION2 + "," + GeneralConstants.OSMOSYS_ADMIN_REGION3)]
    public class RekomendasiTypeController : OMNIBaseController
    {
        private static readonly string INDEX = "~/Views/Master/RekomendasiType/Index.cshtml";
        private static readonly string ADD_EDIT = "~/Views/Master/RekomendasiType/AddEdit.cshtml";

        protected IRekomendasiType _rekomendasiTypeService;
        public RekomendasiTypeController(IAdminLocation adminLocationService, IGuestLocation guestLocationService, IJenis jenisService, IRekomendasiType rekomendasiTypeService, IPort portService, IPeralatanOSR peralatanOSRService) : base(adminLocationService, guestLocationService, rekomendasiTypeService, portService, peralatanOSRService, jenisService)
        {
            _rekomendasiTypeService = rekomendasiTypeService;
        }

        public async Task<JsonResult> GetAll()
        {
            List<RekomendasiType> data = await _rekomendasiTypeService.GetAll();

            int count = data.Count();

            return Json(new
            {
                success = true,
                recordsTotal = count,
                recordsFiltered = count,
                data
            });
        }

        public IActionResult Index()
        {
            return View(INDEX);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id, int portId)
        {
            RekomendasiTypeModel model = new RekomendasiTypeModel();

            if (id > 0)
            {
                RekomendasiType data = await _rekomendasiTypeService.GetById(id);
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
        public async Task<IActionResult> AddEdit(RekomendasiTypeModel model)
        {
            var r = await _rekomendasiTypeService.AddEdit(model);

            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            }

            return Ok(new JsonResponse());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRekomendasiType(int id)
        {
            var r = await _rekomendasiTypeService.Delete(id);

            return Ok(new JsonResponse());
        }
    }
}
