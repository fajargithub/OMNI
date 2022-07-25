using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMNI.API.Model.OMNI;
using OMNI.Data.Data;
using OMNI.Data.Data.Dao;
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
    public class SpesifikasiJenisController : ControllerBase
    {
        private readonly OMNIDbContext _dbOMNI;

        public SpesifikasiJenisController(OMNIDbContext dbOMNI)
        {
            _dbOMNI = dbOMNI;
        }

        // GET: api/<ValuesController>
        //[HttpGet("GetAllByPortId")]
        //public async Task<IActionResult> GetAllByPortId(int id, CancellationToken cancellationToken)
        //{
        //    var result = await _dbOMNI.SpesifikasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO && b.PortId == id).Include(b => b.PeralatanOSR).OrderByDescending(b => b.CreatedAt).OrderByDescending(b => b.UpdatedAt).ToListAsync(cancellationToken);
        //    return Ok(result);
        //}

        // GET api/<ValuesController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _dbOMNI.SpesifikasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == id).Include(b => b.PeralatanOSR).FirstOrDefaultAsync(cancellationToken);
            return Ok(result);
        }

        //[HttpPost]
        //public async Task<IActionResult> AddEdit(SpesifikasiJenisModel model, CancellationToken cancellationToken)
        //{
        //    SpesifikasiJenis data = new SpesifikasiJenis();
        //    if (model.Id > 0)
        //    {
        //        data = await _dbOMNI.SpesifikasiJenis.Where(b => b.Id == model.Id).Include(b => b.PeralatanOSR).FirstOrDefaultAsync(cancellationToken);
        //        data.PeralatanOSR = await _dbOMNI.PeralatanOSR.Where(b => b.Id == int.Parse(model.PeralatanOSR)).FirstOrDefaultAsync(cancellationToken);
        //        data.Name = model.Name;
        //        data.PortId = int.Parse(model.Port);
        //        if (!string.IsNullOrEmpty(model.QRCode))
        //        {
        //            data.QRCode = model.QRCode;
        //        }
        //        data.RekomendasiHubla = model.RekomendasiHubla;
        //        data.Desc = model.Desc;
        //        data.UpdatedAt = DateTime.Now;
        //        data.UpdatedBy = "admin";
        //        _dbOMNI.SpesifikasiJenis.Update(data);
        //        await _dbOMNI.SaveChangesAsync(cancellationToken);
        //    }
        //    else
        //    {
        //        data.PeralatanOSR = await _dbOMNI.PeralatanOSR.Where(b => b.Id == int.Parse(model.PeralatanOSR)).FirstOrDefaultAsync(cancellationToken);
        //        data.Name = model.Name;
        //        data.PortId = int.Parse(model.Port);
        //        if (!string.IsNullOrEmpty(model.QRCode))
        //        {
        //            data.QRCode = model.QRCode;
        //        }
        //        data.RekomendasiHubla = model.RekomendasiHubla;
        //        data.Desc = model.Desc;
        //        data.CreatedAt = DateTime.Now;
        //        data.UpdatedBy = "admin";
        //        await _dbOMNI.SpesifikasiJenis.AddAsync(data, cancellationToken);
        //        await _dbOMNI.SaveChangesAsync(cancellationToken);
        //    }

        //    return Ok(new ReturnJson { });
        //}

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id:int}")]
        public async Task<SpesifikasiJenis> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            SpesifikasiJenis data = await _dbOMNI.SpesifikasiJenis.Where(b => b.Id == id).FirstOrDefaultAsync(cancellationToken);
            data.IsDeleted = GeneralConstants.YES;
            data.UpdatedBy = "admin";
            data.UpdatedAt = DateTime.Now;
            _dbOMNI.SpesifikasiJenis.Update(data);
            await _dbOMNI.SaveChangesAsync(cancellationToken);

            return data;
        }
    }
}
