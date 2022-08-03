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
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            LLPTrxModel result = new LLPTrxModel();
            var data = await _dbOMNI.LLPTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == id)
                .Include(b => b.SpesifikasiJenis)
                .Include(b => b.SpesifikasiJenis.PeralatanOSR)
                .Include(b => b.SpesifikasiJenis.Jenis)
                .OrderBy(b => b.CreatedAt).FirstOrDefaultAsync(cancellationToken);
            if (data != null)
            {
                result.Id = data.Id;
                result.PeralatanOSR = data.SpesifikasiJenis != null ? data.SpesifikasiJenis.PeralatanOSR.Id.ToString() : "-";
                result.Jenis = data.SpesifikasiJenis != null ? data.SpesifikasiJenis.Id.ToString() : "-";

                var findRekomenJenis = await _dbOMNI.RekomendasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == data.Port && b.SpesifikasiJenis.Id == data.SpesifikasiJenis.Id).FirstOrDefaultAsync(cancellationToken);
                if(findRekomenJenis != null)
                {
                    result.RekomendasiHubla = findRekomenJenis.Value;
                } else
                {
                    result.RekomendasiHubla = 0;
                }

                result.SatuanJenis = data.SpesifikasiJenis != null ? data.SpesifikasiJenis.Jenis.Satuan : "-";
                result.KodeInventory = data.SpesifikasiJenis != null ? data.SpesifikasiJenis.Jenis.KodeInventory : "-";
                result.Port = data.Port;
                result.QRCode = data.QRCode;
                result.DetailExisting = data.DetailExisting;
                result.Kondisi = data.Kondisi;
                result.TotalExistingJenis = data.TotalExistingJenis;
                result.TotalExistingKeseluruhan = data.TotalExistingKeseluruhan;
                result.TotalKebutuhanHubla = data.TotalKebutuhanHubla;
                result.SelisihHubla = data.SelisihHubla;
                result.KesesuaianMP58 = data.KesesuaianMP58;
                result.PersentaseHubla = data.PersentaseHubla;
                result.TotalKebutuhanOSCP = data.TotalKebutuhanOSCP;
                result.SelisihOSCP = data.SelisihOSCP;
                result.KesesuaianOSCP = data.KesesuaianOSCP;
                result.PersentaseOSCP = data.PersentaseOSCP;
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(LLPTrxModel model, CancellationToken cancellationToken)
        {
            LLPTrx data = new LLPTrx();

            if (model.Id > 0)
            {
               data = await _dbOMNI.LLPTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == model.Id)
                .Include(b => b.SpesifikasiJenis)
                .Include(b => b.SpesifikasiJenis.PeralatanOSR)
                .Include(b => b.SpesifikasiJenis.Jenis)
                .OrderBy(b => b.CreatedAt).FirstOrDefaultAsync(cancellationToken);

                data.SpesifikasiJenis = await _dbOMNI.SpesifikasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == int.Parse(model.Jenis)).FirstOrDefaultAsync(cancellationToken);

                data.Port = model.Port;
                data.QRCode = !string.IsNullOrWhiteSpace(model.QRCode) ? model.QRCode : "";
                data.DetailExisting = model.DetailExisting;
                data.Kondisi = model.Kondisi;
                data.TotalExistingJenis = model.TotalExistingJenis;
                data.TotalExistingKeseluruhan = model.TotalExistingKeseluruhan;
                data.TotalKebutuhanHubla = model.TotalKebutuhanHubla;
                data.SelisihHubla = model.SelisihHubla;
                data.KesesuaianMP58 = model.KesesuaianMP58;
                data.PersentaseHubla = model.PersentaseHubla;
                data.TotalKebutuhanOSCP = model.TotalKebutuhanOSCP;
                data.SelisihOSCP = model.SelisihOSCP;
                data.KesesuaianOSCP = model.KesesuaianOSCP;
                data.PersentaseOSCP = model.PersentaseOSCP;
                data.UpdatedAt = DateTime.Now;
                data.UpdatedBy = "admin";
                _dbOMNI.LLPTrx.Update(data);
                await _dbOMNI.SaveChangesAsync(cancellationToken);
            }
            else
            {
                data.SpesifikasiJenis = await _dbOMNI.SpesifikasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == int.Parse(model.Jenis)).FirstOrDefaultAsync(cancellationToken);

                data.Port = model.Port;
                data.QRCode = !string.IsNullOrWhiteSpace(model.QRCode) ? model.QRCode : "";
                data.DetailExisting = model.DetailExisting;
                data.Kondisi = model.Kondisi;
                data.TotalExistingJenis = model.TotalExistingJenis;
                data.TotalExistingKeseluruhan = model.TotalExistingKeseluruhan;
                data.TotalKebutuhanHubla = model.TotalKebutuhanHubla;
                data.SelisihHubla = model.SelisihHubla;
                data.KesesuaianMP58 = model.KesesuaianMP58;
                data.PersentaseHubla = model.PersentaseHubla;
                data.TotalKebutuhanOSCP = model.TotalKebutuhanOSCP;
                data.SelisihOSCP = model.SelisihOSCP;
                data.KesesuaianOSCP = model.KesesuaianOSCP;
                data.PersentaseOSCP = model.PersentaseOSCP;
                data.CreatedAt = DateTime.Now;
                data.CreatedBy = "admin";
                await _dbOMNI.LLPTrx.AddAsync(data, cancellationToken);
                await _dbOMNI.SaveChangesAsync(cancellationToken);
            }


            return Ok(new ReturnJson { });
        }

        //// DELETE api/<ValuesController>/5
        [HttpDelete("{id:int}")]
        public async Task<LLPTrx> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            LLPTrx data = await _dbOMNI.LLPTrx.Where(b => b.Id == id).FirstOrDefaultAsync(cancellationToken);
            data.IsDeleted = GeneralConstants.YES;
            data.UpdatedBy = "admin";
            data.UpdatedAt = DateTime.Now;
            _dbOMNI.LLPTrx.Update(data);
            await _dbOMNI.SaveChangesAsync(cancellationToken);

            return data;
        }
    }
}
