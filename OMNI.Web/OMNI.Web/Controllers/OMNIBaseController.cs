using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OMNI.Web.Data.Dao;
using OMNI.Web.Data.Dao.CorePTK;
using OMNI.Web.Services.CorePTK.Interface;
using OMNI.Web.Services.Master.Interface;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMNI.Web.Models.Master;
using OMNI.Web.Services.Trx.Interface;
using System.Net;
using OMNI.Web.Models;
using OMNI.Utilities.Constants;
using Microsoft.AspNetCore.Routing;
using System;

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

        public static class UserData
        {
            public static string Username { get; set; }
            public static string Email { get; set; }
            public static List<string> RoleList;
            public static string ParamRole { get; set; }
        }

        public static bool CheckUserRole(string param)
        {
            bool result = false;
            var roleList = UserData.RoleList;
            if(roleList != null)
            {
                roleList = roleList.FindAll(b => b.Contains(param));
                if (roleList.Count() > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public class CheckRole : ActionFilterAttribute
        {
            bool isInRole = false;
            

            public CheckRole(string values)
            {
                UserData.ParamRole = values;
            }
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                isInRole = CheckUserRole(UserData.ParamRole);

                if (!isInRole)
                {
                    UserData.Username = null;
                    UserData.Email = null;
                    UserData.RoleList = null;
                    context.Result = new RedirectToActionResult("Index", "Login", null);
                }
            }

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
