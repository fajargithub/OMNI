using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMNI.Domain.OMNI.TestRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Controllers
{
    [AllowAnonymous]
    public class TestController : BaseController
    {
        private readonly ITestRepo _testRepo;

        public TestController(ITestRepo testRepo)
        {
            _testRepo = testRepo;
        }

        public async Task<IActionResult> Index()
        {
            List<string> result =await _testRepo.GetPortName();
            return View(result);
        }
    }
}
