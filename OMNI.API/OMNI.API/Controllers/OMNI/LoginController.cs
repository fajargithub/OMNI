using Microsoft.AspNetCore.Identity;
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
using System.Collections.Generic;
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
        private readonly CorePTKContext _dbCorePTK;
        private readonly ApplicationDbContext _appDbContext;

        protected UserManager<ApplicationUser> _userManager;
        private readonly CustomSignInService _customSignInService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginController(ApplicationDbContext appDbContext, CorePTKContext dbCorePTK, UserManager<ApplicationUser> userManager, CustomSignInService customSignInService, SignInManager<ApplicationUser> signInManager = null)
        {
            _dbCorePTK = dbCorePTK;
            _appDbContext = appDbContext;
            _userManager = userManager;
            _customSignInService = customSignInService;
            _signInManager = signInManager;
        }

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

                List<string> roleList = new List<string>();
                List<UserRoleModel> findRoles = (from userRole in _appDbContext.UserRoles
                                             join role in _appDbContext.Roles on userRole.RoleId equals role.Id
                                             join user in _appDbContext.Users on userRole.UserId equals user.Id
                                             where (user.Email == employee.Email && role.Name.Contains("OSMOSYS"))
                                             select new UserRoleModel
                                             {
                                                 RoleName = role.Name,
                                             }).ToList();

                if(findRoles.Count() > 0)
                {
                    for(int i=0; i < findRoles.Count(); i++)
                    {
                        roleList.Add(findRoles[i].RoleName);
                    }
                }

                return Ok(new ReturnJson { IsSuccess = true, UserId = employee.Id, Username = employee.Name, Email = employee.Email, Roles = roleList } );
            } else
            {
                return Ok(new ReturnJson { IsSuccess = false, ErrorMsg = "Invalid Username / Password"});
            }
        }
    }
}
