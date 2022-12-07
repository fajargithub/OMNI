using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMNI.API.Model.OMNI;
using OMNI.Data.Data;
using OMNI.Data.Data.Dao;
using OMNI.Migrations.Data.Dao;
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
    public class GuestRegionController : ControllerBase
    {
        private readonly OMNIDbContext _dbOMNI;
        private readonly CorePTKContext _dbCorePTK;
        protected UserManager<ApplicationUser> _userManager;

        public GuestRegionController(OMNIDbContext dbOMNI, CorePTKContext dbCorePTK, UserManager<ApplicationUser> userManager)
        {
            _dbOMNI = dbOMNI;
            _dbCorePTK = dbCorePTK;
            _userManager = userManager;
        }

        // GET: api/<ValuesController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            List<GuestRegionModel> result = new List<GuestRegionModel>();
            
            var guestRegion1 = await _userManager.GetUsersInRoleAsync("OSMOSYS_GUEST_REGION1");
            if(guestRegion1.Count() > 0)
            {
                for(int i=0; i < guestRegion1.Count(); i++)
                {
                    var findEmployee = await _dbCorePTK.Employees.Where(b => b.IsDeleted == GeneralConstants.NO && b.Email.ToLower() == guestRegion1[i].Email.ToLower()).FirstOrDefaultAsync(cancellationToken);
                    if(findEmployee != null)
                    {
                        
                        GuestRegionModel temp = new GuestRegionModel();
                        temp.UserId = findEmployee.Id;
                        temp.UserName = findEmployee.Name;
                        temp.Email = findEmployee.Email;
                        temp.GuestCategory = "Pertamina";
                        temp.Region = "REGION 1";
                        result.Add(temp);
                    }
                }
            }

            var guestRegion2 = await _userManager.GetUsersInRoleAsync("OSMOSYS_GUEST_REGION2");
            if (guestRegion2.Count() > 0)
            {
                for (int i = 0; i < guestRegion2.Count(); i++)
                {
                    var findEmployee = await _dbCorePTK.Employees.Where(b => b.IsDeleted == GeneralConstants.NO && b.Email.ToLower().Contains(guestRegion2[i].Email.ToLower())).FirstOrDefaultAsync(cancellationToken);
                    if (findEmployee != null)
                    {

                        GuestRegionModel temp = new GuestRegionModel();
                        temp.UserId = findEmployee.Id;
                        temp.UserName = findEmployee.Name;
                        temp.Email = findEmployee.Email;
                        temp.GuestCategory = "Pertamina";
                        temp.Region = "REGION 2";
                        result.Add(temp);
                    }
                }
            }

            var guestRegion3 = await _userManager.GetUsersInRoleAsync("OSMOSYS_GUEST_REGION3");
            if (guestRegion3.Count() > 0)
            {
                for (int i = 0; i < guestRegion3.Count(); i++)
                {
                    var findEmployee = await _dbCorePTK.Employees.Where(b => b.IsDeleted == GeneralConstants.NO && b.Email.ToLower() == guestRegion3[i].Email.ToLower()).FirstOrDefaultAsync(cancellationToken);
                    if (findEmployee != null)
                    {

                        GuestRegionModel temp = new GuestRegionModel();
                        temp.UserId = findEmployee.Id;
                        temp.UserName = findEmployee.Name;
                        temp.Email = findEmployee.Email;
                        temp.GuestCategory = "Pertamina";
                        temp.Region = "REGION 3";
                        result.Add(temp);
                    }
                }
            }

            var guestNonRegion1 = await _userManager.GetUsersInRoleAsync("OSMOSYS_GUEST_NON_REGION1");
            if (guestNonRegion1.Count() > 0)
            {
                for (int i = 0; i < guestNonRegion1.Count(); i++)
                {
                    var findEmployee = await _dbCorePTK.Employees.Where(b => b.IsDeleted == GeneralConstants.NO && b.Email.ToLower() == guestNonRegion1[i].Email.ToLower()).FirstOrDefaultAsync(cancellationToken);
                    if (findEmployee != null)
                    {

                        GuestRegionModel temp = new GuestRegionModel();
                        temp.UserId = findEmployee.Id;
                        temp.UserName = findEmployee.Name;
                        temp.Email = findEmployee.Email;
                        temp.GuestCategory = "Non Pertamina";
                        temp.Region = "REGION 1";
                        result.Add(temp);
                    }
                }
            }

            var guestNonRegion2 = await _userManager.GetUsersInRoleAsync("OSMOSYS_GUEST_NON_REGION2");
            if (guestNonRegion2.Count() > 0)
            {
                for (int i = 0; i < guestNonRegion2.Count(); i++)
                {
                    var findEmployee = await _dbCorePTK.Employees.Where(b => b.IsDeleted == GeneralConstants.NO && b.Email.ToLower() == guestNonRegion2[i].Email.ToLower()).FirstOrDefaultAsync(cancellationToken);
                    if (findEmployee != null)
                    {

                        GuestRegionModel temp = new GuestRegionModel();
                        temp.UserId = findEmployee.Id;
                        temp.UserName = findEmployee.Name;
                        temp.Email = findEmployee.Email;
                        temp.GuestCategory = "Non Pertamina";
                        temp.Region = "REGION 2";
                        result.Add(temp);
                    }
                }
            }

            var guestNonRegion3 = await _userManager.GetUsersInRoleAsync("OSMOSYS_GUEST_NON_REGION3");
            if (guestNonRegion3.Count() > 0)
            {
                for (int i = 0; i < guestNonRegion3.Count(); i++)
                {
                    var findEmployee = await _dbCorePTK.Employees.Where(b => b.IsDeleted == GeneralConstants.NO && b.Email.ToLower() == guestNonRegion3[i].Email.ToLower()).FirstOrDefaultAsync(cancellationToken);
                    if (findEmployee != null)
                    {

                        GuestRegionModel temp = new GuestRegionModel();
                        temp.UserId = findEmployee.Id;
                        temp.UserName = findEmployee.Name;
                        temp.Email = findEmployee.Email;
                        temp.GuestCategory = "Non Pertamina";
                        temp.Region = "REGION 3";
                        result.Add(temp);
                    }
                }
            }

            if(result.Count() > 0)
            {
                result = result.OrderByDescending(b => b.GuestCategory).ThenBy(b => b.Region).ToList();
            }

            return Ok(result);
        }
    }
}
