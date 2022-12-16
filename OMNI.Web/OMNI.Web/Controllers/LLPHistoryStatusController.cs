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
    [CheckRole(Roles = GeneralConstants.OSMOSYS_SUPER_ADMIN + "," + GeneralConstants.OSMOSYS_MANAGEMENT + "," + GeneralConstants.OSMOSYS_ADMIN_LOKASI + "," + GeneralConstants.OSMOSYS_ADMIN_REGION1 + "," + GeneralConstants.OSMOSYS_ADMIN_REGION2 + "," + GeneralConstants.OSMOSYS_ADMIN_REGION3 + "," + GeneralConstants.OSMOSYS_GUEST_LOKASI + "," + GeneralConstants.OSMOSYS_GUEST_NON_LOKASI)]
    public class LLPHistoryStatusController : OMNIBaseController
    {
        private readonly ILogger<LLPHistoryStatusController> _logger;
        private static readonly string INDEX_FILE = "~/Views/LLPHistoryStatus/Index.cshtml";

        protected ILLPTrx _llpTrxService;
        protected ILLPHistoryStatus _llpHistoryStatusService;
        public LLPHistoryStatusController(ILogger<LLPHistoryStatusController> logger, IAdminLocation adminLocationService, ILLPHistoryStatus llpHistoryStatusService, ILLPTrx llpTrxService, IGuestLocation guestLocationService, IRekomendasiType rekomendasiTypeService, IPort portService, IPeralatanOSR peralatanOSRService, IJenis jenisService) : base(adminLocationService, guestLocationService, rekomendasiTypeService, portService, peralatanOSRService, jenisService)
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

            ViewBag.SelectedPort = "";
            ViewBag.YearList = GetYearList(2010, 2030);

            if (year > 0)
            {
                ViewBag.SelectedYear = year;
                SetSelectedYear(year);
            }
            else
            {
                int? getSelectedYear = GetSelectedYear();
                if (getSelectedYear.HasValue)
                {
                    ViewBag.SelectedYear = getSelectedYear.Value;
                }
                else
                {
                    ViewBag.SelectedYear = thisYear;
                }
            }

            List<Port> portList = await GetPorts();

            ViewBag.PortList = portList;
            ViewBag.RegionTxt = GetRegionTxt();

            if (!string.IsNullOrEmpty(port))
            {
                ViewBag.SelectedPort = port;
                SetSelectedPort(port);
            }
            else
            {
                string getSelectedPort = GetSelectedPort();
                if (!string.IsNullOrEmpty(getSelectedPort))
                {
                    ViewBag.SelectedPort = getSelectedPort;
                }
                else
                {
                    if (portList.Count() > 0)
                    {
                        ViewBag.SelectedPort = portList[0].Id;
                        SetSelectedPort(portList[0].Id.ToString());
                    }

                }
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
