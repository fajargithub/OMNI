using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using OMNI.Web.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMNI.Web.Extensions
{
    public class ViewBagFilter : IActionFilter
    {
        private static readonly string Enabled = "Enabled";
        private static readonly string Disabled = string.Empty;

        public readonly HttpContext _httpContext;
        private readonly IConfiguration _configuration;

        public ViewBagFilter(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _configuration = configuration;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            if (context.Controller is Controller controller)
            {
                // SmartAdmin Toggle Features
                controller.ViewBag.AppSidebar = Enabled;
                controller.ViewBag.AppHeader = Enabled;
                controller.ViewBag.AppLayoutShortcut = Enabled;
                controller.ViewBag.AppFooter = Enabled;
                controller.ViewBag.ShortcutMenu = Enabled;
                controller.ViewBag.ChatInterface = Enabled;
                controller.ViewBag.LayoutSettings = Enabled;
                controller.ViewBag.SignalRHub = _configuration.GetSection("BaseURL").GetSection("ShipChandler").Value;

                // SmartAdmin Default Settings
                controller.ViewBag.App = WebConstants.APP_NAME;
                controller.ViewBag.AppName = WebConstants.APP_NAME + "Application";
                controller.ViewBag.AppFlavor = WebConstants.APP_NAME;
                controller.ViewBag.AppFlavorSubscript = "Application";
                controller.ViewBag.User = "";
                controller.ViewBag.Email = "";
                controller.ViewBag.Twitter = "";
                controller.ViewBag.Avatar = "avatar.png";
                controller.ViewBag.Version = WebConstants.APP_VERSION;
                controller.ViewBag.Bs4v = WebConstants.BOOTSTRAP_VERSION;
                controller.ViewBag.Logo = "logo-simontana-lg-transparant.png";
                controller.ViewBag.LogoM = "logo-simontana-lg-transparant.png";
                controller.ViewBag.Copyright = "2021 © Pertamina Trans Kontinental by&nbsp;<a href='https://www.ptk-shipping.com/' class='text-primary fw-500' title='ptk-shipping.com' target='_blank'>PTK Team</a>";
                controller.ViewBag.CopyrightInverse = "2021 © Pertamina Trans Kontinental by&nbsp;<a href='https://www.ptk-shipping.com/' class='text-white opacity-40 fw-500' title='ptk-shipping.com' target='_blank'>PTK Team</a>";
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
