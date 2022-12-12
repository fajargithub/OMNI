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
using System.Net.Sockets;
using Microsoft.AspNetCore.Http;

namespace OMNI.Web.Controllers
{
    public class OMNIBaseController : BaseController
    {
        protected IPort _portService;
        protected IPeralatanOSR _peralatanOSRService;
        protected IJenis _jenisService;
        protected IRekomendasiType _rekomendasiTypeService;
        protected IGuestLocation _guestLocationService;
        protected IAdminLocation _adminLocationService;
        public OMNIBaseController(IAdminLocation adminLocationService, IGuestLocation guestLocationService, IRekomendasiType rekomendasiTypeService, IPort portService, IPeralatanOSR peralatanOSRService, IJenis jenisService) : base()
        {
            _rekomendasiTypeService = rekomendasiTypeService;
            _portService = portService;
            _peralatanOSRService = peralatanOSRService;
            _jenisService = jenisService;
            _guestLocationService = guestLocationService;
            _adminLocationService = adminLocationService;
        }

        public static class PortData
        {
            public static List<Port> PortList { get; set; }
            public static string RegionTxt { get; set; }
        }

        public class UserData
        {
            public int UserId { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
        }

        public void SetSession(UserData data)
        {
            HttpContext.Session.SetInt32("userId", data.UserId);
            HttpContext.Session.SetString("username", data.Username);
            HttpContext.Session.SetString("email", data.Email);
            HttpContext.Session.SetString("role", data.Role);
        }

        public UserData GetSession()
        {
            UserData user = new UserData();
            user.UserId = HttpContext.Session.GetInt32("userId").HasValue ? this.HttpContext.Session.GetInt32("userId").Value : 0;
            user.Username = HttpContext.Session.GetString("username");
            user.Email = HttpContext.Session.GetString("email");
            user.Role = HttpContext.Session.GetString("Role");
            return user;
        }

        public void DeleteSession()
        {
            HttpContext.Session.Clear();
        }

        public static int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }

        public string GetRegionTxt(string role)
        {
            string result = "";

            if (string.IsNullOrEmpty(role))
            {
                role = "";
            }

            if (role.Contains("REGION1"))
            {
                result = "Region 1";
            }
            else if (role.Contains("REGION2"))
            {
                result = "Region 2";
            }
            else if (role.Contains("REGION3"))
            {
                result = "Region 3";
            }
            else if (role.Contains("OSMOSYS_GUEST_LOKASI") || role.Contains("OSMOSYS_GUEST_NON_LOKASI"))
            {
                result = "Region (Selected)";
            }
            else if (role.Contains("OSMOSYS_ADMIN_LOKASI"))
            {
                result = "Region (Selected)";
            }
            else if (role.Contains("OSMOSYS_SUPER_ADMIN"))
            {
                result = "Region 1, 2 & 3";
            }

            return result;
        }

        public async Task<List<Port>> GetPorts(string role, int userId)
        {
            List<Port> result = new List<Port>();

            if (string.IsNullOrEmpty(role))
            {
                role = "";
            }

            if(userId < 1)
            {
                userId = 0;
            }

            if (role.Contains("REGION1"))
            {
                result = await GetPortByRegion(2);
            }
            else if (role.Contains("REGION2"))
            {
                result = await GetPortByRegion(3);
            }
            else if (role.Contains("REGION3"))
            {
                result = await GetPortByRegion(4);
            }
            else if (role.Contains("OSMOSYS_GUEST_LOKASI") || role.Contains("OSMOSYS_GUEST_NON_LOKASI"))
            {
                List<Port> userPorts = new List<Port>();
                GuestLocationModel guestUser = await _guestLocationService.GetByUserId(userId);
                if (guestUser != null)
                {
                    if (guestUser.PortList.Count() > 0)
                    {
                        var portList = await GetAllPort();
                        if (portList.Count() > 0)
                        {
                            for (int i = 0; i < guestUser.PortList.Count(); i++)
                            {
                                Port temp = new Port();
                                var findPort = portList.FindAll(b => b.Name.Contains(guestUser.PortList[i])).FirstOrDefault();
                                if (findPort != null)
                                {
                                    userPorts.Add(findPort);
                                }
                            }
                        }
                    }
                }

                result = userPorts;
            }
            else if (role.Contains("OSMOSYS_ADMIN_LOKASI"))
            {
                List<Port> userPorts = new List<Port>();
                AdminLocationModel adminUser = await _adminLocationService.GetByUserId(userId);
                if (adminUser != null)
                {
                    if (adminUser.PortList.Count() > 0)
                    {
                        var portList = await GetAllPort();
                        if (portList.Count() > 0)
                        {
                            for (int i = 0; i < adminUser.PortList.Count(); i++)
                            {
                                Port temp = new Port();
                                var findPort = portList.FindAll(b => b.Name.Contains(adminUser.PortList[i])).FirstOrDefault();
                                if (findPort != null)
                                {
                                    userPorts.Add(findPort);
                                }
                            }
                        }
                    }
                }

                result = userPorts;
            }
            else 
            {
                result = await GetAllPort();
            }

            return result;
        }

        //public static class UserData
        //{
        //    public static int UserId { get; set; }
        //    public static string Username { get; set; }
        //    public static string Email { get; set; }
        //    public static List<string> RoleList;
        //    public static string[] ParamRole { get; set; }
        //}

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //base.OnActionExecuted(context);

            //ViewBag.Username = UserData.Username;
            //ViewBag.Email = UserData.Email;
            //ViewBag.Roles = UserData.RoleList;

            //if (UserData.RoleList != null)
            //{
            //    if (UserData.RoleList.Contains(GeneralConstants.OSMOSYS_MANAGEMENT) || UserData.RoleList.Contains(GeneralConstants.OSMOSYS_GUEST_LOKASI) || UserData.RoleList.Contains(GeneralConstants.OSMOSYS_GUEST_NON_LOKASI))
            //    {
            //        ViewBag.Editable = false;
            //    }
            //    else
            //    {
            //        ViewBag.Editable = true;
            //    }

            //    if (UserData.RoleList.Contains(GeneralConstants.OSMOSYS_SUPER_ADMIN) || UserData.RoleList.Contains(GeneralConstants.OSMOSYS_MANAGEMENT))
            //    {
            //        ViewBag.EnableUserAccess = true;
            //    }
            //    else
            //    {
            //        ViewBag.EnableUserAccess = false;
            //    }
            //}
            //else
            //{
            //    ViewBag.Editable = false;
            //}
        }

        //public static bool CheckUserRole()
        //{
        //    bool result = false;
        //    var roleList = UserData.RoleList;
        //    if(roleList != null)
        //    {
        //        if(UserData.ParamRole.Count() > 0)
        //        {
        //            for(int i=0; i < UserData.ParamRole.Count(); i++)
        //            {
        //                if (!result)
        //                {
        //                    var findRole = roleList.FindAll(b => b.Contains(UserData.ParamRole[i]));
        //                    if (findRole.Count() > 0)
        //                    {
        //                        result = true;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return result;
        //}

        public class CheckRole : ActionFilterAttribute
        {
            bool isInRole = false;

            public string Roles { get; set; }

            public override void OnActionExecuting(ActionExecutingContext context)
            {

                ////base.OnActionExecuting(context);
                //if (!string.IsNullOrEmpty(Roles))
                //{
                //    var arrRoles = Roles.Split(",");
                //    UserData.ParamRole = arrRoles;
                //}

                //isInRole = CheckUserRole();

                //if (!isInRole)
                //{
                //    if(UserData.ParamRole.Count() > 0 && !string.IsNullOrEmpty(UserData.Username))
                //    {
                //        context.Result = new RedirectToActionResult("NoAccess", "Login", null);
                //    } else
                //    {
                //        UserData.Username = null;
                //        UserData.Email = null;
                //        UserData.RoleList = null;

                //        context.Result = new RedirectToActionResult("Index", "Login", null);
                //    }  
                //} else
                //{
                //}
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

        public async Task<List<Port>> GetPortByRegion(int regionId)
        {
            List<Port> data = await _portService.GetPortByRegion(regionId);

            return data;
        }

        public async Task<Port> GetPortById(int id)
        {
            Port data = await _portService.GetById(id);

            return data;
        }

    }
}
