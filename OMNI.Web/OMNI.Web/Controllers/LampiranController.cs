using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMNI.Utilities.Constants;
using OMNI.Web.Data.Dao.CorePTK;
using OMNI.Web.Models;
using OMNI.Web.Models.Master;
using OMNI.Web.Services.CorePTK.Interface;
using OMNI.Web.Services.Master.Interface;
using OMNI.Web.Services.Trx.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers
{
    [AllowAnonymous]
    public class LampiranController : OMNIBaseController
    {
        private static readonly string INDEX_LAMPIRAN = "~/Views/Lampiran/IndexLampiran.cshtml";
        private static readonly string ADD_EDIT_LAMPIRAN = "~/Views/Lampiran/AddEditLampiran.cshtml";

        protected ILampiran _lampiranService;
        protected ILLPTrx _llpTrxService;
        public LampiranController(ILampiran lampiranService, ILLPTrx llptrxService, IPort portService, IRekomendasiType rekomendasiTypeService, IPeralatanOSR peralatanOSRService, IJenis jenisService) : base(rekomendasiTypeService, portService, peralatanOSRService, jenisService)
        {
            _lampiranService = lampiranService;
            _llpTrxService = llptrxService;
            _portService = portService;
        }

        public async Task<IActionResult> Index(string port)
        {
            var thisYear = DateTime.Now.Year;

            ViewBag.YearList = GetYearList(2010, 2030);

            ViewBag.ThisYear = thisYear;
            //if (year > 0)
            //{
            //    ViewBag.ThisYear = year;
            //}

            List<Port> portList = await GetAllPort();
            ViewBag.PortList = portList;

            if (!string.IsNullOrEmpty(port))
            {
                ViewBag.SelectedPort = portList.Where(b => b.Name == port).FirstOrDefault();
            }
            else
            {
                ViewBag.SelectedPort = portList.OrderBy(b => b.Id).FirstOrDefault();
            }

            return View(INDEX_LAMPIRAN);
        }

        public async Task<JsonResult> GetById(int id)
        {
            var data = await _lampiranService.GetById(id);

            return Json(new
            {
                data
            });
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id, string port, string lampiranType)
        {
            ViewBag.Id = id;

            var region = await _portService.GetPortRegion(port);
            ViewBag.Region = region;

            LampiranModel model = new LampiranModel();
            model.Port = port;
            model.LampiranType = lampiranType;

            if (id > 0)
            {
                model = await _lampiranService.GetById(id);
            }

            return PartialView(ADD_EDIT_LAMPIRAN, model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(LampiranModel model)
        {
            var r = await _lampiranService.AddEdit(model);
            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            }
            return Ok(new JsonResponse());
        }

        [HttpGet]
        public async Task<JsonResult> GetAllSuratPenilaian(string port)
        {
            List<LampiranModel> data = await _lampiranService.GetAllByPort(port);

            return Json(new
            {
                data
            });
        }

        [HttpGet]
        public async Task<IActionResult> ViewFile(int id, string flag)
        {
            var r = await _llpTrxService.ReadFile(id, flag);
            var contentType = await _llpTrxService.GetContentType(id);

            return File(r, @"" + contentType);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLampiran(int id)
        {
            var r = await _lampiranService.Delete(id);

            return Ok(new JsonResponse());
        }
    }
}
