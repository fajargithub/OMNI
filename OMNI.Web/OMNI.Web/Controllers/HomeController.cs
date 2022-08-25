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
    [Authorize(Policy = "osmosys.user.read")]
    public class HomeController : OMNIBaseController
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly string ADD_EDIT_LLPTRX = "~/Views/Home/AddEditLLPTrx.cshtml";
        private static readonly string ADD_EDIT_PERSONIL = "~/Views/Home/AddEditPersonilTrx.cshtml";
        private static readonly string ADD_EDIT_LATIHAN = "~/Views/Home/AddEditLatihanTrx.cshtml";

        private static readonly string INDEX_FILE = "~/Views/Home/IndexFile.cshtml";
        private static readonly string ADD_EDIT_FILE = "~/Views/Home/AddEditFile.cshtml";

        protected ILLPTrx _llpTrxService;
        protected IPersonilTrx _personilTrxService;
        protected IPersonil _personilService;
        protected IKondisi _kondisiService;
        protected ISpesifikasiJenis _spesifikasiJenisService;
        protected ILatihan _latihanService;
        protected ILatihanTrx _latihanTrxService;
        public HomeController(ILogger<HomeController> logger,ILatihan latihanService, ILatihanTrx latihanTrxService, IPersonil personilService, IPersonilTrx personilTrxService, ISpesifikasiJenis spesifikasiJenisService, IKondisi kondisiService, ILLPTrx llpTrxService, IRekomendasiType rekomendasiTypeService, IPort portService, IPeralatanOSR peralatanOSRService, IJenis jenisService) : base(rekomendasiTypeService, portService, peralatanOSRService, jenisService)
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
        }

        [HttpGet]
        public async Task<IActionResult> Index(string port)
        {
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
        public async Task<JsonResult> GetAllLLPTrx(string port)
        {
            List<LLPTrxModel> data = await _llpTrxService.GetAllLLPTrx(port);

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

        [HttpGet]
        public async Task<JsonResult> GetLLPTrxById(int id)
        {
            LLPTrxModel data = await _llpTrxService.GetById(id);

            return Json(new
            {
                data
            });
        }

        public class yearData
        {
            public int Value { get; set; }
            public int Name { get; set; }
        }

        public List<yearData> GetYearList(int startYear, int endYear)
        {
            List<yearData> yearList = new List<yearData>();
            int yearRange = endYear - startYear;
            if (yearRange > 0)
            {
                for (int i = 0; i <= yearRange; i++)
                {
                    yearData temp = new yearData();
                    temp.Value = startYear;
                    temp.Name = startYear;
                    yearList.Add(temp);

                    startYear += 1;
                }
            }

            return yearList;
        }

        [HttpGet]
        public async Task<IActionResult> AddEditLLPTrx(int id, string port)
        {
            List<PeralatanOSR> peralatanOSRList = await GetAllPeralatanOSR();
            ViewBag.PeralatanOSRList = peralatanOSRList;
            ViewBag.KondisiList = await _kondisiService.GetAll();
            ViewBag.JenisId = 0;
            ViewBag.YearList = GetYearList(2010, 2030);
            ViewBag.YearNow = DateTime.Now.Year;
            ViewBag.Year = "";
            ViewBag.NoAsset = "";

            var region = await _portService.GetPortRegion(port);
            ViewBag.Region = region;

            LLPTrxModel model = new LLPTrxModel();
            model.Port = port;

            if (id > 0)
            {
                model = await _llpTrxService.GetById(id);
                ViewBag.JenisId = model.Jenis;

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
        public async Task<IActionResult> AddEditLLPTrx(LLPTrxModel model)
        {
            var r = await _llpTrxService.AddEdit(model);

            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            }

            return Ok(new JsonResponse());
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
        public async Task<JsonResult> GetAllPersonilTrx(string port)
        {
            List<PersonilTrxModel> data = await _personilTrxService.GetAllPersonilTrx(port);

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
        public async Task<IActionResult> AddEditPersonilTrx(int id, string port)
        {
            List<Personil> personilList = await _personilService.GetAll();
            ViewBag.PersonilList = personilList;
            ViewBag.Port = port;

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

        public async Task<JsonResult> GetRekomendasiPersonilByPersonilId(int id, string port)
        {
            RekomendasiPersonil data = await _personilTrxService.GetRekomendasiPersonilByPersonilId(id, port);

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
        public async Task<JsonResult> GetAllLatihanTrx(string port)
        {
            List<LatihanTrxModel> data = await _latihanTrxService.GetAllLatihanTrx(port);

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
        public async Task<IActionResult> AddEditLatihanTrx(int id, string port)
        {
            List<Latihan> latihanList = await _latihanService.GetAll();
            ViewBag.LatihanList = latihanList;
            ViewBag.Port = port;

            LatihanTrxModel model = new LatihanTrxModel();
            model.Port = port;

            if (id > 0)
            {
                model = await _latihanTrxService.GetById(id);
            }

            return PartialView(ADD_EDIT_LATIHAN, model);
        }

        public async Task<JsonResult> GetRekomendasiLatihanByLatihanId(int id, string port)
        {
            RekomendasiLatihan data = await _latihanTrxService.GetRekomendasiLatihanByLatihanId(id, port);

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
    }
}
