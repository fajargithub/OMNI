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

namespace OMNI.Web.Controllers
{
    [AllowAnonymous]
    public class PublicController : BaseController
    {
        private static readonly string PUBLIC_LLPTRX_FILE_URL = "~/Views/PublicLLPTRXFile.cshtml";

        protected ILLPTrx _llpTrxService;
        protected IPort _portService;
        public PublicController(ILLPTrx llpTrxService, IPort portService) : base()
        {
            _llpTrxService = llpTrxService;
            _portService = portService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id, string port, string year)
        {
            ViewBag.Id = id;
            List<FilesModel> fileList = await _llpTrxService.GetPublicFiles(id, "OMNI_LLP");
            LLPTrxModel data = await _llpTrxService.GetById(id);
            var getPort = await _portService.GetById(int.Parse(port));
            ViewBag.Port = getPort.Name;
            ViewBag.Year = year;
            ViewBag.FileList = fileList;
            ViewBag.PeralatanOSR = data.PeralatanOSRName;
            ViewBag.Jenis = data.JenisName;
            
            //LLPHistoryStatusModel llpHistory = new LLPHistoryStatusModel();
            //ViewBag.LLPHistory = llpHistory;

            //var findLLPHistory = await _llpHistoryStatusService.GetLastHistoryByTrxId(id);

            //if(findLLPHistory != null)
            //{
            //    ViewBag.LLPHistory = findLLPHistory;
            //}

            //ViewBag.Data = data;
            //ViewBag.FileList = fileList;
            
            return PartialView(PUBLIC_LLPTRX_FILE_URL);
        }

        [HttpGet]
        public async Task<IActionResult> ViewFile(int id, string fileName, string flag)
        {
            var r = await _llpTrxService.ReadFile(id, fileName, flag);
            var file = await _llpTrxService.GetFileData(id);

            return File(r, @"" + file.ContentType, file.FileName);
        }
    }
}
