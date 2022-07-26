using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMNI.Utilities.Constants;
using OMNI.Web.Data.Dao;
using OMNI.Web.Data.Dao.CorePTK;
using OMNI.Web.Models;
using OMNI.Web.Models.Master;
using OMNI.Web.Services.CorePTK.Interface;
using OMNI.Web.Services.Master.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers.Master
{
    [AllowAnonymous]
    public class PersonilController : OMNIBaseController
    {
        private static readonly string INDEX = "~/Views/Master/Personil/Index.cshtml";
        private static readonly string ADD_EDIT = "~/Views/Master/Personil/AddEdit.cshtml";

        protected IPersonil _PersonilService;
        public PersonilController(IPersonil PersonilService, IPort portService, IPeralatanOSR peralatanOSRService) : base(portService, peralatanOSRService)
        {
            _PersonilService = PersonilService;
        }

        public async Task<JsonResult> GetAll(int portId)
        {
            List<PersonilModel> data = new List<PersonilModel>();
            List<Personil> list = await _PersonilService.GetAllByPortId(portId);

            //if (list.Count() > 0)
            //{
            //    for (int i = 0; i < list.Count(); i++)
            //    {
            //        PersonilModel temp = new PersonilModel();
            //        temp.Id = list[i].Id;
            //        temp.Name = list[i].Name;
            //        temp.Port = list[i].PortId > 0 ? GetPortById(list[i].PortId).Result.Name : "-";
            //        temp.Satuan = list[i].Satuan;
            //        temp.Desc = list[i].Desc;
            //        data.Add(temp);
            //    }
            //}

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
            }
            else
            {
                ViewBag.SelectedPort = portList.OrderBy(b => b.Id).FirstOrDefault();
            }

            return View(INDEX);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id, int portId)
        {
            PersonilModel model = new PersonilModel();
           // model.Port = portId.ToString();

            if (id > 0)
            {
                Personil data = await _PersonilService.GetById(id);
                if (data != null)
                {
                    model.Id = data.Id;
                    model.Name = data.Name;
                    model.Satuan = data.Satuan;
                    model.Desc = data.Desc;
                }
            }

            return PartialView(ADD_EDIT, model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(PersonilModel model)
        {
            var r = await _PersonilService.AddEdit(model);

            if (!r.IsSuccess || r.Code != (int)HttpStatusCode.OK)
            {
                return Ok(new JsonResponse { Status = GeneralConstants.FAILED, ErrorMsg = r.ErrorMsg });
            }

            return Ok(new JsonResponse());
        }

        [HttpPost]
        public async Task<IActionResult> DeletePersonil(int id)
        {
            var r = await _PersonilService.Delete(id);

            return Ok(new JsonResponse());
        }
    }
}
