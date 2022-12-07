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
    public class GuestLocationController : ControllerBase
    {
        private readonly OMNIDbContext _dbOMNI;
        private readonly CorePTKContext _dbCorePTK;
        protected UserManager<ApplicationUser> _userManager;

        public GuestLocationController(OMNIDbContext dbOMNI, CorePTKContext dbCorePTK, UserManager<ApplicationUser> userManager)
        {
            _dbOMNI = dbOMNI;
            _dbCorePTK = dbCorePTK;
            _userManager = userManager;
        }

        // GET: api/<ValuesController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            List<GuestLocationModel> result = new List<GuestLocationModel>();
            
            var guestPertaminaList = await _userManager.GetUsersInRoleAsync("OSMOSYS_GUEST_LOCATION");
            List<GuestLocation> guestLocationList = await _dbOMNI.GuestLocation.Where(b => b.IsDeleted == GeneralConstants.NO).ToListAsync(cancellationToken);

            if(guestPertaminaList.Count() > 0)
            {
                for(int i=0; i < guestPertaminaList.Count(); i++)
                {
                    var findEmployee = await _dbCorePTK.Employees.Where(b => b.IsDeleted == GeneralConstants.NO && b.Email.ToLower() == guestPertaminaList[i].Email.ToLower()).FirstOrDefaultAsync(cancellationToken);
                    if(findEmployee != null)
                    {
                        
                        GuestLocationModel temp = new GuestLocationModel();
                        temp.UserId = findEmployee.Id;
                        temp.UserName = findEmployee.Name;
                        temp.Email = findEmployee.Email;
                        if(guestLocationList.Count() > 0)
                        {
                            var findPort = guestLocationList.FindAll(b => b.UserId == findEmployee.Id).FirstOrDefault();
                            if(findPort != null)
                            {
                                if (!string.IsNullOrEmpty(findPort.Port))
                                {
                                    temp.StrPortList = findPort.Port.Replace("::", ", ");
                                }
                            }
                        }
                        
                        temp.GuestCategory = "Pertamina";
                        result.Add(temp);
                    }
                }
            }

            var guestNonPertaminaList = await _userManager.GetUsersInRoleAsync("OSMOSYS_GUEST_NON_LOCATION");
            if (guestNonPertaminaList.Count() > 0)
            {
                for (int i = 0; i < guestNonPertaminaList.Count(); i++)
                {
                    var findEmployee = await _dbCorePTK.Employees.Where(b => b.IsDeleted == GeneralConstants.NO && b.Email.ToLower() == guestNonPertaminaList[i].Email.ToLower()).FirstOrDefaultAsync(cancellationToken);
                    if (findEmployee != null)
                    {
                        GuestLocationModel temp = new GuestLocationModel();
                        temp.UserId = findEmployee.Id;
                        temp.UserName = findEmployee.Name;
                        temp.Email = findEmployee.Email;
                        if (guestLocationList.Count() > 0)
                        {
                            var findPort = guestLocationList.FindAll(b => b.UserId == findEmployee.Id).FirstOrDefault();
                            if(findPort != null)
                            {
                                if (!string.IsNullOrEmpty(findPort.Port))
                                {
                                    temp.StrPortList = findPort.Port.Replace("::", ", ");
                                }
                            }
                        }
                        temp.GuestCategory = "Non Pertamina";
                        result.Add(temp);
                    }
                }
            }

            if (result.Count() > 0)
            {
                result = result.OrderByDescending(b => b.GuestCategory).ToList();
            }

            return Ok(result);
        }

        // GET: api/<ValuesController>
        [HttpGet("GetByUserId")]
        public async Task<IActionResult> GetByUserId(int id, CancellationToken cancellationToken)
        {
            GuestLocationModel result = new GuestLocationModel();
            var findGuest = await _dbOMNI.GuestLocation.Where(b => b.IsDeleted == GeneralConstants.NO && b.UserId == id).OrderBy(b => b.CreatedAt).FirstOrDefaultAsync(cancellationToken);
            
            result.UserId = id;

            if (findGuest != null)
            {
                result.Id = findGuest.Id;
                var portList = findGuest.Port.Split("::");
                if(portList.Count() > 0)
                {
                    List<string> newPorts = new List<string>();
                    for(int i=0; i < portList.Count(); i++)
                    {
                        newPorts.Add(portList[i]);
                    }
                    result.PortList = newPorts;
                }
            }

            return Ok(result);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _dbOMNI.GuestLocation.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == id).FirstOrDefaultAsync(cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(GuestLocationModel model, CancellationToken cancellationToken)
        {
            GuestLocation data = new GuestLocation();

            string strPorts = null;
            if(model.PortList != null)
            {
                if (model.PortList.Count() > 0)
                {
                    for (int i = 0; i < model.PortList.Count(); i++)
                    {
                        if (i == (model.PortList.Count() - 1))
                        {
                            strPorts += model.PortList[i];
                        }
                        else
                        {
                            strPorts += model.PortList[i] + "::";
                        }
                    }
                }
            }

            if (model.Id > 0)
            {
                data = await _dbOMNI.GuestLocation.Where(b => b.Id == model.Id).FirstOrDefaultAsync(cancellationToken);
                data.Id = model.Id;
                data.UserId = model.UserId;
                data.Port = strPorts;
                data.UpdatedAt = DateTime.Now;
                data.UpdatedBy = "admin";
                _dbOMNI.GuestLocation.Update(data);
                await _dbOMNI.SaveChangesAsync(cancellationToken);
            }
            else
            {
                data.UserId = model.UserId;
                data.Port = strPorts;
                data.CreatedAt = DateTime.Now;
                data.CreatedBy = "admin";
                await _dbOMNI.GuestLocation.AddAsync(data, cancellationToken);
                await _dbOMNI.SaveChangesAsync(cancellationToken);
            }

            return Ok(new ReturnJson { Payload = data });
        }
    }
}
