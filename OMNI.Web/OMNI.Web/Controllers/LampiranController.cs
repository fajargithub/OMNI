using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMNI.Web.Services.Master.Interface;
using OMNI.Web.Services.Trx.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers
{
    [AllowAnonymous]
    public class LampiranController : Controller
    {
        private static readonly string INDEX_LAMPIRAN = "~/Views/Lampiran/IndexLampiran.cshtml";

        protected ILampiran _LampiranService;
        protected ILLPTrx _LLPTrxService;
        public LampiranController(ILampiran lampiranService, ILLPTrx llptrxService) : base()
        {
            _LampiranService = lampiranService;
            _LLPTrxService = llptrxService;
        }

        public async Task<IActionResult> Index(int? trxId)
        {
            return View(INDEX_LAMPIRAN);
        }
    }
}
