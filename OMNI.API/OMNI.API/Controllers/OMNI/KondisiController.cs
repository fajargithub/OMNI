﻿using Microsoft.AspNetCore.Mvc;
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
    public class KondisiController : ControllerBase
    {
        private readonly OMNIDbContext _dbOMNI;

        public KondisiController(OMNIDbContext dbOMNI)
        {
            _dbOMNI = dbOMNI;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _dbOMNI.Kondisi.Where(b => b.IsDeleted == GeneralConstants.NO).OrderByDescending(b => b.CreatedAt).OrderByDescending(b => b.UpdatedAt).ToListAsync(cancellationToken);
            return Ok(result);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _dbOMNI.Kondisi.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == id).FirstOrDefaultAsync(cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(KondisiModel model, CancellationToken cancellationToken)
        {
            Kondisi data = new Kondisi();
            if (model.Id > 0)
            {
                data = await _dbOMNI.Kondisi.Where(b => b.Id == model.Id).FirstOrDefaultAsync(cancellationToken);
                data.Name = model.Name;
                data.Desc = model.Desc;
                data.UpdatedAt = DateTime.Now;
                data.UpdatedBy = "admin";
                _dbOMNI.Kondisi.Update(data);
                await _dbOMNI.SaveChangesAsync(cancellationToken);
            }
            else
            {
                data.Name = model.Name;
                data.Desc = model.Desc;
                data.CreatedAt = DateTime.Now;
                data.UpdatedBy = "admin";
                await _dbOMNI.Kondisi.AddAsync(data, cancellationToken);
                await _dbOMNI.SaveChangesAsync(cancellationToken);
            }

            return Ok(new ReturnJson { Payload = data });
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id:int}")]
        public async Task<Kondisi> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            Kondisi data = await _dbOMNI.Kondisi.Where(b => b.Id == id).FirstOrDefaultAsync(cancellationToken);
            data.IsDeleted = GeneralConstants.YES;
            data.UpdatedBy = "admin";
            data.UpdatedAt = DateTime.Now;
            _dbOMNI.Kondisi.Update(data);
            await _dbOMNI.SaveChangesAsync(cancellationToken);

            return data;
        }
    }
}
