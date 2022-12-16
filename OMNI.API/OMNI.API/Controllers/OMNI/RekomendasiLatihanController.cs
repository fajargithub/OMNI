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
    public class RekomendasiLatihanController : ControllerBase
    {
        private readonly OMNIDbContext _dbOMNI;

        public RekomendasiLatihanController(OMNIDbContext dbOMNI)
        {
            _dbOMNI = dbOMNI;
        }

        // GET: api/<ValuesController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(string port, int year, CancellationToken cancellationToken)
        {
            List<RekomendasiLatihanModel> result = new List<RekomendasiLatihanModel>();
            var list = await _dbOMNI.RekomendasiLatihan.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port && b.Year == year).Include(b => b.RekomendasiType)
                .Include(b => b.Latihan).Include(b => b.RekomendasiType).OrderByDescending(b => b.CreatedAt).OrderByDescending(b => b.UpdatedAt).ToListAsync(cancellationToken);
            if (list.Count() > 0)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    RekomendasiLatihanModel temp = new RekomendasiLatihanModel();
                    temp.Id = list[i].Id;
                    temp.Latihan = list[i].Latihan != null ? list[i].Latihan.Name : "-";
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
            RekomendasiLatihanModel result = new RekomendasiLatihanModel();
            var data = await _dbOMNI.RekomendasiLatihan.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == id).Include(b => b.RekomendasiType)
                .Include(b => b.Latihan).Include(b => b.RekomendasiType).FirstOrDefaultAsync(cancellationToken);
            if (data != null)
            {
                result.Id = data.Id;
                result.Latihan = data.Latihan != null ? data.Latihan.Id.ToString() : "0";
                result.RekomendasiType = data.RekomendasiType != null ? data.RekomendasiType.Id.ToString() : "0";
                result.Port = data.Port;
                result.Value = data.Value;
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(RekomendasiLatihanModel model, CancellationToken cancellationToken)
        {
            RekomendasiLatihan data = new RekomendasiLatihan();

            if (model.Id > 0)
            {
                data = await _dbOMNI.RekomendasiLatihan.Where(b => b.Id == model.Id).Include(b => b.Latihan).Include(b => b.RekomendasiType).FirstOrDefaultAsync(cancellationToken);

                data.Latihan = await _dbOMNI.Latihan.Where(b => b.Id == int.Parse(model.Latihan)).FirstOrDefaultAsync(cancellationToken);
                data.RekomendasiType = await _dbOMNI.RekomendasiType.Where(b => b.Id == int.Parse(model.RekomendasiType)).FirstOrDefaultAsync(cancellationToken);
                data.Port = model.Port;
                data.Year = model.Year;
                data.Value = model.Value;
                data.UpdatedAt = DateTime.Now;
                data.UpdatedBy = "admin";
                _dbOMNI.RekomendasiLatihan.Update(data);
                await _dbOMNI.SaveChangesAsync(cancellationToken);

                return Ok(new ReturnJson { });
            }
            else
            {
                var check = await _dbOMNI.RekomendasiLatihan.Where(b => b.IsDeleted == GeneralConstants.NO && b.Latihan.Id == int.Parse(model.Latihan) && b.Port == model.Port).Include(b => b.Latihan).FirstOrDefaultAsync(cancellationToken);
                if (check != null)
                {
                    return Ok(new ReturnJson { IsSuccess = false, ErrorMsg = "Data already exist" });
                }
                else
                {
                    data.Latihan = await _dbOMNI.Latihan.Where(b => b.Id == int.Parse(model.Latihan)).FirstOrDefaultAsync(cancellationToken);
                    data.RekomendasiType = await _dbOMNI.RekomendasiType.Where(b => b.Id == int.Parse(model.RekomendasiType)).FirstOrDefaultAsync(cancellationToken);
                    data.Port = model.Port;
                    data.Year = model.Year;
                    data.Value = model.Value;
                    data.CreatedAt = DateTime.Now;
                    data.CreatedBy = "admin";
                    await _dbOMNI.RekomendasiLatihan.AddAsync(data, cancellationToken);
                    await _dbOMNI.SaveChangesAsync(cancellationToken);

                    return Ok(new ReturnJson { });
                }
            }

            
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id:int}")]
        public async Task<RekomendasiLatihan> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            RekomendasiLatihan data = await _dbOMNI.RekomendasiLatihan.Where(b => b.Id == id).FirstOrDefaultAsync(cancellationToken);
            data.IsDeleted = GeneralConstants.YES;
            data.UpdatedBy = "admin";
            data.UpdatedAt = DateTime.Now;
            _dbOMNI.RekomendasiLatihan.Update(data);
            await _dbOMNI.SaveChangesAsync(cancellationToken);

            return data;
        }
    }
}
