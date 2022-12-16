using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMNI.API.Model.OMNI;
using OMNI.Data.Data;
using OMNI.Data.Data.Dao;
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
    public class RekomendasiPersonilController : ControllerBase
    {
        private readonly OMNIDbContext _dbOMNI;

        public RekomendasiPersonilController(OMNIDbContext dbOMNI)
        {
            _dbOMNI = dbOMNI;
        }

        // GET: api/<ValuesController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(string port, int year, CancellationToken cancellationToken)
        {
            List<RekomendasiPersonilModel> result = new List<RekomendasiPersonilModel>();
            var list = await _dbOMNI.RekomendasiPersonil.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port && b.Year == year).Include(b => b.RekomendasiType)
                .Include(b => b.Personil).Include(b => b.RekomendasiType).OrderByDescending(b => b.CreatedAt).OrderByDescending(b => b.UpdatedAt).ToListAsync(cancellationToken);
            if (list.Count() > 0)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    RekomendasiPersonilModel temp = new RekomendasiPersonilModel();
                    temp.Id = list[i].Id;
                    temp.Personil = list[i].Personil != null ? list[i].Personil.Name : "-";
                    temp.RekomendasiType = list[i].RekomendasiType != null ? list[i].RekomendasiType.Name : "-";
                    temp.Port = list[i].Port;
                    temp.Value = list[i].Value;
                    temp.CreateDate = list[i].CreatedAt.ToString("dd MMM yyyy");
                    temp.CreatedBy = list[i].CreatedBy;
                    result.Add(temp);
                }
            }

            return Ok(result);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            RekomendasiPersonilModel result = new RekomendasiPersonilModel();
            var data = await _dbOMNI.RekomendasiPersonil.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == id).Include(b => b.RekomendasiType)
                .Include(b => b.Personil).Include(b => b.RekomendasiType).FirstOrDefaultAsync(cancellationToken);
            if (data != null)
            {
                result.Id = data.Id;
                result.Personil = data.Personil != null ? data.Personil.Id.ToString() : "0";
                result.RekomendasiType = data.RekomendasiType != null ? data.RekomendasiType.Id.ToString() : "0";
                result.Port = data.Port;
                result.Value = data.Value;
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(RekomendasiPersonilModel model, CancellationToken cancellationToken)
        {
            RekomendasiPersonil data = new RekomendasiPersonil();

            if (model.Id > 0)
            {
                data = await _dbOMNI.RekomendasiPersonil.Where(b => b.Id == model.Id).Include(b => b.Personil).Include(b => b.RekomendasiType).FirstOrDefaultAsync(cancellationToken);

                data.Personil = await _dbOMNI.Personil.Where(b => b.Id == int.Parse(model.Personil)).FirstOrDefaultAsync(cancellationToken);
                data.RekomendasiType = await _dbOMNI.RekomendasiType.Where(b => b.Id == int.Parse(model.RekomendasiType)).FirstOrDefaultAsync(cancellationToken);
                data.Port = model.Port;
                data.Year = model.Year;
                data.Value = model.Value;
                data.UpdatedAt = DateTime.Now;
                data.UpdatedBy = "admin";
                _dbOMNI.RekomendasiPersonil.Update(data);
                await _dbOMNI.SaveChangesAsync(cancellationToken);

                return Ok(new ReturnJson { });
            }
            else
            {
                var check = await _dbOMNI.RekomendasiPersonil.Where(b => b.IsDeleted == GeneralConstants.NO && b.Personil.Id == int.Parse(model.Personil) && b.Port == model.Port).Include(b => b.Personil).FirstOrDefaultAsync(cancellationToken);

                if (check != null)
                {
                    return Ok(new ReturnJson { IsSuccess = false, ErrorMsg = "Data already exist" });
                }
                else
                {
                    data.Personil = await _dbOMNI.Personil.Where(b => b.Id == int.Parse(model.Personil)).FirstOrDefaultAsync(cancellationToken);
                    data.RekomendasiType = await _dbOMNI.RekomendasiType.Where(b => b.Id == int.Parse(model.RekomendasiType)).FirstOrDefaultAsync(cancellationToken);
                    data.Port = model.Port;
                    data.Year = model.Year;
                    data.Value = model.Value;
                    data.CreatedAt = DateTime.Now;
                    data.CreatedBy = "admin";
                    await _dbOMNI.RekomendasiPersonil.AddAsync(data, cancellationToken);
                    await _dbOMNI.SaveChangesAsync(cancellationToken);

                    return Ok(new ReturnJson { });
                }
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id:int}")]
        public async Task<RekomendasiPersonil> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            RekomendasiPersonil data = await _dbOMNI.RekomendasiPersonil.Where(b => b.Id == id).FirstOrDefaultAsync(cancellationToken);
            data.IsDeleted = GeneralConstants.YES;
            data.UpdatedBy = "admin";
            data.UpdatedAt = DateTime.Now;
            _dbOMNI.RekomendasiPersonil.Update(data);
            await _dbOMNI.SaveChangesAsync(cancellationToken);

            return data;
        }
    }
}
