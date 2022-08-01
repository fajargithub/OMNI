using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OMNI.Utilities.Constants;
using OMNI.Web.Data.Dao.CorePTK;
using OMNI.Web.Models;
using OMNI.Web.Models.Master;
using OMNI.Web.Services.CorePTK.Interface;
using OMNI.Web.Services.Master.Interface;
using OMNI.Web.Services.Trx.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : OMNIBaseController
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly string ADD_EDIT_LLPTRX = "~/Views/Home/AddEditLLPTrx.cshtml";
        private static readonly string ADD_EDIT_PERSONIL = "~/Views/Home/AddEditPersonil.cshtml";
        private static readonly string ADD_EDIT_LATIHAN = "~/Views/Home/AddEditLatihan.cshtml";

        private static readonly string INDEX_FILE = "~/Views/Home/IndexFile.cshtml";

        protected ILLPTrx _llpTrxService;
        protected IKondisi _kondisiService;
        protected ISpesifikasiJenis _spesifikasiJenisService;
        public HomeController(ILogger<HomeController> logger, ISpesifikasiJenis spesifikasiJenisService, IKondisi kondisiService, ILLPTrx llpTrxService, IRekomendasiType rekomendasiTypeService, IPort portService, IPeralatanOSR peralatanOSRService, IJenis jenisService) : base(rekomendasiTypeService, portService, peralatanOSRService, jenisService)
        {
            _logger = logger;
            _llpTrxService = llpTrxService;
            _kondisiService = kondisiService;
            _spesifikasiJenisService = spesifikasiJenisService;
        }

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

        public async Task<JsonResult> GetAllSpesifikasiJenisByPeralatanOSR(int id)
        {
            List<SpesifikasiJenisModel> data = await _spesifikasiJenisService.GetAllSpesifikasiJenisByPeralatanOSR(id);

            return Json(new
            {
                data
            });
        }

        [HttpGet]
        public async Task<IActionResult> AddEditLLPTrx(int id, string port)
        {
            ViewBag.PeralatanOSRList = await GetAllPeralatanOSR();
            ViewBag.KondisiList = await _kondisiService.GetAll();

            LLPTrxModel model = new LLPTrxModel();

            model.Port = port;
            if (id > 0)
            {
                model = await _llpTrxService.GetById(id);
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

        [HttpGet("ManageTrxPersonil")]
        [HttpGet("ManageTrxPersonil/{id:int}")]
        public IActionResult AddEditTrxPersonil(int id)
        {
            return PartialView(ADD_EDIT_PERSONIL);
        }

        [HttpPost("ManageTrxPersonil")]
        [HttpPost("ManageTrxPersonil/{id:int}")]
        public IActionResult AddEditTrxPersonil([FromBody] object account, [FromRoute] int id)
        {
            return Ok();
        }

        [HttpGet("ManageTrxLatihan")]
        [HttpGet("ManageTrxLatihan/{id:int}")]
        public IActionResult AddEditTrxLatihan(int id)
        {
            return PartialView(ADD_EDIT_LATIHAN);
        }

        [HttpPost("ManageTrxLatihan")]
        [HttpPost("ManageTrxLatihan/{id:int}")]
        public IActionResult AddEditTrxLatihan([FromBody] object account, [FromRoute] int id)
        {
            return Ok();
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
    }
}
