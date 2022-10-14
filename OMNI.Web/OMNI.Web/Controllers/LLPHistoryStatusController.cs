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
    //[Authorize(Policy = "osmosys.user.read")]
    [AllowAnonymous]
    [CheckRole(GeneralConstants.OSMOSYS_SUPER_ADMIN + "," + GeneralConstants.OSMOSYS_MANAGEMENT + "," + GeneralConstants.OSMOSYS_ADMIN_LOKASI + "," + GeneralConstants.OSMOSYS_ADMIN_LOKASI + "," + GeneralConstants.OSMOSYS_ADMIN_REGION1 + "," + GeneralConstants.OSMOSYS_ADMIN_REGION2 + "," + GeneralConstants.OSMOSYS_ADMIN_REGION3 + "," + GeneralConstants.OSMOSYS_GUEST_LOKASI + "," + GeneralConstants.OSMOSYS_GUEST_NON_LOKASI)]
    public class LLPHistoryStatusController : OMNIBaseController
    {
        private readonly ILogger<LLPHistoryStatusController> _logger;
        private static readonly string INDEX_FILE = "~/Views/LLPHistoryStatus/Index.cshtml";

        protected ILLPTrx _llpTrxService;
        protected ILLPHistoryStatus _llpHistoryStatusService;
        public LLPHistoryStatusController(ILogger<LLPHistoryStatusController> logger, ILLPHistoryStatus llpHistoryStatusService, ILLPTrx llpTrxService, IGuestLocation guestLocationService, IRekomendasiType rekomendasiTypeService, IPort portService, IPeralatanOSR peralatanOSRService, IJenis jenisService) : base(guestLocationService, rekomendasiTypeService, portService, peralatanOSRService, jenisService)
        {
            _logger = logger;
            _llpTrxService = llpTrxService;
            _llpHistoryStatusService = llpHistoryStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string port, int year)
        {
            var dateNow = DateTime.Now;
            var thisYear = dateNow.Year;
            ViewBag.YearList = GetYearList(2010, 2030);
            ViewBag.ThisYear = thisYear;

            if (year > 0)
            {
                ViewBag.ThisYear = year;
            }
            else
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
                ViewBag.SelectedPort = findPort;
                port = findPort.Name;
            }

            return View(INDEX_FILE);
        }

        public async Task<JsonResult> GetAll(string port, int year)
        {
            List<LLPHistoryStatusModel> data = await _llpHistoryStatusService.GetAll(port, year);

            int count = data.Count();

            return Json(new
            {
                success = true,
                recordsTotal = count,
                recordsFiltered = count,
                data
            });
        }

        public async Task<JsonResult> GetById(int id)
        {
            var data = await _llpHistoryStatusService.GetById(id);

            return Json(new
            {
                data
            });
        }
    }
}
