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

namespace OMNI.API.Controllers.CorePTK
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PortController : ControllerBase
    {
        private readonly CorePTKContext _corePTKDb;

        public PortController(CorePTKContext corePTKDb)
        {
            _corePTKDb = corePTKDb;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _corePTKDb.Port.Where(b => b.IsDeleted == GeneralConstants.NO && b.PAreaSub.Id > 0).Include(b => b.PAreaSub).OrderBy(b => b.Id).ToListAsync(cancellationToken);
                return Ok(result);
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
                return Ok(null);
            }
        }

        [HttpGet("GetPortByRegion")]
        public async Task<IActionResult> GetPortByRegion(int regionId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _corePTKDb.Port.Where(b => b.IsDeleted == GeneralConstants.NO && b.PAreaSub.Id > 0 && b.PAreaSub.PArea.Region.Id == regionId).Include(b => b.PAreaSub).Include(b =>b.PAreaSub.PArea).Include(b => b.PAreaSub.PArea.Region).OrderBy(b => b.Id).ToListAsync(cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Ok(null);
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _corePTKDb.Port.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == id).FirstOrDefaultAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("GetPortRegion")]
        public async Task<IActionResult> GetPortRegion(string port, CancellationToken cancellationToken)
        {
            string result = "";
            var data = await _corePTKDb.Port.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == int.Parse(port)).Include(b => b.PAreaSub).Include(b => b.PAreaSub.PArea).Include(b => b.PAreaSub.PArea.Region).FirstOrDefaultAsync(cancellationToken);
            if(data != null)
            {
                if(data.PAreaSub != null)
                {
                    if(data.PAreaSub.PArea != null)
                    {
                        if(data.PAreaSub.PArea.Region != null)
                        {
                            result = data.PAreaSub.PArea.Region.Code;
                        }
                    }
                }
            }

            return Ok(result);
        }

    }
}
