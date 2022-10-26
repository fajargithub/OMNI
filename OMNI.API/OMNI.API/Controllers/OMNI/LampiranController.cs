using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minio;
using OMNI.API.Model.OMNI;
using OMNI.Data.Data;
using OMNI.Data.Data.Dao;
using OMNI.Migrations.Data.Dao;
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
    public class LampiranController : BaseController
    {
        public LampiranController(OMNIDbContext dbOMNI, MinioClient mc) : base(dbOMNI, mc)
        {
            _dbOMNI = dbOMNI;
            _mc = mc;
        }

        // GET: api/<ValuesController>
        [HttpGet("GetAllByPort")]
        public async Task<IActionResult> GetAllByPort(string port, CancellationToken cancellationToken)
        {
            List<LampiranModel> result = new List<LampiranModel>();
            var list = await _dbOMNI.Lampiran.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port).OrderByDescending(b => b.CreatedAt).OrderByDescending(b => b.UpdatedAt).ToListAsync(cancellationToken);
            if(list.Count() > 0)
            {
                for(int i=0; i < list.Count(); i++)
                {
                    LampiranModel temp = new LampiranModel();
                    temp.Id = list[i].Id;
                    temp.Name = list[i].Name;
                    temp.LampiranType = list[i].LampiranType;
                    temp.Port = list[i].Port;
                    temp.StartDate = list[i].StartDate.HasValue ? list[i].StartDate.Value.ToString("dd MMM yyyy") : "-";
                    temp.EndDate = list[i].EndDate.HasValue ? list[i].EndDate.Value.ToString("dd MMM yyyy") : "-";
                    temp.CreateDate = list[i].CreatedAt != null ? list[i].CreatedAt.ToString("dd MMM yyyy") : "-";
                    temp.CreatedBy = list[i].CreatedBy;
                    temp.Remark = list[i].Remark;
                    result.Add(temp);
                }
            }

            return Ok(result);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            LampiranModel result = new LampiranModel();
            var temp = await _dbOMNI.Lampiran.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == id).FirstOrDefaultAsync(cancellationToken);
            if(temp != null)
            {
                result.Id = temp.Id;
                result.Name = temp.Name;
                result.LampiranType = temp.LampiranType;
                result.Port = temp.Port;
                result.StartDate = temp.StartDate.HasValue ? temp.StartDate.Value.ToString("MM/dd/yyyy") : "";
                result.EndDate = temp.EndDate.HasValue ? temp.EndDate.Value.ToString("MM/dd/yyyy") : "";
                result.Remark = temp.Remark;
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit([FromForm] LampiranModel model, CancellationToken cancellationToken)
        {
            DateTime nullDate = new DateTime();
            Lampiran data = new Lampiran();
            if (model.Id > 0)
            {
                data = await _dbOMNI.Lampiran.Where(b => b.Id == model.Id).FirstOrDefaultAsync(cancellationToken);
                data.LampiranType = model.LampiranType;
                data.Name = model.Name;
                data.Port = model.Port;
                data.StartDate = string.IsNullOrEmpty(model.StartDate) ? (DateTime?)null : DateTime.ParseExact(model.StartDate, "MM/dd/yyyy", null);
                data.Remark = model.Remark;
                data.UpdatedAt = DateTime.Now;
                data.UpdatedBy = "admin";
                _dbOMNI.Lampiran.Update(data);
                await _dbOMNI.SaveChangesAsync(cancellationToken);
            }
            else
            {
                try
                {
                    data.LampiranType = model.LampiranType;
                    data.Name = model.Name;
                    data.Port = model.Port;
                    data.StartDate = string.IsNullOrEmpty(model.StartDate) ? (DateTime?)null : DateTime.ParseExact(model.StartDate, "MM/dd/yyyy", null);

                    if (data.LampiranType == "PENGESAHAN")
                    {
                        data.EndDate = DateTime.Now.AddDays(910);
                    }
                    else if (data.LampiranType == "VERIFIKASI1")
                    {
                        var findPengesahan = await _dbOMNI.Lampiran.Where(b => b.LampiranType == "PENGESAHAN").OrderByDescending(b => b.Id).FirstOrDefaultAsync(cancellationToken);
                        if(findPengesahan != null)
                        {
                            data.EndDate = findPengesahan.EndDate.Value.AddDays(910);
                        }
                    }
                    else
                    {
                        data.EndDate = string.IsNullOrEmpty(model.EndDate) ? (DateTime?)null : DateTime.ParseExact(model.EndDate, "MM/dd/yyyy", null);
                    }

                    data.Remark = model.Remark;
                    data.CreatedAt = DateTime.Now;
                    data.CreatedBy = "admin";
                    data.UpdatedBy = "admin";
                    await _dbOMNI.Lampiran.AddAsync(data, cancellationToken);
                    await _dbOMNI.SaveChangesAsync(cancellationToken);
                } catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            string fileFlag = "";

            if(data.LampiranType == "PENILAIAN")
            {
                fileFlag = GeneralConstants.OSMOSYS_PENILAIAN;
            } else if(data.LampiranType == "PENGESAHAN")
            {
                fileFlag = GeneralConstants.OSMOSYS_PENGESAHAN;
            } else if(data.LampiranType == "VERIFIKASI1" || data.LampiranType == "VERIFIKASI2")
            {
                fileFlag = GeneralConstants.OSMOSYS_VERIFIKASI;
            } 

            if (model.Files != null)
            {
                if (model.Files.Count() > 0)
                {
                    for (int i = 0; i < model.Files.Count(); i++)
                    {
                        await UploadFileWithReturn(path: $"OMNI/{data.Id}/Files/", createBy: data.CreatedBy, trxId: data.Id, file: model.Files[i], Flag: fileFlag, isUpdate: model.Files != null, remark: null);
                    }

                }
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
