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
    public class AdminLocationController : ControllerBase
    {
        private readonly OMNIDbContext _dbOMNI;
        private readonly CorePTKContext _dbCorePTK;
        protected UserManager<ApplicationUser> _userManager;

        public AdminLocationController(OMNIDbContext dbOMNI, CorePTKContext dbCorePTK, UserManager<ApplicationUser> userManager)
        {
            _dbOMNI = dbOMNI;
            _dbCorePTK = dbCorePTK;
            _userManager = userManager;
        }

        // GET: api/<ValuesController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            List<AdminLocationModel> result = new List<AdminLocationModel>();

            var adminList = await _userManager.GetUsersInRoleAsync("OSMOSYS_ADMIN_LOKASI");
            List<AdminLocation> AdminLocationList = await _dbOMNI.AdminLocation.Where(b => b.IsDeleted == GeneralConstants.NO).ToListAsync(cancellationToken);

            if (adminList.Count() > 0)
            {
                for (int i = 0; i < adminList.Count(); i++)
                {
                    var findEmployee = await _dbCorePTK.Employees.Where(b => b.IsDeleted == GeneralConstants.NO && b.Email.ToLower() == adminList[i].Email.ToLower()).FirstOrDefaultAsync(cancellationToken);
                    if (findEmployee != null)
                    {

                        AdminLocationModel temp = new AdminLocationModel();
                        temp.UserId = findEmployee.Id;
                        temp.UserName = findEmployee.Name;
                        temp.Email = findEmployee.Email;
                        if (AdminLocationList.Count() > 0)
                        {
                            var findPort = AdminLocationList.FindAll(b => b.UserId == findEmployee.Id).FirstOrDefault();
                            if (findPort != null)
                            {
                                if (!string.IsNullOrEmpty(findPort.Port))
                                {
                                    temp.StrPortList = findPort.Port.Replace("::", ", ");
                                }
                            }
                        }

                        result.Add(temp);
                    }
                }
            }

            return Ok(result);
        }

        // GET: api/<ValuesController>
        [HttpGet("GetByUserId")]
        public async Task<IActionResult> GetByUserId(int id, CancellationToken cancellationToken)
        {
            AdminLocationModel result = new AdminLocationModel();
            var findGuest = await _dbOMNI.AdminLocation.Where(b => b.IsDeleted == GeneralConstants.NO && b.UserId == id).OrderBy(b => b.CreatedAt).FirstOrDefaultAsync(cancellationToken);

            result.UserId = id;

            if (findGuest != null)
            {
                result.Id = findGuest.Id;
                var portList = findGuest.Port.Split("::");
                if (portList.Count() > 0)
                {
                    List<string> newPorts = new List<string>();
                    for (int i = 0; i < portList.Count(); i++)
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
            var result = await _dbOMNI.AdminLocation.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == id).FirstOrDefaultAsync(cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(AdminLocationModel model, CancellationToken cancellationToken)
        {
            AdminLocation data = new AdminLocation();

            string strPorts = null;
            if (model.PortList != null)
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
                data = await _dbOMNI.AdminLocation.Where(b => b.Id == model.Id).FirstOrDefaultAsync(cancellationToken);
                data.Id = model.Id;
                data.UserId = model.UserId;
                data.Port = strPorts;
                data.UpdatedAt = DateTime.Now;
                data.UpdatedBy = "admin";
                _dbOMNI.AdminLocation.Update(data);
                await _dbOMNI.SaveChangesAsync(cancellationToken);
            }
            else
            {
                data.UserId = model.UserId;
                data.Port = strPorts;
                data.CreatedAt = DateTime.Now;
                data.CreatedBy = "admin";
                await _dbOMNI.AdminLocation.AddAsync(data, cancellationToken);
                await _dbOMNI.SaveChangesAsync(cancellationToken);
            }

            return Ok(new ReturnJson { Payload = data });
        }
    }
}
