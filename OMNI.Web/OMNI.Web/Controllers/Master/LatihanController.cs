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
    public class LatihanController : OMNIBaseController
    {
        private static readonly string INDEX = "~/Views/Master/Latihan/Index.cshtml";
        private static readonly string ADD_EDIT = "~/Views/Master/Latihan/AddEdit.cshtml";

        protected ILatihan _latihanService;
        public LatihanController(IAdminLocation adminLocationService, IGuestLocation guestLocationService, IRekomendasiType rekomendasiTypeService, ILatihan LatihanService, IPort portService, IPeralatanOSR peralatanOSRService, IJenis jenisService) : base(adminLocationService, guestLocationService, rekomendasiTypeService, portService, peralatanOSRService, jenisService)
        {
            _latihanService = LatihanService;
        }

        [HttpGet]
        public async Task<JsonResult> GetById(int id)
        {
            Latihan data = await _latihanService.GetById(id);

            return Json(new
            {
                data
            });
        }

        public async Task<JsonResult> GetAll()
        {
            List<Latihan> data = await _latihanService.GetAll();

            int count = data.Count();

            return Json(new
            {
                success = true,
                recordsTotal = count,
                recordsFiltered = count,
                data
            });
        }

        public IActionResult Index(int? portId)
        {
            return View(INDEX);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id, int portId)
        {
            LatihanModel model = new LatihanModel();
            // model.Port = portId.ToString();

            if (id > 0)
            {
                Latihan data = await _latihanService.GetById(id);
                if (data != null)
                {
                    model.Id = data.Id;
                    model.Name = data.Name;
                    model.Satuan = data.Satuan;
                    model.Desc = data.Desc;
                }
            }

            return PartialView(ADD_EDIT, model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(LatihanModel model)
        {
            var r = await _latihanService.AddEdit(model);

            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            }

            return Ok(new JsonResponse());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLatihan(int id)
        {
            var r = await _latihanService.Delete(id);

            return Ok(new JsonResponse());
        }
    }
}
