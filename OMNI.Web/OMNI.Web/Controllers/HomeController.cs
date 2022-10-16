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
    public class HomeController : OMNIBaseController
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly string ADD_EDIT_LLPTRX = "~/Views/Home/AddEditLLPTrx.cshtml";
        private static readonly string ADD_EDIT_PERSONIL = "~/Views/Home/AddEditPersonilTrx.cshtml";
        private static readonly string ADD_EDIT_LATIHAN = "~/Views/Home/AddEditLatihanTrx.cshtml";

        private static readonly string INDEX_FILE = "~/Views/Home/IndexFile.cshtml";
        private static readonly string ADD_EDIT_FILE = "~/Views/Home/AddEditFile.cshtml";

        private static readonly string QR_CODE_DETAIL = "~/Views/Home/QRCodeDetail.cshtml";

        private static readonly string ADD_EDIT_LLP_HISTORY_STATUS = "~/Views/Home/AddEditLLPHistoryStatus.cshtml";

        protected ILLPTrx _llpTrxService;
        protected IPersonilTrx _personilTrxService;
        protected IPersonil _personilService;
        protected IKondisi _kondisiService;
        protected ISpesifikasiJenis _spesifikasiJenisService;
        protected ILatihan _latihanService;
        protected ILatihanTrx _latihanTrxService;
        protected ILampiran _lampiranService;
        public HomeController(ILogger<HomeController> logger, IAdminLocation adminLocationService, IGuestLocation guestLocationService, ILampiran lampiranService, ILatihan latihanService, ILatihanTrx latihanTrxService, IPersonil personilService, IPersonilTrx personilTrxService, ISpesifikasiJenis spesifikasiJenisService, IKondisi kondisiService, ILLPTrx llpTrxService, IRekomendasiType rekomendasiTypeService, IPort portService, IPeralatanOSR peralatanOSRService, IJenis jenisService) : base(adminLocationService, guestLocationService, rekomendasiTypeService, portService, peralatanOSRService, jenisService)
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
        public async Task<IActionResult> Index(string port, int year)
        {
            var dateNow = DateTime.Now;
            var thisYear = dateNow.Year;
            ViewBag.YearList = GetYearList(2010, 2030);
            ViewBag.ThisYear = thisYear;
            ViewBag.Info = "* Surat Penilaian belum terupload pada sistem OSMOSYS, Mohon upload Surat Penilaian";

            if (year > 0)
            {
                ViewBag.ThisYear = year;
            } else
            {
                year = thisYear;
            }

            await GetPorts();
            ViewBag.PortList = PortData.PortList;
            ViewBag.RegionTxt = PortData.RegionTxt;

            if (!string.IsNullOrEmpty(port))
            {
                ViewBag.SelectedPort = PortData.PortList.Where(b => b.Name == port).FirstOrDefault();
            }
            else
            {
                var findPort = PortData.PortList.OrderBy(b => b.Id).FirstOrDefault();
                if(findPort != null)
                {
                    ViewBag.SelectedPort = findPort;
                    port = findPort.Name;
                }
            }

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

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEditLatihanTrx(LatihanTrxModel model)
        {
            var r = await _latihanTrxService.AddEdit(model);

            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            }

            return Ok(new JsonResponse());
        }

        public async Task<JsonResult> GetAllFiles(int trxId, string flag)
        {
            List<FilesModel> data = await _llpTrxService.GetAllFiles(trxId, flag);

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
        public async Task<IActionResult> ViewFile(int id, string flag)
        {
            var r = await _llpTrxService.ReadFile(id, flag);
            var contentType = await _llpTrxService.GetContentType(id);

            return File(r, @"" + contentType);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> QrCodeDetail(int id)
        {
            LLPTrxModel data = await _llpTrxService.GetById(id);
            ViewBag.Data = data;
            return PartialView(QR_CODE_DETAIL);
        }

        [HttpGet]
        public IActionResult IndexFile(int trxId, string flag)
        {
            ViewBag.TrxId = trxId;
            ViewBag.Flag = flag;
            return PartialView(INDEX_FILE);
        }

        [HttpGet]
        public IActionResult AddEditFile(int trxId, string flag)
        {
            ViewBag.TrxId = trxId;
            ViewBag.Flag = flag;

            return PartialView(ADD_EDIT_FILE);
        }

        [HttpPost]
        public async Task<IActionResult> AddEditFiles(FilesModel model)
        {
            var r = await _llpTrxService.AddEditFiles(model);

            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            }

            return Ok(new JsonResponse());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var r = await _llpTrxService.DeleteFile(id);

            return Ok(new JsonResponse());
        }

        #region LLPTRX REGION
        public async Task<JsonResult> GetAllLLPTrx(string port, int year)
        {
            List<LLPTrxModel> data = await _llpTrxService.GetAllLLPTrx(port, year);

            int count = data.Count();

            return Json(new
            {
                success = true,
                recordsTotal = count,
                recordsFiltered = count,
                data
            });
        }

        public async Task<JsonResult> GetAllSpesifikasiJenisByPeralatanOSR(int id)
        {
            List<SpesifikasiJenisModel> data = await _spesifikasiJenisService.GetAllSpesifikasiJenisByPeralatanOSR(id);

            return Json(new
            {
                data
            });
        }

        [HttpPost]
        public async Task<JsonResult> GetLastNoAsset(AssetDataModel data)
        {
            string NoAsset = await _llpTrxService.GetLastNoAsset(data);

            return Json(new
            {
                NoAsset
            });
        }

        [HttpGet]
        public async Task<JsonResult> GetLLPTrxById(int id)
        {
            LLPTrxModel data = await _llpTrxService.GetById(id);

            return Json(new
            {
                data
            });
        }

        [HttpGet]
        public async Task<IActionResult> AddEditLLPTrx(int id, string port, int year)
        {
            List<PeralatanOSR> peralatanOSRList = await GetAllPeralatanOSR();
            ViewBag.PeralatanOSRList = peralatanOSRList;
            ViewBag.KondisiList = await _kondisiService.GetAll();
            ViewBag.JenisId = 0;
            ViewBag.PeralatanOSRId = 0;
            ViewBag.YearList = GetYearList(2010, 2030);
            ViewBag.YearNow = DateTime.Now.Year;
            ViewBag.Year = "";
            ViewBag.NoAsset = "";
            ViewBag.SelectedYear = "";
            ViewBag.Id = id;

            var region = await _portService.GetPortRegion(port);
            ViewBag.Region = region;

            LLPTrxModel model = new LLPTrxModel();
            model.Port = port;
            model.Year = year;

            if (id > 0)
            {
                model = await _llpTrxService.GetById(id);
                ViewBag.JenisId = model.Jenis;
                ViewBag.PeralatanOSRId = model.PeralatanOSR;
                if (!string.IsNullOrEmpty(model.QRCodeText))
                {
                    var arr = model.QRCodeText.Split("-");
                    if (arr.Count() > 0)
                    {
                        ViewBag.QRCodeYear = arr[1];
                        ViewBag.NoAsset = arr[3];
                    }
                }
            }

            return PartialView(ADD_EDIT_LLPTRX, model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQRCode(QRCodeDataModel data)
        {
            var r = await _llpTrxService.UpdateQRCode(data);
            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            }
            return Ok(new JsonResponse());
        }

        [HttpPost]
        public async Task<IActionResult> AddEditLLPTrx(LLPTrxModel model)
        {
            var r = await _llpTrxService.AddEdit(model);
            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Id = r.Id, Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            }
            return Ok(new JsonResponse { Id = r.Id, Status = GeneralConstants.SUCCESS, ErrorMsg = r.ErrorMsg });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLLPTrx(int id)
        {
            var r = await _llpTrxService.Delete(id);
            return Ok(new JsonResponse());
        }

        [HttpGet("ManageFile")]
        [HttpGet("ManageFile/{id:int}")]
        public IActionResult IndexFile()
        {
            return PartialView(INDEX_FILE);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion

        #region PERSONIL REGION
        public async Task<JsonResult> GetAllPersonilTrx(string port, int year)
        {
            List<PersonilTrxModel> data = await _personilTrxService.GetAllPersonilTrx(port, year);

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
        public async Task<IActionResult> AddEditPersonilTrx(int id, string port, int year)
        {
            List<Personil> personilList = await _personilService.GetAll();
            ViewBag.PersonilList = personilList;
            ViewBag.Port = port;
            ViewBag.Year = year;

            PersonilTrxModel model = new PersonilTrxModel();
            model.Port = port;

            if (id > 0)
            {
                model = await _personilTrxService.GetById(id);
            }

            return PartialView(ADD_EDIT_PERSONIL, model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEditPersonilTrx(PersonilTrxModel model)
        {
            var r = await _personilTrxService.AddEdit(model);

            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            }

            return Ok(new JsonResponse());
        }

        public async Task<JsonResult> GetRekomendasiPersonilByPersonilId(int id, string port, int year)
        {
            RekomendasiPersonil data = await _personilTrxService.GetRekomendasiPersonilByPersonilId(id, port, year);

            return Json(new
            {
                data
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeletePersonilTrx(int id)
        {
            var r = await _personilTrxService.Delete(id);

            return Ok(new JsonResponse());
        }
        #endregion

        #region LATIHAN TRX REGION
        public async Task<JsonResult> GetAllLatihanTrx(string port, int year)
        {
            List<LatihanTrxModel> data = await _latihanTrxService.GetAllLatihanTrx(port, year);

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
        public async Task<IActionResult> AddEditLatihanTrx(int id, string port, int year)
        {
            List<Latihan> latihanList = await _latihanService.GetAll();
            ViewBag.LatihanList = latihanList;
            ViewBag.Port = port;
            ViewBag.Year = year;

            LatihanTrxModel model = new LatihanTrxModel();
            model.Port = port;
            model.Year = year;

            if (id > 0)
            {
                model = await _latihanTrxService.GetById(id);
            }

            return PartialView(ADD_EDIT_LATIHAN, model);
        }

        public async Task<JsonResult> GetRekomendasiLatihanByLatihanId(int id, string port, int year)
        {
            RekomendasiLatihan data = await _latihanTrxService.GetRekomendasiLatihanByLatihanId(id, port, year);

            return Json(new
            {
                data
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLatihanTrx(int id)
        {
            var r = await _latihanTrxService.Delete(id);

            return Ok(new JsonResponse());
        }
        #endregion

        #region
        [HttpGet]
        public async Task<IActionResult> AddEditLLPHistoryStatus(int llpTrxId, string port)
        {
            List<Port> portList = await GetAllPort();
            ViewBag.PortList = portList;

            LLPHistoryStatusModel model = new LLPHistoryStatusModel();
            LLPTrxModel llpTrx = await _llpTrxService.GetById(llpTrxId);
            model.LLPTrx = llpTrx.Id.ToString();
            model.PortFrom = llpTrx.Port;
            model.PeralatanOSR = llpTrx.PeralatanOSRName;
            model.Jenis = llpTrx.JenisName;

            return PartialView(ADD_EDIT_LLP_HISTORY_STATUS, model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEditLLPHistoryStatus(LLPHistoryStatusModel model)
        {
            var r = await _llpTrxService.AddEditLLPHistoryStatus(model);

            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            }

            return Ok(new JsonResponse());
        }
        #endregion
    }
}
