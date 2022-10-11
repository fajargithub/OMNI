﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMNI.API.Model.OMNI;
using OMNI.API.Services.LDAP;
using OMNI.Data.Data;
using OMNI.Data.Data.Dao;
using OMNI.Migrations.Data.Dao;
using OMNI.Migrations.Data.Dao.CorePTK;
using OMNI.Utilities.Base;
using OMNI.Utilities.Constants;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OMNI.API.Controllers.OMNI
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LoginController : ControllerBase
    {
        private readonly OMNIDbContext _dbOMNI;
        private readonly CorePTKContext _dbCorePTK;

        protected UserManager<ApplicationUser> _userManager;
        private readonly CustomSignInService _customSignInService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginController(OMNIDbContext dbOMNI, CorePTKContext dbCorePTK, UserManager<ApplicationUser> userManager, CustomSignInService customSignInService, SignInManager<ApplicationUser> signInManager = null)
        {
            _dbOMNI = dbOMNI;
            _dbCorePTK = dbCorePTK;
            _userManager = userManager;
            _customSignInService = customSignInService;
            _signInManager = signInManager;
        }

        //[HttpGet]
        //public async Task<IActionResult> Get(CancellationToken cancellationToken)
        //{
        //    var result = await _dbCorePTK.Employees.Where(b => b.IsDeleted == GeneralConstants.NO).OrderByDescending(b => b.CreatedAt).OrderByDescending(b => b.UpdatedAt).ToListAsync(cancellationToken);
        //    return Ok(result);
        //}

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginModel model, CancellationToken cancellationToken)
        {
            ApplicationUser appUser =
                    await _userManager.FindByNameAsync(model.Username) ??
                    await _userManager.FindByEmailAsync(model.Username) ??
                    await _userManager.FindByEmailAsync($@"{model.Username}@pertamina.com") ??
                    await _userManager.FindByEmailAsync($@"{model.Username}@mitrakerja.pertamina.com") ??
                    await _userManager.FindByEmailAsync($@"{model.Username}@ptk-shipping.com");

            Microsoft.AspNetCore.Identity.SignInResult result;

            if (appUser == null)
                result = Microsoft.AspNetCore.Identity.SignInResult.Failed;
            else if ((appUser.Email.Contains("@pertamina.com") || appUser.Email.Contains("@mitrakerja.pertamina.com")) && await _customSignInService.Authenticate(appUser.Email, model.Password, true, false) == Microsoft.AspNetCore.Identity.SignInResult.Success)
                result = Microsoft.AspNetCore.Identity.SignInResult.Success;
            else
                result = await _signInManager.CheckPasswordSignInAsync(appUser, model.Password, lockoutOnFailure: true);

            if (result.Succeeded)
            {

                Employee employee = await _dbCorePTK.Employees
                    .Where(b => b.IsDeleted == GeneralConstants.NO)
                    .Include(b => b.EmployeePosition.EmployeePositionAbbrv)
                    .Include(b => b.MaritalStatus)
                    .Include(b => b.EmployeePosition.Organization)
                    .Include(b => b.EmployeePosition.PAreaSub.PArea.Region)
                    .Include(b => b.Religion)
                    .Include(b => b.PortEmployees)
                    .ThenInclude(b => b.EmployeePosition.PAreaSub)
                    .Include(b => b.ShipEmployees)
                    .ThenInclude(b => b.Ship)
                    .FirstOrDefaultAsync(
                    b => b.Email.ToLower() == appUser.Email.ToLower() &&
                    b.IsDeleted == GeneralConstants.NO
                    );

                var user = new ApplicationUser
                {
                    UserName = GeneralConstants.CheckIsPtkDomain(model.Username) ? GeneralConstants.ConvertToUserName(model.Username) : model.Username,
                    Email = model.Username
                };

                var userRoles = await _userManager.GetUserNameAsync(user);

                return Ok(new ReturnJson());
            } else
            {
                return Ok(new ReturnJson { IsSuccess = false, ErrorMsg = "Invalid Username / Password"});
            }
        }
    }
}
