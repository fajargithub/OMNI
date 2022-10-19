using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OMNI.Utilities.Constants;
using OMNI.Web.Data.Dao;
using OMNI.Web.Data.Dao.CorePTK;
using OMNI.Web.Models;
using OMNI.Web.Models.Master;
using OMNI.Web.Services.CorePTK.Interface;
using OMNI.Web.Services.Master.Interface;
using OMNI.Web.Services.Trx.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers
{
    [AllowAnonymous]
    [CheckRole(Roles = GeneralConstants.OSMOSYS_SUPER_ADMIN + "," + GeneralConstants.OSMOSYS_MANAGEMENT + "," + GeneralConstants.OSMOSYS_ADMIN_LOKASI + "," + GeneralConstants.OSMOSYS_ADMIN_REGION1 + "," + GeneralConstants.OSMOSYS_ADMIN_REGION2 + "," + GeneralConstants.OSMOSYS_ADMIN_REGION3 + "," + GeneralConstants.OSMOSYS_GUEST_LOKASI + "," + GeneralConstants.OSMOSYS_GUEST_NON_LOKASI)]
    public class HistoryTrxController : OMNIBaseController
    {
        private readonly ILogger<HistoryTrxController> _logger;
        private static readonly string INDEX_HISTORY_LLPTRX = "~/Views/HistoryTrx/IndexHistoryLLPTrx.cshtml";
        private static readonly string INDEX_PERSONIL_TRX = "~/Views/HistoryTrx/IndexHistoryPersonilTrx.cshtml";

        protected ILLPTrx _llpTrxService;
        protected IKondisi _kondisiService;
        protected ISpesifikasiJenis _spesifikasiJenisService;
        protected IHistoryLLPTrx _historyLLPTrxService;
        protected IHistoryPersonilTrx _historyPersonilTrxService;
        public HistoryTrxController(ILogger<HistoryTrxController> logger, IHistoryPersonilTrx historyPersonilTrxService, IHistoryLLPTrx historyLLPTrxService, IAdminLocation adminLocationService, IGuestLocation guestLocationService, ILampiran lampiranService, ILatihan latihanService, ILatihanTrx latihanTrxService, IPersonil personilService, IPersonilTrx personilTrxService, ISpesifikasiJenis spesifikasiJenisService, IKondisi kondisiService, ILLPTrx llpTrxService, IRekomendasiType rekomendasiTypeService, IPort portService, IPeralatanOSR peralatanOSRService, IJenis jenisService) : base(adminLocationService, guestLocationService, rekomendasiTypeService, portService, peralatanOSRService, jenisService)
        {
            _logger = logger;
            _llpTrxService = llpTrxService;
            _kondisiService = kondisiService;
            _spesifikasiJenisService = spesifikasiJenisService;
            _portService = portService;
            _historyLLPTrxService = historyLLPTrxService;
            _historyPersonilTrxService = historyPersonilTrxService;
        }

        #region HISTORY LLP TRX
        [HttpGet]
        public async Task<JsonResult> GetHistoryLLPTrxById(int id)
        {
            HistoryLLPTrxModel data = await _historyLLPTrxService.GetHistoryLLPTrxById(id);

            return Json(new
            {
                data
            });
        }

        [HttpGet]
        public async Task<JsonResult> GetAllHistoryLLPTrx(int trxId, string port, int year)
        {
            List<HistoryLLPTrxModel> data = await _historyLLPTrxService.GetAllHistoryLLPTrx(trxId, port, year);

            int count = data.Count();

            return Json(new
            {
                success = true,
                recordsTotal = count,
                recordsFiltered = count,
                data
            });
        }

        [HttpGet]
        public IActionResult HistoryLLPTrx(int trxId, string port, int year)
        {
            var dateNow = DateTime.Now;
            var thisYear = dateNow.Year;
            ViewBag.YearList = GetYearList(2010, 2030);
            ViewBag.ThisYear = thisYear;

            ViewBag.Year = year;
            ViewBag.Port = port;
            ViewBag.TrxId = trxId;

            ViewBag.EnablePengesahan = false;
            ViewBag.EnableVerifikasi1 = false;
            ViewBag.EnableVerifikasi2 = false;

            return PartialView(INDEX_HISTORY_LLPTRX);
        }
        #endregion

        #region HISTORY PersonilTrx
        [HttpGet]
        public async Task<JsonResult> HistoryPersonilTrxById(int id)
        {
            HistoryLLPTrxModel data = await _historyLLPTrxService.GetHistoryLLPTrxById(id);

            return Json(new
            {
                data
            });
        }

        [HttpGet]
        public async Task<JsonResult> GetAllHistoryPersonilTrx(int trxId, string port, int year)
        {
            List<HistoryPersonilTrxModel> data = await _historyPersonilTrxService.GetAllHistoryPersonilTrx(trxId, port, year);

            int count = data.Count();

            return Json(new
            {
                success = true,
                recordsTotal = count,
                recordsFiltered = count,
                data
            });
        }

        [HttpGet]
        public IActionResult HistoryPersonilTrx(int trxId, string port, int year)
        {
            var dateNow = DateTime.Now;
            var thisYear = dateNow.Year;
            ViewBag.YearList = GetYearList(2010, 2030);
            ViewBag.ThisYear = thisYear;

            ViewBag.Year = year;
            ViewBag.Port = port;
            ViewBag.TrxId = trxId;

            ViewBag.EnablePengesahan = false;
            ViewBag.EnableVerifikasi1 = false;
            ViewBag.EnableVerifikasi2 = false;

            return PartialView(INDEX_PERSONIL_TRX);
        }
        #endregion

    }
}
