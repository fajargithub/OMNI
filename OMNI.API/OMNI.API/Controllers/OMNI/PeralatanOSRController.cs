using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMNI.API.Model.OMNI;
using OMNI.Data.Data;
using OMNI.Data.Data.Dao;
using OMNI.Domain.AppLogRepo;
using OMNI.Utilities.Base;
using OMNI.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OMNI.API.Controllers.OMNI
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeralatanOSRController : ControllerBase
    {
        private readonly OMNIDbContext _dbOMNI;

        public PeralatanOSRController(OMNIDbContext dbOMNI) 
        {
            _dbOMNI = dbOMNI;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _dbOMNI.PeralatanOSR.Where(b => b.IsDeleted == GeneralConstants.NO).ToListAsync(cancellationToken);
            return Ok(result);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _dbOMNI.PeralatanOSR.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == id).FirstOrDefaultAsync(cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(PeralatanOSRModel model, CancellationToken cancellationToken)
        {
            try
            {
                PeralatanOSR data = new PeralatanOSR();
                if(model.Id > 0)
                {
                    data = await _dbOMNI.PeralatanOSR.Where(b => b.Id == model.Id).FirstOrDefaultAsync(cancellationToken);
                    data.Name = model.Name;
                    data.Desc = model.Desc;
                    data.UpdatedAt = DateTime.Now;
                    data.UpdatedBy = "admin";
                    _dbOMNI.PeralatanOSR.Update(data);
                    await _dbOMNI.SaveChangesAsync(cancellationToken);
                } else
                {
                    data.Name = model.Name;
                    data.Desc = model.Desc;
                    data.CreatedAt = DateTime.Now;
                    data.UpdatedBy = "admin";
                    await _dbOMNI.PeralatanOSR.AddAsync(data, cancellationToken);
                    await _dbOMNI.SaveChangesAsync(cancellationToken);
                }

                return Ok(new ReturnJson { Payload = data });
            }
            catch (DomainLayerException e)
            {
                //await SaveAppLog(GetCurrentMethod(), model.Name, GeneralConstants.FAILED, cancellationToken, errorMessage: e.InnerException?.Message ?? e.Message, info: "API");
                return StatusCode(500, new ReturnJson { ErrorMsg = e.Message, Code = 500, IsSuccess = false });
            }
            catch (Exception e)
            {
                //await SaveAppLog(GetCurrentMethod(), model.Name, GeneralConstants.FAILED, cancellationToken, errorMessage: e.InnerException?.Message ?? e.Message, info: "API");
                throw;
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
