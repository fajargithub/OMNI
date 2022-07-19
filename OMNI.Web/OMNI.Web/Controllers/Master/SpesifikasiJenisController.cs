using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMNI.Utilities.Constants;
using OMNI.Web.Data.Dao;
using OMNI.Web.Data.Dao.CorePTK;
using OMNI.Web.Models;
using OMNI.Web.Models.Master;
using OMNI.Web.Services.CorePTK.Interface;
using OMNI.Web.Services.Master.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers.Master
{
    [AllowAnonymous]
    public class SpesifikasiJenisController : OMNIBaseController
    {
        private static readonly string INDEX = "~/Views/Master/SpesifikasiJenis/Index.cshtml";
        private static readonly string ADD_EDIT = "~/Views/Master/SpesifikasiJenis/AddEdit.cshtml";

        protected ISpesifikasiJenis _spesifikasiJenisService;
        public SpesifikasiJenisController(ISpesifikasiJenis spesifikasiJenisService, IPort portService, IPeralatanOSR peralatanOSRService) : base(portService, peralatanOSRService)
        {
            _spesifikasiJenisService = spesifikasiJenisService;
        }

        public async Task<JsonResult> GetAll(int portId)
        {
            List<SpesifikasiJenisModel> data = new List<SpesifikasiJenisModel>();
            List<SpesifikasiJenis> list = await _spesifikasiJenisService.GetAllByPortId(portId);

            if(list.Count() > 0)
            {
                for(int i=0; i < list.Count(); i++)
                {
                    SpesifikasiJenisModel temp = new SpesifikasiJenisModel();
                    temp.Id = list[i].Id;
                    temp.Name = list[i].Name;
                    temp.PeralatanOSR = list[i].PeralatanOSR.Name;
                    temp.Port = list[i].PortId > 0 ? GetPortById(list[i].PortId).Result.Name : "-";
                    temp.QRCode = list[i].QRCode;
                    temp.RekomendasiHubla = list[i].RekomendasiHubla;
                    temp.Desc = list[i].Desc;
                    data.Add(temp);
                }
            }

            int count = data.Count();

            return Json(new
            {
                success = true,
                recordsTotal = count,
                recordsFiltered = count,
                data
            });
        }

        public async Task<IActionResult> Index(int? portId)
        {
            List<Port> portList = await GetAllPort();
            ViewBag.PortList = portList;

            if (portId.HasValue)
            {
                ViewBag.SelectedPort = portList.Where(b => b.Id == portId).FirstOrDefault();
            } else
            {
                ViewBag.SelectedPort = portList.OrderBy(b => b.Id).FirstOrDefault();
            }
            
            return View(INDEX);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id, int portId)
        {
            ViewBag.PeralatanOSRList = await GetAllPeralatanOSR();
            SpesifikasiJenisModel model = new SpesifikasiJenisModel();
            model.Port = portId.ToString();

            if (id > 0)
            {
                SpesifikasiJenis data = await _spesifikasiJenisService.GetById(id);
                if (data != null)
                {
                    model.Id = data.Id;
                    model.QRCode = data.QRCode;
                    model.RekomendasiHubla = data.RekomendasiHubla;
                    model.Name = data.Name;
                    model.Desc = data.Desc;
                }
            }

            return PartialView(ADD_EDIT, model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(SpesifikasiJenisModel model)
        {
            var r = await _spesifikasiJenisService.AddEdit(model);

            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            }

            return Ok(new JsonResponse());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSpesifikasiJenis(int id)
        {
            var r = await _spesifikasiJenisService.Delete(id);

            return Ok(new JsonResponse());
        }
    }
}
