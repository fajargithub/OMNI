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
    public class SpesifikasiJenisController : ControllerBase
    {
        private readonly OMNIDbContext _dbOMNI;

        public SpesifikasiJenisController(OMNIDbContext dbOMNI)
        {
            _dbOMNI = dbOMNI;
        }

        // GET: api/<ValuesController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            List<SpesifikasiJenisModel> result = new List<SpesifikasiJenisModel>();
            var list = await _dbOMNI.SpesifikasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO).Include(b => b.PeralatanOSR).Include(b => b.Jenis).OrderBy(b => b.CreatedAt).ToListAsync(cancellationToken);
            if(list.Count() > 0)
            {
                for(int i=0; i < list.Count(); i++)
                {
                    SpesifikasiJenisModel temp = new SpesifikasiJenisModel();
                    temp.Id = list[i].Id;
                    temp.PeralatanOSR = list[i].PeralatanOSR != null ? list[i].PeralatanOSR.Name : "-";
                    temp.Jenis = list[i].Jenis != null ? list[i].Jenis.Name : "-";
                    temp.CreateDate = list[i].CreatedAt.ToString("dd MMM yyyy");
                    temp.CreatedBy = list[i].CreatedBy;
                    result.Add(temp);
                }
            }

            return Ok(result);
        }

        [HttpGet("GetAllSpesifikasiJenisByPeralatanOSR")]
        public async Task<IActionResult> GetAllSpesifikasiJenisByPeralatanOSR(string peralatanOSRId, CancellationToken cancellationToken)
        {
            List<SpesifikasiJenisModel> result = new List<SpesifikasiJenisModel>();
            var data = await _dbOMNI.SpesifikasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO && b.PeralatanOSR.Id == int.Parse(peralatanOSRId)).Include(b => b.PeralatanOSR).Include(b => b.Jenis).ToListAsync(cancellationToken);
            if (data != null)
            {
                for(int i=0; i < data.Count(); i++)
                {
                    SpesifikasiJenisModel temp = new SpesifikasiJenisModel();
                    temp.Id = data[i].Id;
                    temp.Jenis = data[i].Jenis.Name;
                    result.Add(temp);
                }
            }
            return Ok(result);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            SpesifikasiJenisModel result = new SpesifikasiJenisModel();
            var data = await _dbOMNI.SpesifikasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == id).Include(b => b.PeralatanOSR).Include(b => b.Jenis).FirstOrDefaultAsync(cancellationToken);
            if(data != null)
            {
                result.Id = data.Id;
                result.PeralatanOSR = data.PeralatanOSR != null ? data.PeralatanOSR.Id.ToString() : "0";
                result.Jenis = data.Jenis != null ? data.Jenis.Id.ToString() : "0";
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(SpesifikasiJenisModel model, CancellationToken cancellationToken)
        {
            SpesifikasiJenis data = new SpesifikasiJenis();
            if (model.Id > 0)
            {
                data = await _dbOMNI.SpesifikasiJenis.Where(b => b.Id == model.Id).Include(b => b.PeralatanOSR).FirstOrDefaultAsync(cancellationToken);
                data.PeralatanOSR = await _dbOMNI.PeralatanOSR.Where(b => b.Id == int.Parse(model.PeralatanOSR)).FirstOrDefaultAsync(cancellationToken);
                data.Jenis = await _dbOMNI.Jenis.Where(b => b.Id == int.Parse(model.Jenis)).FirstOrDefaultAsync(cancellationToken);
                data.UpdatedAt = DateTime.Now;
                data.UpdatedBy = "admin";
                _dbOMNI.SpesifikasiJenis.Update(data);
                await _dbOMNI.SaveChangesAsync(cancellationToken);
            }
            else
            {
                data.PeralatanOSR = await _dbOMNI.PeralatanOSR.Where(b => b.Id == int.Parse(model.PeralatanOSR)).FirstOrDefaultAsync(cancellationToken);
                data.Jenis = await _dbOMNI.Jenis.Where(b => b.Id == int.Parse(model.Jenis)).FirstOrDefaultAsync(cancellationToken);
                data.CreatedAt = DateTime.Now;
                data.CreatedBy = "admin";
                await _dbOMNI.SpesifikasiJenis.AddAsync(data, cancellationToken);
                await _dbOMNI.SaveChangesAsync(cancellationToken);
            }

            return Ok(new ReturnJson { });
        }

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
