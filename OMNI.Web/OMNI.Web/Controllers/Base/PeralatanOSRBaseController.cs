using Microsoft.AspNetCore.Mvc;
using OMNI.Utilities.Constants;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models.Master;
using OMNI.Web.Services.Master.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers.Base
{
    public class PeralatanOSRBaseController : BaseController
    {
        protected IPeralatanOSR _peralatanOSRService;
        public PeralatanOSRBaseController(IPeralatanOSR peralatanOSRService) : base()
        {
            _peralatanOSRService = peralatanOSRService;
        }

        public JsonResult GetAll()
        {
            List<PeralatanOSR> data = _peralatanOSRService.GetAllWithFilter(b => b.IsDeleted == GeneralConstants.NO).ToList();

            int count = data.Count();

            return Json(new
            {
                success = true,
                recordsTotal = count,
                recordsFiltered = count,
                data
            });
        }

        public PeralatanOSRModel GetDataById(int id)
        {
            PeralatanOSRModel model = new PeralatanOSRModel();
            if(id > 0)
            {
                PeralatanOSR data = _peralatanOSRService.GetById(id);
                if(data != null)
                {
                    model.Id = data.Id;
                    model.Name = data.Name;
                    model.Desc = data.Desc;
                }
            }

            return model;
        }

        public JsonResult AddEditFunction(PeralatanOSRModel model)
        {
            try
            {
                if (model.Id > 0)
                {
                    PeralatanOSR temp = _peralatanOSRService.Find(b => b.Id == model.Id);
                    temp.Id = model.Id;
                    temp.Name = model.Name;
                    temp.Desc = model.Desc;
                    temp.UpdatedBy = "admin";
                    temp.UpdatedAt = DateTime.Now;

                    _peralatanOSRService.Update(temp);
                }
                else
                {
                    PeralatanOSR temp = new PeralatanOSR();
                    temp.Id = model.Id;
                    temp.Name = model.Name;
                    temp.Desc = model.Desc;
                    temp.CreatedBy = "admin";
                    temp.CreatedAt = DateTime.Now;

                    _peralatanOSRService.Add(temp);
                }


                return JsonReturn(GeneralConstants.SUCCESS, null, null);

            }
            catch (Exception e)
            {
                return JsonReturn(GeneralConstants.FAILED, null, null);
            }
        }

        public IActionResult DeleteFunction(int id)
        {
            PeralatanOSR data = _peralatanOSRService.GetById(id);
            data.IsDeleted = GeneralConstants.YES;

            _peralatanOSRService.Update(data);

            return Json(new
            {
                success = true
            });
        }
    }
}
