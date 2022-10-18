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

        protected ILLPTrx _llpTrxService;
        protected IPersonilTrx _personilTrxService;
        protected IPersonil _personilService;
        protected IKondisi _kondisiService;
        protected ISpesifikasiJenis _spesifikasiJenisService;
        protected ILatihan _latihanService;
        protected ILatihanTrx _latihanTrxService;
        protected ILampiran _lampiranService;
        public HistoryTrxController(ILogger<HistoryTrxController> logger, IAdminLocation adminLocationService, IGuestLocation guestLocationService, ILampiran lampiranService, ILatihan latihanService, ILatihanTrx latihanTrxService, IPersonil personilService, IPersonilTrx personilTrxService, ISpesifikasiJenis spesifikasiJenisService, IKondisi kondisiService, ILLPTrx llpTrxService, IRekomendasiType rekomendasiTypeService, IPort portService, IPeralatanOSR peralatanOSRService, IJenis jenisService) : base(adminLocationService, guestLocationService, rekomendasiTypeService, portService, peralatanOSRService, jenisService)
        {
            _logger = logger;
            _llpTrxService = llpTrxService;
            _kondisiService = kondisiService;
            _spesifikasiJenisService = spesifikasiJenisService;
            _portService = portService;
            _personilTrxService = personilTrxService;
            _personilService = personilService;
            _latihanService = latihanService;
            _latihanTrxService = latihanTrxService;
            _lampiranService = lampiranService;
        }

        [HttpGet]
        public async Task<IActionResult> HistoryLLPTrx(int trxId, string port, int year)
        {
            var dateNow = DateTime.Now;
            var thisYear = dateNow.Year;
            ViewBag.YearList = GetYearList(2010, 2030);
            ViewBag.ThisYear = thisYear;
            ViewBag.Info = "* Surat Penilaian belum terupload pada sistem OSMOSYS, Mohon upload Surat Penilaian";

            ViewBag.Year = year;
            ViewBag.Port = port;
            ViewBag.TrxId = trxId;

            ViewBag.EnablePengesahan = false;
            ViewBag.EnableVerifikasi1 = false;
            ViewBag.EnableVerifikasi2 = false;

            List<LampiranModel> lampiranList = await _lampiranService.GetAllByPort(port);
            if (lampiranList.Count() > 0)
            {
                var findPenilaian = lampiranList.FindAll(b => b.LampiranType == "PENILAIAN").OrderByDescending(b => b.Id).FirstOrDefault();
                if (findPenilaian != null)
                {
                    ViewBag.EnablePengesahan = true;
                    ViewBag.Info = "* Mohon upload Surat Pengesahan";
                    var findPengesahan = lampiranList.FindAll(b => b.LampiranType == "PENGESAHAN").OrderByDescending(b => b.Id).FirstOrDefault();
                    if (findPengesahan != null)
                    {
                        ViewBag.Info = "* Mohon upload Verifikasi Surat Perpanjangan Pengesahan (2,5 tahun pertama) per tanggal " + findPengesahan.EndDate;

                        var findVerifikasi1 = lampiranList.FindAll(b => b.LampiranType == "VERIFIKASI1").OrderByDescending(b => b.Id).FirstOrDefault();
                        if (findVerifikasi1 != null)
                        {
                            ViewBag.Info = "* Mohon upload Verifikasi Surat Perpanjangan Pengesahan (2,5 tahun kedua) per tanggal " + findVerifikasi1.EndDate;

                            var findVerifikasi2 = lampiranList.FindAll(b => b.LampiranType == "VERIFIKASI2").OrderByDescending(b => b.Id).FirstOrDefault();
                            if (findVerifikasi2 != null)
                            {
                                ViewBag.Info = "";
                            }
                        }
                    }
                }
            }

            return PartialView(INDEX_HISTORY_LLPTRX);
        }
    }
}
