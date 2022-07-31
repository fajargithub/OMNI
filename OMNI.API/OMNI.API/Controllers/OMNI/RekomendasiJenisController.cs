﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetAll(string port, CancellationToken cancellationToken)
        {
            List<RekomendasiJenisModel> result = new List<RekomendasiJenisModel>();
            var list = await _dbOMNI.RekomendasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port).Include(b => b.RekomendasiType)
                .Include(b => b.SpesifikasiJenis).Include(b => b.SpesifikasiJenis.PeralatanOSR).Include(b => b.SpesifikasiJenis.Jenis).Include(b => b.RekomendasiType).OrderByDescending(b => b.CreatedAt).OrderByDescending(b => b.UpdatedAt).ToListAsync(cancellationToken);
            if (list.Count() > 0)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    RekomendasiJenisModel temp = new RekomendasiJenisModel();
                    temp.Id = list[i].Id;
                    temp.RekomendasiType = list[i].RekomendasiType != null ? list[i].RekomendasiType.Name : "-";
                    temp.PeralatanOSR = list[i].SpesifikasiJenis != null ? list[i].SpesifikasiJenis.PeralatanOSR.Name : "-";
                    temp.Jenis = list[i].SpesifikasiJenis != null ? list[i].SpesifikasiJenis.Jenis.Name : "-";
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
            RekomendasiJenisModel result = new RekomendasiJenisModel();
            var data = await _dbOMNI.RekomendasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == id).Include(b => b.RekomendasiType)
                .Include(b => b.SpesifikasiJenis).Include(b => b.SpesifikasiJenis.PeralatanOSR).Include(b => b.SpesifikasiJenis.Jenis).Include(b => b.RekomendasiType).FirstOrDefaultAsync(cancellationToken);
            if (data != null)
            {
                result.Id = data.Id;
                result.PeralatanOSR = data.SpesifikasiJenis != null ? data.SpesifikasiJenis.PeralatanOSR.Id.ToString() : "0";
                result.Jenis = data.SpesifikasiJenis != null ? data.SpesifikasiJenis.Jenis.Id.ToString() : "0";
                result.RekomendasiType = data.RekomendasiType != null ? data.RekomendasiType.Id.ToString() : "0";
                result.Port = data.Port;
                result.Value = data.Value;
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(RekomendasiJenisModel model, CancellationToken cancellationToken)
        {
            RekomendasiJenis data = new RekomendasiJenis();

            var findSpesifikasiJenis = await _dbOMNI.SpesifikasiJenis.Where(b => b.PeralatanOSR.Id == int.Parse(model.PeralatanOSR) && b.Jenis.Id == int.Parse(model.Jenis)).FirstOrDefaultAsync(cancellationToken);
            if(findSpesifikasiJenis != null)
            {
                if (model.Id > 0)
                {
                    data = await _dbOMNI.RekomendasiJenis.Where(b => b.Id == model.Id).Include(b => b.SpesifikasiJenis)
                        .Include(b => b.SpesifikasiJenis.PeralatanOSR).Include(b => b.SpesifikasiJenis.Jenis).Include(b => b.RekomendasiType).FirstOrDefaultAsync(cancellationToken);

                    data.SpesifikasiJenis = await _dbOMNI.SpesifikasiJenis.Where(b => b.Id == findSpesifikasiJenis.Id).FirstOrDefaultAsync(cancellationToken);
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
                    data.SpesifikasiJenis = await _dbOMNI.SpesifikasiJenis.Where(b => b.Id == findSpesifikasiJenis.Id).FirstOrDefaultAsync(cancellationToken);
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
