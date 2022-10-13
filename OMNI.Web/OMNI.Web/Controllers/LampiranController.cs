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
    [CheckRole(GeneralConstants.OSMOSYS_SUPER_ADMIN + "," + GeneralConstants.OSMOSYS_MANAGEMENT + "," + GeneralConstants.OSMOSYS_ADMIN_LOKASI + "," + GeneralConstants.OSMOSYS_ADMIN_LOKASI + "," + GeneralConstants.OSMOSYS_ADMIN_REGION1 + "," + GeneralConstants.OSMOSYS_ADMIN_REGION2 + "," + GeneralConstants.OSMOSYS_ADMIN_REGION3 + "," + GeneralConstants.OSMOSYS_GUEST_LOKASI + "," + GeneralConstants.OSMOSYS_GUEST_NON_LOKASI)]
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
            var dateNow = DateTime.Now;
            var thisYear = dateNow.Year;

            ViewBag.Info = "* Silahkan upload Surat Penilaian";

            ViewBag.YearList = GetYearList(2010, 2030);

            ViewBag.ThisYear = thisYear;

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

            ViewBag.EnablePengesahan = false;
            ViewBag.EnableVerifikasi1 = false;
            ViewBag.EnableVerifikasi2 = false;

            List<LampiranModel> lampiranList = await _lampiranService.GetAllByPort(port);
            if(lampiranList.Count() > 0)
            {
                var findPenilaian = lampiranList.FindAll(b => b.LampiranType == "PENILAIAN").OrderByDescending(b => b.Id).FirstOrDefault();
                if(findPenilaian != null)
                {
                    ViewBag.EnablePengesahan = true;
                    ViewBag.Info = "* Mohon upload Surat Pengesahan";
                    var findPengesahan = lampiranList.FindAll(b => b.LampiranType == "PENGESAHAN").OrderByDescending(b => b.Id).FirstOrDefault();
                    if(findPengesahan != null)
                    {
                        var endDatePengesahan = DateTime.ParseExact(findPengesahan.EndDate, "dd MMM yyyy", null);
                        if(dateNow >= endDatePengesahan)
                        {
                            ViewBag.EnableVerifikasi1 = true;
                        } 
                        else
                        {
                            ViewBag.EnableVerifikasi1 = false;
                        }

                        ViewBag.Info = "* Mohon upload Verifikasi Surat Perpanjangan Pengesahan (2,5 tahun pertama) per tanggal " + findPengesahan.EndDate;

                        var findVerifikasi1 = lampiranList.FindAll(b => b.LampiranType == "VERIFIKASI1").OrderByDescending(b => b.Id).FirstOrDefault();
                        if(findVerifikasi1 != null)
                        {
                            var endDateVerifikasi1 = DateTime.ParseExact(findVerifikasi1.EndDate, "dd MMM yyyy", null);
                            if (dateNow >= endDateVerifikasi1)
                            {
                                ViewBag.EnableVerifikasi2 = true;
                            }
                            else
                            {
                                ViewBag.EnableVerifikasi2 = false;
                            }

                            ViewBag.Info = "* Mohon upload Verifikasi Surat Perpanjangan Pengesahan (2,5 tahun kedua) per tanggal " + findVerifikasi1.EndDate;

                            var findVerifikasi2 = lampiranList.FindAll(b => b.LampiranType == "VERIFIKASI2").OrderByDescending(b => b.Id).FirstOrDefault();
                            if(findVerifikasi2 != null)
                            {
                                ViewBag.Info = "";
                            }
                        } else
                        {
                            ViewBag.EnableVerifikasi2 = false;
                        }
                    } else
                    {
                        ViewBag.EnableVerifikasi1 = false;
                        ViewBag.EnableVerifikasi2 = false;
                    }
                } else
                {
                    ViewBag.EnablePengesahan = false;
                    ViewBag.EnableVerifikasi1 = false;
                    ViewBag.EnableVerifikasi2 = false;
                }
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
            ViewBag.LampiranType = lampiranType;
            ViewBag.Title = "";

            if(lampiranType == "PENILAIAN")
            {
                ViewBag.Title = "Surat Penilaian";
            } else if(lampiranType == "PENGESAHAN")
            {
                ViewBag.Title = "Surat Pengesahan";
            } else if(lampiranType == "VERIFIKASI1")
            {
                ViewBag.Title = "Verifikasi Surat Pengesahan 2,5 Tahun Pertama";
            } else if(lampiranType == "VERIFIKASI2")
            {
                ViewBag.Title = "Verifikasi Surat Pengesahan 2,5 Tahun Kedua";
            }

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

            if(data.Count() > 0)
            {
                data = data.FindAll(b => b.LampiranType == "PENILAIAN").ToList();
            }
            return Json(new
            {
                data
            });
        }

        [HttpGet]
        public async Task<JsonResult> GetAllVerifikasiSurat(string port)
        {
            List<LampiranModel> data = await _lampiranService.GetAllByPort(port);

            if (data.Count() > 0)
            {
                data = data.FindAll(b => b.LampiranType.Contains("VERIFIKASI")).ToList();
            }
            return Json(new
            {
                data
            });
        }

        [HttpGet]
        public async Task<JsonResult> GetAllSuratPengesahan(string port)
        {
            List<LampiranModel> data = new List<LampiranModel>();
            List<LampiranModel> list = await _lampiranService.GetAllByPort(port);

            if (list.Count() > 0)
            {
                data = list.FindAll(b => b.LampiranType == "PENGESAHAN").ToList();
            }
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
