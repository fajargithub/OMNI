using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMNI.API.Model.OMNI;
using OMNI.Data.Data;
using OMNI.Data.Data.Dao;
using OMNI.Migrations.Data.Dao;
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
    public class LampiranController : ControllerBase
    {
        private readonly OMNIDbContext _dbOMNI;

        public LampiranController(OMNIDbContext dbOMNI)
        {
            _dbOMNI = dbOMNI;
        }

        // GET: api/<ValuesController>
        [HttpGet("GetByTrxId")]
        public async Task<IActionResult> GetByTrxId(int id, string lampiranType, CancellationToken cancellationToken)
        {
            var result = await _dbOMNI.Lampiran.Where(b => b.IsDeleted == GeneralConstants.NO && b.LLPTrx.Id == id && b.LampiranType == lampiranType).Include(b => b.LLPTrx).OrderByDescending(b => b.CreatedAt).OrderByDescending(b => b.UpdatedAt).ToListAsync(cancellationToken);
            return Ok(result);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _dbOMNI.Lampiran.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == id).FirstOrDefaultAsync(cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(LampiranModel model, CancellationToken cancellationToken)
        {
            DateTime nullDate = new DateTime();
            Lampiran data = new Lampiran();
            if (model.Id > 0)
            {
                data = await _dbOMNI.Lampiran.Where(b => b.Id == model.Id).FirstOrDefaultAsync(cancellationToken);
                data.LampiranType = model.LampiranType;
                data.StartDate = !string.IsNullOrEmpty(model.StartDate) ? DateTime.ParseExact("dd/MM/yyyy", model.StartDate, null) : nullDate;
                data.EndDate = !string.IsNullOrEmpty(model.StartDate) ? DateTime.ParseExact("dd/MM/yyyy", model.StartDate, null) : nullDate;
                data.Remark = model.Remark;
                data.UpdatedAt = DateTime.Now;
                data.UpdatedBy = "admin";
                _dbOMNI.Lampiran.Update(data);
                await _dbOMNI.SaveChangesAsync(cancellationToken);
            }
            else
            {
                data.LampiranType = model.LampiranType;
                data.StartDate = !string.IsNullOrEmpty(model.StartDate) ? DateTime.ParseExact("dd/MM/yyyy", model.StartDate, null) : nullDate;
                data.EndDate = !string.IsNullOrEmpty(model.StartDate) ? DateTime.ParseExact("dd/MM/yyyy", model.StartDate, null) : nullDate;
                data.Remark = model.Remark;
                data.CreatedAt = DateTime.Now;
                data.UpdatedBy = "admin";
                await _dbOMNI.Lampiran.AddAsync(data, cancellationToken);
                await _dbOMNI.SaveChangesAsync(cancellationToken);
            }

            return Ok(new ReturnJson { Payload = data });
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id:int}")]
        public async Task<Lampiran> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            Lampiran data = await _dbOMNI.Lampiran.Where(b => b.Id == id).FirstOrDefaultAsync(cancellationToken);
            data.IsDeleted = GeneralConstants.YES;
            data.UpdatedBy = "admin";
            data.UpdatedAt = DateTime.Now;
            _dbOMNI.Lampiran.Update(data);
            await _dbOMNI.SaveChangesAsync(cancellationToken);

            return data;
        }
    }
}
