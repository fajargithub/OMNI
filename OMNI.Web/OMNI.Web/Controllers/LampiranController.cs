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
    [CheckRole(Roles = GeneralConstants.OSMOSYS_SUPER_ADMIN + "," + GeneralConstants.OSMOSYS_MANAGEMENT + "," + GeneralConstants.OSMOSYS_ADMIN_LOKASI + "," + GeneralConstants.OSMOSYS_ADMIN_REGION1 + "," + GeneralConstants.OSMOSYS_ADMIN_REGION2 + "," + GeneralConstants.OSMOSYS_ADMIN_REGION3 + "," + GeneralConstants.OSMOSYS_GUEST_LOKASI + "," + GeneralConstants.OSMOSYS_GUEST_NON_LOKASI)]
    public class LampiranController : OMNIBaseController
    {
        private static readonly string INDEX_LAMPIRAN = "~/Views/Lampiran/IndexLampiran.cshtml";
        private static readonly string ADD_EDIT_LAMPIRAN = "~/Views/Lampiran/AddEditLampiran.cshtml";

        protected ILampiran _lampiranService;
        protected ILLPTrx _llpTrxService;
        public LampiranController(IAdminLocation adminLocationService, ILampiran lampiranService, ILLPTrx llptrxService, IPort portService, IGuestLocation guestLocationService, IRekomendasiType rekomendasiTypeService, IPeralatanOSR peralatanOSRService, IJenis jenisService) : base(adminLocationService, guestLocationService, rekomendasiTypeService, portService, peralatanOSRService, jenisService)
        {
            _lampiranService = lampiranService;
            _llpTrxService = llptrxService;
            _portService = portService;
        }

        public async Task<IActionResult> Index(string port)
        {
            ViewBag.Info = "* Silahkan upload Surat Penilaian";

            var dateNow = DateTime.Now;

            ViewBag.SelectedPort = "";

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

            ViewBag.EnablePenilaian = true;
            ViewBag.EnablePengesahan = false;
            ViewBag.EnableVerifikasi1 = false;

            List<LampiranModel> lampiranList = await _lampiranService.GetAllByPort(GetSelectedPort());
            if(lampiranList.Count() > 0)
            {
                var findPenilaian = lampiranList.FindAll(b => b.LampiranType == "PENILAIAN").OrderByDescending(b => b.Id).FirstOrDefault();
                if(findPenilaian != null)
                {
                    //ViewBag.EnablePenilaian = false;
                    ViewBag.EnablePengesahan = true;
                    ViewBag.Info = "* Mohon upload Surat Pengesahan";
                    var findPengesahan = lampiranList.FindAll(b => b.LampiranType == "PENGESAHAN").OrderByDescending(b => b.Id).FirstOrDefault();
                    if(findPengesahan != null && findPengesahan.EndDate != "-")
                    {
                        //ViewBag.EnablePengesahan = false;
                        var endDatePengesahan = DateTime.ParseExact(findPengesahan.EndDate, "dd MMM yyyy", null);
                        if(dateNow >= endDatePengesahan)
                        {
                            ViewBag.EnableVerifikasi1 = true;
                        } 
                        else
                        {
                            ViewBag.EnableVerifikasi1 = false;
                        }

                        //ViewBag.Info = "* Mohon upload Verifikasi Surat Perpanjangan Pengesahan (2,5 tahun pertama) per tanggal " + findPengesahan.EndDate;
                        ViewBag.Info = "* Mohon upload Surat Verifikasi Antara per tanggal " + findPengesahan.EndDate;
                    } else
                    {
                        ViewBag.EnableVerifikasi1 = false;
                        //ViewBag.EnableVerifikasi2 = false;
                    }

                    var findVerifikasi1 = lampiranList.FindAll(b => b.LampiranType == "VERIFIKASI1").OrderByDescending(b => b.Id).FirstOrDefault();
                    if (findVerifikasi1 != null)
                    {
                        ViewBag.EnableVerifikasi1 = false;
                        ViewBag.Info = "";
                    }
                }
                else
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
            var session = GetSession();
            if (session != null)
            {
                if (model.Id > 0)
                {
                    model.UpdatedBy = session.Username;
                }
                else
                {
                    model.CreatedBy = session.Username;
                }
            }

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
        public async Task<IActionResult> ViewFile(int id, string fileName, string flag)
        {
            var r = await _llpTrxService.ReadFile(id, fileName, flag);
            var file = await _llpTrxService.GetFileData(id);

            return File(r, @"" + file.ContentType, file.FileName);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLampiran(int id)
        {
            var r = await _lampiranService.Delete(id);

            return Ok(new JsonResponse());
        }
    }
}
