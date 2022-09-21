using Microsoft.AspNetCore.Mvc;
using OMNI.Web.Data.Dao;
using OMNI.Web.Data.Dao.CorePTK;
using OMNI.Web.Services.CorePTK.Interface;
using OMNI.Web.Services.Master.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers
{
    public class OMNIBaseController : BaseController
    {
        protected IPort _portService;
        protected IPeralatanOSR _peralatanOSRService;
        protected IJenis _jenisService;
        protected IRekomendasiType _rekomendasiTypeService;
        public OMNIBaseController(IRekomendasiType rekomendasiTypeService, IPort portService, IPeralatanOSR peralatanOSRService, IJenis jenisService) : base()
        {
            _rekomendasiTypeService = rekomendasiTypeService;
            _portService = portService;
            _peralatanOSRService = peralatanOSRService;
            _jenisService = jenisService;
        }

        public class yearData
        {
            public int Value { get; set; }
            public int Name { get; set; }
        }

        public List<yearData> GetYearList(int startYear, int endYear)
        {
            List<yearData> yearList = new List<yearData>();
            int yearRange = endYear - startYear;
            if (yearRange > 0)
            {
                for (int i = 0; i <= yearRange; i++)
                {
                    yearData temp = new yearData();
                    temp.Value = startYear;
                    temp.Name = startYear;
                    yearList.Add(temp);

                    startYear += 1;
                }
            }

            return yearList;
        }

        public async Task<List<PeralatanOSR>> GetAllPeralatanOSR()
        {
            List<PeralatanOSR> data = await _peralatanOSRService.GetAll();

            if(data.Count() > 0)
            {
                data = data.OrderBy(b => b.Id).ToList();
            }

            return data;
        }

        public async Task<List<Jenis>> GetAllJenis()
        {
            List<Jenis> data = await _jenisService.GetAll();

            return data;
        }

        public async Task<List<RekomendasiType>> GetAllRekomendasiType()
        {
            List<RekomendasiType> data = await _rekomendasiTypeService.GetAll();

            return data;
        }

        public async Task<List<Port>> GetAllPort()
        {
            List<Port> data = await _portService.GetAll();

            return data;
        }

        public async Task<Port> GetPortById(int id)
        {
            Port data = await _portService.GetById(id);

            return data;
        }

    }
}
