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
    public class RekomendasiJenisController : ControllerBase
    {
        private readonly OMNIDbContext _dbOMNI;

        public RekomendasiJenisController(OMNIDbContext dbOMNI)
        {
            _dbOMNI = dbOMNI;
        }

        // GET: api/<ValuesController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(string port, string typeId, CancellationToken cancellationToken)
        {
            List<RekomendasiJenisModel> result = new List<RekomendasiJenisModel>();
            //var list = await _dbOMNI.RekomendasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port).Include(b => b.RekomendasiType)
            //    .Include(b => b.SpesifikasiJenis).Include(b => b.SpesifikasiJenis.PeralatanOSR).Include(b => b.SpesifikasiJenis.Jenis).Include(b => b.RekomendasiType).OrderByDescending(b => b.CreatedAt).OrderByDescending(b => b.UpdatedAt).ToListAsync(cancellationToken);

            var list = await _dbOMNI.SpesifikasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO).Include(b => b.PeralatanOSR).Include(b => b.Jenis).OrderBy(b => b.CreatedAt).ToListAsync(cancellationToken);
            var rekomenJenisList = await _dbOMNI.RekomendasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port && b.RekomendasiType.Id == int.Parse(typeId)).Include(b => b.RekomendasiType)
                .Include(b => b.SpesifikasiJenis).Include(b => b.SpesifikasiJenis.PeralatanOSR).Include(b => b.SpesifikasiJenis.Jenis).Include(b => b.RekomendasiType).OrderBy(b => b.CreatedAt).ToListAsync(cancellationToken);

            if (list.Count() > 0)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    RekomendasiJenisModel temp = new RekomendasiJenisModel();
                    //temp.Id = list[i].Id;
                    //temp.PeralatanOSR = list[i].SpesifikasiJenis != null ? list[i].SpesifikasiJenis.PeralatanOSR.Name : "-";
                    //temp.Jenis = list[i].SpesifikasiJenis != null ? list[i].SpesifikasiJenis.Jenis.Name : "-";
                    //temp.RekomendasiType = list[i].RekomendasiType != null ? list[i].RekomendasiType.Name : "-";
                    //temp.Port = list[i].Port;
                    //temp.Value = list[i].Value;
                    //temp.CreateDate = list[i].CreatedAt.ToString("dd MMM yyyy");
                    //temp.CreatedBy = list[i].CreatedBy;
                    //result.Add(temp);

                    temp.Id = list[i].Id;
                    temp.PeralatanOSR = list[i].PeralatanOSR != null ? list[i].PeralatanOSR.Name : "-";
                    temp.Jenis = list[i].Jenis != null ? list[i].Jenis.Name : "-";

                    var findRekomenJenis = rekomenJenisList.Find(b => b.SpesifikasiJenis.Id == list[i].Id);
                    if(findRekomenJenis != null)
                    {
                        temp.RekomendasiJenisId = findRekomenJenis != null ? findRekomenJenis.Id : 0;
                        temp.RekomendasiType = findRekomenJenis.RekomendasiType != null ? findRekomenJenis.RekomendasiType.Name : "-";
                        temp.Port = findRekomenJenis.Port;
                        temp.Value = findRekomenJenis.Value;
                        temp.CreateDate = findRekomenJenis.CreatedAt.ToString("dd MMM yyyy");
                        temp.CreatedBy = findRekomenJenis.CreatedBy;
                    }
                    
                    result.Add(temp);
                }
            }

            return Ok(result);
        }

        // GET api/<ValuesController>/5
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id, string port, string typeId, CancellationToken cancellationToken)
        {
            RekomendasiJenisModel result = new RekomendasiJenisModel();
            var data = await _dbOMNI.RekomendasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO && b.SpesifikasiJenis.Id == int.Parse(id) && b.Port == port && b.RekomendasiType.Id == int.Parse(typeId)).Include(b => b.RekomendasiType)
                .Include(b => b.SpesifikasiJenis).Include(b => b.SpesifikasiJenis.PeralatanOSR).Include(b => b.SpesifikasiJenis.Jenis).Include(b => b.RekomendasiType).FirstOrDefaultAsync(cancellationToken);

            var findSpesifikasiJenis = await _dbOMNI.SpesifikasiJenis.Where(b => b.Id == int.Parse(id)).Include(b => b.PeralatanOSR).Include(b => b.Jenis).FirstOrDefaultAsync(cancellationToken);

            result.PeralatanOSR = findSpesifikasiJenis != null ? findSpesifikasiJenis.PeralatanOSR.Name : "0";
            result.Jenis = findSpesifikasiJenis != null ? findSpesifikasiJenis.Jenis.Name : "0";

            var findRekomendasiType = await _dbOMNI.RekomendasiType.Where(b => b.Id == int.Parse(typeId)).FirstOrDefaultAsync(cancellationToken);

            result.RekomendasiType = findRekomendasiType != null ? findRekomendasiType.Id.ToString() : "0";
            result.TypeName = findRekomendasiType != null ? findRekomendasiType.Name : "-";

            if (data != null)
            {
                result.Id = data.Id;
                result.Port = data.Port;
                result.Value = data.Value;
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(RekomendasiJenisModel model, CancellationToken cancellationToken)
        {
            RekomendasiJenis data = new RekomendasiJenis();

            //var findSpesifikasiJenis = await _dbOMNI.SpesifikasiJenis.Where(b => b.PeralatanOSR.Id == int.Parse(model.PeralatanOSR) && b.Jenis.Id == int.Parse(model.Jenis)).FirstOrDefaultAsync(cancellationToken);
            var findSpesifikasiJenis = await _dbOMNI.SpesifikasiJenis.Where(b => b.Id == model.Id).FirstOrDefaultAsync(cancellationToken);
            var rekomendasiJenis = await _dbOMNI.RekomendasiJenis.Where(b => b.Port == model.Port && b.SpesifikasiJenis.Id == model.Id && b.RekomendasiType.Id == int.Parse(model.RekomendasiType)).Include(b => b.SpesifikasiJenis).Include(b => b.RekomendasiType).FirstOrDefaultAsync(cancellationToken);
            if (findSpesifikasiJenis != null)
            {
                if (rekomendasiJenis != null)
                {
                    data = await _dbOMNI.RekomendasiJenis.Where(b => b.Id == rekomendasiJenis.Id).Include(b => b.SpesifikasiJenis)
                        .Include(b => b.SpesifikasiJenis.PeralatanOSR).Include(b => b.SpesifikasiJenis.Jenis).Include(b => b.RekomendasiType).FirstOrDefaultAsync(cancellationToken);

                    data.SpesifikasiJenis = findSpesifikasiJenis;
                    data.RekomendasiType = await _dbOMNI.RekomendasiType.Where(b => b.Id == int.Parse(model.RekomendasiType)).FirstOrDefaultAsync(cancellationToken);
                    data.Port = model.Port;
                    data.Value = model.Value;
                    data.UpdatedAt = DateTime.Now;
                    data.UpdatedBy = "admin";
                    _dbOMNI.RekomendasiJenis.Update(data);
                    await _dbOMNI.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    data.SpesifikasiJenis = findSpesifikasiJenis;
                    data.RekomendasiType = await _dbOMNI.RekomendasiType.Where(b => b.Id == int.Parse(model.RekomendasiType)).FirstOrDefaultAsync(cancellationToken);
                    data.Port = model.Port;
                    data.Value = model.Value;
                    data.CreatedAt = DateTime.Now;
                    data.CreatedBy = "admin";
                    await _dbOMNI.RekomendasiJenis.AddAsync(data, cancellationToken);
                    await _dbOMNI.SaveChangesAsync(cancellationToken);
                }
            }
            

            return Ok(new ReturnJson { });
        }

        [HttpGet("UpdateValue")]
        public async Task<IActionResult> UpdateValue(string id, string port, string typeId, string value, CancellationToken cancellationToken)
        {
            //RekomendasiJenis data = await _dbOMNI.RekomendasiJenis.Where(b => b.Id == int.Parse(id)).FirstOrDefaultAsync(cancellationToken);
            //data.Value = decimal.Parse(value);
            //data.UpdatedBy = "admin";
            //data.UpdatedAt = DateTime.Now;
            //_dbOMNI.RekomendasiJenis.Update(data);
            //await _dbOMNI.SaveChangesAsync(cancellationToken);

            //return data;

            RekomendasiJenis data = new RekomendasiJenis();

            //var findSpesifikasiJenis = await _dbOMNI.SpesifikasiJenis.Where(b => b.PeralatanOSR.Id == int.Parse(model.PeralatanOSR) && b.Jenis.Id == int.Parse(model.Jenis)).FirstOrDefaultAsync(cancellationToken);
            var findSpesifikasiJenis = await _dbOMNI.SpesifikasiJenis.Where(b => b.Id == int.Parse(id)).FirstOrDefaultAsync(cancellationToken);
            var rekomendasiJenis = await _dbOMNI.RekomendasiJenis.Where(b => b.Port == port && b.SpesifikasiJenis.Id == int.Parse(id) && b.RekomendasiType.Id == int.Parse(typeId)).Include(b => b.SpesifikasiJenis).Include(b => b.RekomendasiType).FirstOrDefaultAsync(cancellationToken);
            if (findSpesifikasiJenis != null)
            {
                if (rekomendasiJenis != null)
                {
                    data = await _dbOMNI.RekomendasiJenis.Where(b => b.Id == rekomendasiJenis.Id).Include(b => b.SpesifikasiJenis)
                        .Include(b => b.SpesifikasiJenis.PeralatanOSR).Include(b => b.SpesifikasiJenis.Jenis).Include(b => b.RekomendasiType).FirstOrDefaultAsync(cancellationToken);

                    data.SpesifikasiJenis = findSpesifikasiJenis;
                    data.RekomendasiType = await _dbOMNI.RekomendasiType.Where(b => b.Id == int.Parse(typeId)).FirstOrDefaultAsync(cancellationToken);
                    data.Port = port;
                    data.Value = decimal.Parse(value);
                    data.UpdatedAt = DateTime.Now;
                    data.UpdatedBy = "admin";
                    _dbOMNI.RekomendasiJenis.Update(data);
                    await _dbOMNI.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    data.SpesifikasiJenis = findSpesifikasiJenis;
                    data.RekomendasiType = await _dbOMNI.RekomendasiType.Where(b => b.Id == int.Parse(typeId)).FirstOrDefaultAsync(cancellationToken);
                    data.Port = port;
                    data.Value = decimal.Parse(value);
                    data.CreatedAt = DateTime.Now;
                    data.CreatedBy = "admin";
                    await _dbOMNI.RekomendasiJenis.AddAsync(data, cancellationToken);
                    await _dbOMNI.SaveChangesAsync(cancellationToken);
                }
            }


            return Ok(new ReturnJson { });
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id:int}")]
        public async Task<RekomendasiJenis> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            RekomendasiJenis data = await _dbOMNI.RekomendasiJenis.Where(b => b.Id == id).FirstOrDefaultAsync(cancellationToken);
            data.IsDeleted = GeneralConstants.YES;
            data.UpdatedBy = "admin";
            data.UpdatedAt = DateTime.Now;
            _dbOMNI.RekomendasiJenis.Update(data);
            await _dbOMNI.SaveChangesAsync(cancellationToken);

            return data;
        }
    }
}
