﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OMNI.Web.Controllers.Master
{
    [AllowAnonymous]
    public class SpesifikasiJenisController : BaseController
    {
        private static readonly string INDEX = "~/Views/Master/SpesifikasiJenis/Index.cshtml";
        private static readonly string ADD_EDIT = "~/Views/Master/SpesifikasiJenis/AddEdit.cshtml";

        private static readonly string DETAIL_INDEX = "~/Views/Master/SpesifikasiJenis/DetailIndex.cshtml";
        private static readonly string DETAIL_ADD_EDIT = "~/Views/Master/SpesifikasiJenis/AddEditDetail.cshtml";

        public IActionResult Index()
        {
            return View(INDEX);
        }

        [HttpPost("List")]
        public IActionResult GetAllAccount()
        {
            return Ok();
        }

        [HttpGet("ManageSpesifikasi")]
        [HttpGet("ManageSpesifikasi/{id:int}")]
        public IActionResult AddEdit(int id)
        {
            return PartialView(ADD_EDIT);
        }

        [HttpPost("ManageSpesifikasi")]
        [HttpPost("ManageSpesifikasi/{id:int}")]
        public IActionResult AddEdit([FromBody] object account, [FromRoute] int id)
        {
            return Ok();
        }

        public IActionResult DetailIndex()
        {
            return View(DETAIL_INDEX);
        }

        [HttpGet("ManageDetailSpesifikasi")]
        [HttpGet("ManageDetailSpesifikasi/{id:int}")]
        public IActionResult DetailAddEdit(int id)
        {
            return PartialView(DETAIL_ADD_EDIT);
        }

        [HttpPost("ManageDetailSpesifikasi")]
        [HttpPost("ManageDetailSpesifikasi/{id:int}")]
        public IActionResult DetailAddEdit([FromBody] object account, [FromRoute] int id)
        {
            return Ok();
        }
    }
}
