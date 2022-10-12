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
    [CheckRole(GeneralConstants.OSMOSYS_SUPER_ADMIN + "," + GeneralConstants.OSMOSYS_MANAGEMENT + "," + GeneralConstants.OSMOSYS_ADMIN_LOKASI + "," + GeneralConstants.OSMOSYS_ADMIN_REGION + "," + GeneralConstants.OSMOSYS_GUEST_LOKASI + "," + GeneralConstants.OSMOSYS_GUEST_NON_LOKASI)]
    public class JenisController : OMNIBaseController
    {
        private static readonly string INDEX = "~/Views/Master/Jenis/Index.cshtml";
        private static readonly string ADD_EDIT = "~/Views/Master/Jenis/AddEdit.cshtml";

        protected IJenis _jenisService;
        public JenisController(IJenis jenisService, IRekomendasiType rekomendasiTypeService, IPort portService, IPeralatanOSR peralatanOSRService) : base(rekomendasiTypeService, portService, peralatanOSRService, jenisService)
        {
            _jenisService = jenisService;
        }

        public async Task<JsonResult> GetAll()
        {
            List<Jenis> data = await _jenisService.GetAll();

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
            JenisModel model = new JenisModel();

            if (id > 0)
            {
                Jenis data = await _jenisService.GetById(id);
                if (data != null)
                {
                    model.Id = data.Id;
                    model.Name = data.Name;
                    model.Satuan = data.Satuan;
                    model.Kode = data.Kode;
                    model.KodeInventory = data.KodeInventory;
                    model.Desc = data.Desc;
                }
            }

            return PartialView(ADD_EDIT, model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(JenisModel model)
        {
            var r = await _jenisService.AddEdit(model);

            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            }

            return Ok(new JsonResponse());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteJenis(int id)
        {
            var r = await _jenisService.Delete(id);

            return Ok(new JsonResponse());
        }
    }
}
