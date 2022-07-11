using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OMNI.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly string ADD_EDIT_LLP = "~/Views/Home/AddEditLLP.cshtml";
        private static readonly string ADD_EDIT_PERSONIL = "~/Views/Home/AddEditPersonil.cshtml";
        private static readonly string ADD_EDIT_LATIHAN = "~/Views/Home/AddEditLatihan.cshtml";

        private static readonly string INDEX_FILE = "~/Views/Home/IndexFile.cshtml";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("ManageLLP")]
        [HttpGet("ManageLLP/{id:int}")]
        public IActionResult AddEditLLP(int id)
        {
            return PartialView(ADD_EDIT_LLP);
        }

        [HttpPost("ManageLLP")]
        [HttpPost("ManageLLP/{id:int}")]
        public IActionResult AddEditLLP([FromBody] object account, [FromRoute] int id)
        {
            return Ok();
        }

        [HttpGet("ManageTrxPersonil")]
        [HttpGet("ManageTrxPersonil/{id:int}")]
        public IActionResult AddEditTrxPersonil(int id)
        {
            return PartialView(ADD_EDIT_PERSONIL);
        }

        [HttpPost("ManageTrxPersonil")]
        [HttpPost("ManageTrxPersonil/{id:int}")]
        public IActionResult AddEditTrxPersonil([FromBody] object account, [FromRoute] int id)
        {
            return Ok();
        }

        [HttpGet("ManageTrxLatihan")]
        [HttpGet("ManageTrxLatihan/{id:int}")]
        public IActionResult AddEditTrxLatihan(int id)
        {
            return PartialView(ADD_EDIT_LATIHAN);
        }

        [HttpPost("ManageTrxLatihan")]
        [HttpPost("ManageTrxLatihan/{id:int}")]
        public IActionResult AddEditTrxLatihan([FromBody] object account, [FromRoute] int id)
        {
            return Ok();
        }

        [HttpGet("ManageFile")]
        [HttpGet("ManageFile/{id:int}")]
        public IActionResult IndexFile()
        {
            return PartialView(INDEX_FILE);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
