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
    public class SpesifikasiJenisBaseController : BaseController
    {
        protected IPeralatanOSR _peralatanOSRService;
        protected ISpesifikasiJenis _spesifikasiJenisService;
        protected IDetailSpesifikasi _detailSpesifikasiService;
        public SpesifikasiJenisBaseController(IPeralatanOSR peralatanOSRService, ISpesifikasiJenis spesifikasiJenisService, IDetailSpesifikasi detailSpesifikasiService) : base()
        {
            _peralatanOSRService = peralatanOSRService;
            _spesifikasiJenisService = spesifikasiJenisService;
            _detailSpesifikasiService = detailSpesifikasiService;
        }

        #region SPESIFIKASI JENIS REGION
        public JsonResult GetAllSpesifikasiJenis()
        {
            List<SpesifikasiJenis> list = _spesifikasiJenisService.GetAllIncludingWithFilter(b => b.IsDeleted == GeneralConstants.NO, b => b.PeralatanOSR).ToList();

            List<SpesifikasiJenisModel> data = new List<SpesifikasiJenisModel>();

            if(list.Count() > 0)
            {
                for(int i=0; i < list.Count(); i++)
                {
                    SpesifikasiJenisModel temp = new SpesifikasiJenisModel();
                    temp.Id = list[i].Id;
                    temp.PeralatanOSR = list[i].PeralatanOSR.Name;
                    temp.Name = list[i].Name;
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

        public SpesifikasiJenisModel GetSpesifikasiJenisById(int id)
        {
            SpesifikasiJenisModel model = new SpesifikasiJenisModel();
            if (id > 0)
            {
                SpesifikasiJenis data = _spesifikasiJenisService.GetById(id);
                if (data != null)
                {
                    model.Id = data.Id;
                    model.Name = data.Name;
                    model.Desc = data.Desc;
                }
            }

            return model;
        }

        public JsonResult AddEditSpesifikasiJenisFunction(SpesifikasiJenisModel model)
        {
            try
            {
                if (model.Id > 0)
                {
                    SpesifikasiJenis temp = _spesifikasiJenisService.Find(b => b.Id == model.Id);
                    temp.Id = model.Id;
                    temp.PeralatanOSR = _peralatanOSRService.GetById(int.Parse(model.PeralatanOSR));
                    temp.Name = model.Name;
                    temp.Desc = model.Desc;
                    temp.UpdatedBy = "admin";
                    temp.UpdatedAt = DateTime.Now;

                    _spesifikasiJenisService.Update(temp);
                }
                else
                {
                    SpesifikasiJenis temp = new SpesifikasiJenis();
                    temp.Id = model.Id;
                    temp.PeralatanOSR = _peralatanOSRService.GetById(int.Parse(model.PeralatanOSR));
                    temp.Name = model.Name;
                    temp.Desc = model.Desc;
                    temp.CreatedBy = "admin";
                    temp.CreatedAt = DateTime.Now;

                    _spesifikasiJenisService.Add(temp);
                }


                return JsonReturn(GeneralConstants.SUCCESS, null, null);

            }
            catch (Exception e)
            {
                return JsonReturn(GeneralConstants.FAILED, null, null);
            }
        }

        public IActionResult DeleteSpesifikasiJenisFunction(int id)
        {
            SpesifikasiJenis data = _spesifikasiJenisService.GetById(id);
            data.IsDeleted = GeneralConstants.YES;

            _spesifikasiJenisService.Update(data);

            return Json(new
            {
                success = true
            });
        }
        #endregion

    }
}
