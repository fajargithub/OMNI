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
    public class QRCodeDetailController : OMNIBaseController
    {
        private static readonly string QR_CODE_DETAIL = "~/Views/Home/QRCodeDetail.cshtml";

        protected ILLPTrx _llpTrxService;

        public QRCodeDetailController(ILLPTrx llpTrxService, IAdminLocation adminLocationService, IGuestLocation guestLocationService, IJenis jenisService, IRekomendasiType rekomendasiTypeService, IPort portService, IPeralatanOSR peralatanOSRService) : base(adminLocationService, guestLocationService, rekomendasiTypeService, portService, peralatanOSRService, jenisService)
        {
            _jenisService = jenisService;
            _llpTrxService = llpTrxService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            List<FilesModel> fileList = await _llpTrxService.GetQRCodeFiles(id, "OMNI_LLP");
            LLPTrxModel data = await _llpTrxService.GetById(id);
            ViewBag.Data = data;
            ViewBag.FileList = fileList;
            return PartialView(QR_CODE_DETAIL);
        }

        [HttpGet]
        public async Task<IActionResult> ViewFile(int id, string fileName, string flag)
        {
            var r = await _llpTrxService.ReadFile(fileName, flag);
            var file = await _llpTrxService.GetFileData(id);

            return File(r, @"" + file.ContentType, file.FileName);
        }
    }
}
