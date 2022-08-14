using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minio;
using Minio.DataModel;
using Minio.Exceptions;
using OMNI.API.Model.OMNI;
using OMNI.Data.Data;
using OMNI.Data.Data.Dao;
using OMNI.Domain.AppLogRepo;
using OMNI.Migrations.Data.Dao;
using OMNI.Utilities.Base;
using OMNI.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OMNI.API.Controllers.OMNI
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PersonilTrxController : BaseController
    {
        public PersonilTrxController(OMNIDbContext dbOMNI, MinioClient mc) : base(dbOMNI, mc)
        {
            _dbOMNI = dbOMNI;
            _mc = mc;
        }

        public class CountData
        {
            public int TrxId { get; set; }
            public decimal TotalCount { get; set; }
            public decimal SelisihHubla { get; set; }
            public string KesesuaianPM58 { get; set; }
        }

        // GET: api/<ValuesController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(string port, CancellationToken cancellationToken)
        {
            List<PersonilTrxModel> result = new List<PersonilTrxModel>();
            List<RekomendasiPersonil> rekomenPersonilList = await _dbOMNI.RekomendasiPersonil.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port).Include(b => b.RekomendasiType).ToListAsync(cancellationToken);

            var list = await _dbOMNI.PersonilTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port)
                .Include(b => b.Personil)
                .OrderBy(b => b.CreatedAt).ToListAsync(cancellationToken);
            if (list.Count() > 0)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    PersonilTrxModel temp = new PersonilTrxModel();
                    temp.Id = list[i].Id;
                    temp.Personil = list[i].Personil != null ? list[i].Personil.Name : "-";
                    temp.Satuan = list[i].Personil != null ? list[i].Personil.Satuan : "-";
                    temp.Name = list[i].Name;
                    temp.TotalDetailExisting = list[i].TotalDetailExisting;
                    temp.TanggalPelatihan = list[i].TanggalPelatihan != null ? list[i].TanggalPelatihan.ToString("dd/MM/yyyy") : "-";
                    temp.TanggalExpired = list[i].TanggalExpired != null ? list[i].TanggalExpired.ToString("dd/MM/yyyy") : "-";
                    temp.SelisihHubla = list[i].SelisihHubla;
                    temp.KesesuaianPM58 = list[i].KesesuaianPM58;
                    temp.PersentasePersonil = list[i].PersentasePersonil;
                    temp.SisaMasaBerlaku = list[i].SisaMasaBerlaku;
                    temp.RekomendasiHubla = 0;
                    temp.Port = list[i].Port;
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
        //    PersonilTrxModel result = new PersonilTrxModel();
        //    var data = await _dbOMNI.PersonilTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == id)
        //        .Include(b => b.SpesifikasiJenis)
        //        .Include(b => b.SpesifikasiJenis.PeralatanOSR)
        //        .Include(b => b.SpesifikasiJenis.Jenis)
        //        .OrderBy(b => b.CreatedAt).FirstOrDefaultAsync(cancellationToken);
        //    if (data != null)
        //    {
        //        result.Id = data.Id;
        //        result.PeralatanOSR = data.SpesifikasiJenis != null ? data.SpesifikasiJenis.PeralatanOSR.Id.ToString() : "-";
        //        result.Jenis = data.SpesifikasiJenis != null ? data.SpesifikasiJenis.Id.ToString() : "-";

        //        //var findRekomenJenis = await _dbOMNI.RekomendasiPersonil.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == data.Port && b.SpesifikasiJenis.Id == data.SpesifikasiJenis.Id).Include().ToListAsync(cancellationToken);
        //        //if(findRekomenJenis.Count() > 0)
        //        //{
        //        //    result.RekomendasiHubla = findRekomenJenis.Find(b => b.)
        //        //    result.RekomendasiOSCP = findRekomenJenis.
        //        //} else
        //        //{
        //        //    result.RekomendasiHubla = 0;
        //        //}

        //        result.SatuanJenis = data.SpesifikasiJenis != null ? data.SpesifikasiJenis.Jenis.Satuan : "-";
        //        result.KodeInventory = data.SpesifikasiJenis != null ? data.SpesifikasiJenis.Jenis.KodeInventory : "-";
        //        result.Port = data.Port;
        //        result.QRCode = data.QRCode;
        //        result.DetailExisting = data.DetailExisting;
        //        result.Kondisi = data.Kondisi;
        //        result.TotalExistingJenis = data.TotalExistingJenis;
        //        result.TotalExistingKeseluruhan = data.TotalExistingKeseluruhan;
        //        result.TotalKebutuhanHubla = data.TotalKebutuhanHubla;
        //        result.SelisihHubla = data.SelisihHubla;
        //        //result.KesesuaianPM58 = data.KesesuaianPM58;
        //        result.PersentaseHubla = data.PersentaseHubla;
        //        result.TotalKebutuhanOSCP = data.TotalKebutuhanOSCP;
        //        result.SelisihOSCP = data.SelisihOSCP;
        //        result.KesesuaianOSCP = data.KesesuaianOSCP;
        //        result.PersentaseOSCP = data.PersentaseOSCP;
        //    }
        //    return Ok(result);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddEdit([FromForm] PersonilTrxModel model, CancellationToken cancellationToken)
        //{
        //    PersonilTrx data = new PersonilTrx();

        //    if (model.Id > 0)
        //    {
        //        data = await _dbOMNI.PersonilTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == model.Id)
        //         .Include(b => b.SpesifikasiJenis)
        //         .Include(b => b.SpesifikasiJenis.PeralatanOSR)
        //         .Include(b => b.SpesifikasiJenis.Jenis)
        //         .OrderBy(b => b.CreatedAt).FirstOrDefaultAsync(cancellationToken);

        //        data.SpesifikasiJenis = await _dbOMNI.SpesifikasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == int.Parse(model.Jenis)).FirstOrDefaultAsync(cancellationToken);

        //        data.Port = model.Port;
        //        data.QRCode = !string.IsNullOrWhiteSpace(model.QRCode) ? model.QRCode : "";
        //        data.DetailExisting = model.DetailExisting;
        //        data.Kondisi = model.Kondisi;
        //        data.TotalExistingJenis = model.TotalExistingJenis;
        //        data.TotalExistingKeseluruhan = model.TotalExistingKeseluruhan;
        //        data.TotalKebutuhanHubla = model.TotalKebutuhanHubla;
        //        data.SelisihHubla = model.SelisihHubla;
        //        //data.KesesuaianPM58 = model.KesesuaianPM58;
        //        data.PersentaseHubla = model.PersentaseHubla;
        //        data.TotalKebutuhanOSCP = model.TotalKebutuhanOSCP;
        //        data.SelisihOSCP = model.SelisihOSCP;
        //        data.KesesuaianOSCP = model.KesesuaianOSCP;
        //        data.PersentaseOSCP = model.PersentaseOSCP;
        //        data.UpdatedAt = DateTime.Now;
        //        data.UpdatedBy = "admin";
        //        _dbOMNI.PersonilTrx.Update(data);
        //        await _dbOMNI.SaveChangesAsync(cancellationToken);
        //    }
        //    else
        //    {
        //        data.SpesifikasiJenis = await _dbOMNI.SpesifikasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == int.Parse(model.Jenis)).FirstOrDefaultAsync(cancellationToken);

        //        data.Port = model.Port;
        //        data.QRCode = !string.IsNullOrWhiteSpace(model.QRCode) ? model.QRCode : "";
        //        data.DetailExisting = model.DetailExisting;
        //        data.Kondisi = model.Kondisi;
        //        data.TotalExistingJenis = model.TotalExistingJenis;
        //        data.TotalExistingKeseluruhan = model.TotalExistingKeseluruhan;
        //        data.TotalKebutuhanHubla = model.TotalKebutuhanHubla;
        //        data.SelisihHubla = model.SelisihHubla;
        //        //data.KesesuaianPM58 = model.KesesuaianPM58;
        //        data.PersentaseHubla = model.PersentaseHubla;
        //        data.TotalKebutuhanOSCP = model.TotalKebutuhanOSCP;
        //        data.SelisihOSCP = model.SelisihOSCP;
        //        data.KesesuaianOSCP = model.KesesuaianOSCP;
        //        data.PersentaseOSCP = model.PersentaseOSCP;
        //        data.CreatedAt = DateTime.Now;
        //        data.CreatedBy = "admin";
        //        await _dbOMNI.PersonilTrx.AddAsync(data, cancellationToken);
        //        await _dbOMNI.SaveChangesAsync(cancellationToken);
        //    }

        //    if (model.Files.Count() > 0)
        //    {
        //        for (int i = 0; i < model.Files.Count(); i++)
        //        {
        //            await UploadFileWithReturn(path: $"OMNI/{data.Id}/Files/", createBy: data.CreatedBy, trxId: data.Id, file: model.Files[i], Flag: GeneralConstants.OMNI_LLP, isUpdate: model.Files != null, remark: null);
        //        }

        //    }


        //    //_dbOMNI.PersonilTrx.Update(data);
        //    //await _dbOMNI.SaveChangesAsync(cancellationToken);

        //    return Ok(new ReturnJson { });
        //}

        ////// DELETE api/<ValuesController>/5
        //[HttpDelete("{id:int}")]
        //public async Task<PersonilTrx> Delete([FromRoute] int id, CancellationToken cancellationToken)
        //{
        //    PersonilTrx data = await _dbOMNI.PersonilTrx.Where(b => b.Id == id).Include(b => b.SpesifikasiJenis).FirstOrDefaultAsync(cancellationToken);
        //    data.IsDeleted = GeneralConstants.YES;
        //    data.UpdatedBy = "admin";
        //    data.UpdatedAt = DateTime.Now;
        //    _dbOMNI.PersonilTrx.Update(data);
        //    await _dbOMNI.SaveChangesAsync(cancellationToken);

        //    return data;
        //}
    }
}
