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
    public class LLPTrxController : ControllerBase
    {
        private readonly OMNIDbContext _dbOMNI;

        public LLPTrxController(OMNIDbContext dbOMNI)
        {
            _dbOMNI = dbOMNI;
        }

        // GET: api/<ValuesController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(string port, CancellationToken cancellationToken)
        {
            List<LLPTrxModel> result = new List<LLPTrxModel>();
            List<RekomendasiJenis> rekomenJenisList = await _dbOMNI.RekomendasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port).Include(b => b.SpesifikasiJenis).ToListAsync(cancellationToken);

            var list = await _dbOMNI.LLPTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port)
                .Include(b => b.SpesifikasiJenis)
                .Include(b => b.SpesifikasiJenis.PeralatanOSR)
                .Include(b => b.SpesifikasiJenis.Jenis)
                .OrderBy(b => b.CreatedAt).ToListAsync(cancellationToken);
            if (list.Count() > 0)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    LLPTrxModel temp = new LLPTrxModel();
                    temp.Id = list[i].Id;
                    temp.PeralatanOSR = list[i].SpesifikasiJenis != null ? list[i].SpesifikasiJenis.PeralatanOSR.Name : "-";
                    temp.Jenis = list[i].SpesifikasiJenis != null ? list[i].SpesifikasiJenis.Jenis.Name : "-";

                    if(rekomenJenisList.Count() > 0 && list[i].SpesifikasiJenis != null)
                    {
                        var findRekomenJenis = rekomenJenisList.FindAll(b => b.SpesifikasiJenis.Id == list[i].SpesifikasiJenis.Id).FirstOrDefault();
                        if(findRekomenJenis != null)
                        {
                            temp.RekomendasiHubla = findRekomenJenis.Value;
                        }
                    } else
                    {
                        temp.RekomendasiHubla = 0;
                    }
                    temp.SatuanJenis = list[i].SpesifikasiJenis != null ? list[i].SpesifikasiJenis.Jenis.Satuan : "-";
                    temp.Port = list[i].Port;
                    temp.QRCode = list[i].QRCode;
                    temp.DetailExisting = list[i].DetailExisting;
                    temp.Kondisi = list[i].Kondisi;
                    temp.TotalExistingJenis = list[i].TotalExistingJenis;
                    temp.TotalExistingKeseluruhan = list[i].TotalExistingKeseluruhan;
                    temp.TotalKebutuhanHubla = list[i].TotalKebutuhanHubla;
                    temp.SelisihHubla = list[i].SelisihHubla;
                    temp.KesesuaianMP58 = list[i].KesesuaianMP58;
                    temp.PersentaseHubla = list[i].PersentaseHubla;
                    temp.TotalKebutuhanOSCP = list[i].TotalKebutuhanOSCP;
                    temp.SelisihOSCP = list[i].SelisihOSCP;
                    temp.KesesuaianOSCP = list[i].KesesuaianOSCP;
                    temp.PersentaseOSCP = list[i].PersentaseOSCP;
                    temp.CreateDate = list[i].CreatedAt.ToString("dd MMM yyyy");
                    temp.CreatedBy = list[i].CreatedBy;
                    result.Add(temp);
                }
            }

            return Ok(result);
        }

        // GET api/<ValuesController>/5
        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        //{
        //    RekomendasiLatihanModel result = new RekomendasiLatihanModel();
        //    var data = await _dbOMNI.RekomendasiLatihan.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == id).Include(b => b.RekomendasiType)
        //        .Include(b => b.Latihan).Include(b => b.RekomendasiType).FirstOrDefaultAsync(cancellationToken);
        //    if (data != null)
        //    {
        //        result.Id = data.Id;
        //        result.Latihan = data.Latihan != null ? data.Latihan.Id.ToString() : "0";
        //        result.RekomendasiType = data.RekomendasiType != null ? data.RekomendasiType.Id.ToString() : "0";
        //        result.Port = data.Port;
        //        result.Value = data.Value;
        //    }
        //    return Ok(result);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddEdit(RekomendasiLatihanModel model, CancellationToken cancellationToken)
        //{
        //    RekomendasiLatihan data = new RekomendasiLatihan();

        //    if (model.Id > 0)
        //    {
        //        data = await _dbOMNI.RekomendasiLatihan.Where(b => b.Id == model.Id).Include(b => b.Latihan).Include(b => b.RekomendasiType).FirstOrDefaultAsync(cancellationToken);

        //        data.Latihan = await _dbOMNI.Latihan.Where(b => b.Id == int.Parse(model.Latihan)).FirstOrDefaultAsync(cancellationToken);
        //        data.RekomendasiType = await _dbOMNI.RekomendasiType.Where(b => b.Id == int.Parse(model.RekomendasiType)).FirstOrDefaultAsync(cancellationToken);
        //        data.Port = model.Port;
        //        data.Value = model.Value;
        //        data.UpdatedAt = DateTime.Now;
        //        data.UpdatedBy = "admin";
        //        _dbOMNI.RekomendasiLatihan.Update(data);
        //        await _dbOMNI.SaveChangesAsync(cancellationToken);
        //    }
        //    else
        //    {
        //        data.Latihan = await _dbOMNI.Latihan.Where(b => b.Id == int.Parse(model.Latihan)).FirstOrDefaultAsync(cancellationToken);
        //        data.RekomendasiType = await _dbOMNI.RekomendasiType.Where(b => b.Id == int.Parse(model.RekomendasiType)).FirstOrDefaultAsync(cancellationToken);
        //        data.Port = model.Port;
        //        data.Value = model.Value;
        //        data.CreatedAt = DateTime.Now;
        //        data.CreatedBy = "admin";
        //        await _dbOMNI.RekomendasiLatihan.AddAsync(data, cancellationToken);
        //        await _dbOMNI.SaveChangesAsync(cancellationToken);
        //    }


        //    return Ok(new ReturnJson { });
        //}

        //// DELETE api/<ValuesController>/5
        //[HttpDelete("{id:int}")]
        //public async Task<RekomendasiLatihan> Delete([FromRoute] int id, CancellationToken cancellationToken)
        //{
        //    RekomendasiLatihan data = await _dbOMNI.RekomendasiLatihan.Where(b => b.Id == id).FirstOrDefaultAsync(cancellationToken);
        //    data.IsDeleted = GeneralConstants.YES;
        //    data.UpdatedBy = "admin";
        //    data.UpdatedAt = DateTime.Now;
        //    _dbOMNI.RekomendasiLatihan.Update(data);
        //    await _dbOMNI.SaveChangesAsync(cancellationToken);

        //    return data;
        //}
    }
}
