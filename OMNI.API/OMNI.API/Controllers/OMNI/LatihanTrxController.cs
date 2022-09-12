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
    public class LatihanTrxController : BaseController
    {
        public LatihanTrxController(OMNIDbContext dbOMNI, MinioClient mc) : base(dbOMNI, mc)
        {
            _dbOMNI = dbOMNI;
            _mc = mc;
        }

        public class CountData
        {
            public int TrxId { get; set; }
            public decimal TotalCount { get; set; }
        }

        // GET: api/<ValuesController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(string port, int year, CancellationToken cancellationToken)
        {
            int lastLatihanId = 0;
            decimal totalDetailExisting = 0;
            List<CountData> countTanggalPelaksanaan = new List<CountData>();

            List<LatihanTrxModel> result = new List<LatihanTrxModel>();
            List<RekomendasiLatihan> rekomendasiLatihanList = await _dbOMNI.RekomendasiLatihan.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port && b.Year == year && b.RekomendasiType.Id == 1).Include(b => b.Latihan).Include(b => b.RekomendasiType).ToListAsync(cancellationToken);

            try
            {
                var list = await _dbOMNI.LatihanTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port && b.Year == year)
                .Include(b => b.Latihan)
                .OrderBy(b => b.Latihan.Id).ToListAsync(cancellationToken);
                if (list.Count() > 0)
                {
                    for (int i = 0; i < list.Count(); i++)
                    {
                        //COUNT TOTAL TANGGAL PELAKSANAAN
                        if (i == 0)
                        {
                            if (i == (list.Count() - 1))
                            {
                                CountData tempTanggalPelaksanaan = new CountData();
                                tempTanggalPelaksanaan.TrxId = list[i].Latihan.Id;
                                tempTanggalPelaksanaan.TotalCount = 1;
                                countTanggalPelaksanaan.Add(tempTanggalPelaksanaan);
                            }
                            else
                            {
                                lastLatihanId = list[i].Latihan.Id;
                                totalDetailExisting += 1;
                            }
                        }
                        else
                        {
                            if (lastLatihanId == list[i].Latihan.Id)
                            {
                                if (i == (list.Count() - 1))
                                {
                                    totalDetailExisting += 1;
                                    CountData tempTanggalPelaksanaan1 = new CountData();
                                    tempTanggalPelaksanaan1.TrxId = lastLatihanId;
                                    tempTanggalPelaksanaan1.TotalCount = totalDetailExisting;
                                    countTanggalPelaksanaan.Add(tempTanggalPelaksanaan1);
                                }
                                else
                                {
                                    totalDetailExisting += 1;
                                }
                            }
                            else
                            {
                                CountData tempTanggalPelaksanaan = new CountData();
                                tempTanggalPelaksanaan.TrxId = lastLatihanId;
                                tempTanggalPelaksanaan.TotalCount = totalDetailExisting;
                                countTanggalPelaksanaan.Add(tempTanggalPelaksanaan);

                                totalDetailExisting = 0;

                                if (i == (list.Count() - 1))
                                {
                                    lastLatihanId = list[i].Latihan.Id;
                                    totalDetailExisting = 1;

                                    CountData tempTanggalPelaksanaan_2 = new CountData();
                                    tempTanggalPelaksanaan_2.TrxId = lastLatihanId;
                                    tempTanggalPelaksanaan_2.TotalCount = 1;
                                    countTanggalPelaksanaan.Add(tempTanggalPelaksanaan_2);
                                }
                                else
                                {
                                    lastLatihanId = list[i].Latihan.Id;
                                    totalDetailExisting += 1;
                                }
                            }
                        }

                        LatihanTrxModel temp = new LatihanTrxModel();

                        var findRekomendasiHubla = rekomendasiLatihanList.Find(b => b.Latihan.Id == list[i].Latihan.Id);

                        if (findRekomendasiHubla != null)
                        {
                            temp.RekomendasiHubla = findRekomendasiHubla.Value;
                        }

                        temp.Id = list[i].Id;
                        temp.Latihan = list[i].Latihan != null ? list[i].Latihan.Name : "-";
                        temp.LatihanId = list[i].Latihan != null ? list[i].Latihan.Id : 0;
                        temp.Satuan = list[i].Latihan != null ? list[i].Latihan.Satuan : "-";
                        temp.TanggalPelaksanaan = list[i].TanggalPelaksanaan != null ? list[i].TanggalPelaksanaan.ToString("dd/MM/yyyy") : "-";
                        temp.PersentaseLatihan = list[i].PersentaseLatihan;
                        temp.Port = list[i].Port;
                        temp.CreateDate = list[i].CreatedAt.ToString("dd MMM yyyy");
                        temp.CreatedBy = list[i].CreatedBy;
                        result.Add(temp);
                    }

                    if (countTanggalPelaksanaan.Count() > 0)
                    {
                        for (int i = 0; i < result.Count(); i++)
                        {
                            var find = countTanggalPelaksanaan.Find(b => b.TrxId == result[i].LatihanId);
                            if (find != null)
                            {
                                result[i].TotalTanggalPelaksanaan = find.TotalCount;
                                result[i].SelisihHubla = find.TotalCount - result[i].RekomendasiHubla;

                                if(result[i].RekomendasiHubla > 0)
                                {
                                    result[i].PersentaseLatihan = find.TotalCount / result[i].RekomendasiHubla * 100;
                                }
                                

                                if (result[i].SelisihHubla >= 0)
                                {
                                    result[i].KesesuaianPM58 = "TERPENUHI";
                                }
                                else
                                {
                                    result[i].KesesuaianPM58 = "KURANG";
                                }
                            }
                        }
                    }

                    LatihanTrxModel totalPersentaseModel = new LatihanTrxModel();
                    totalPersentaseModel.Latihan = "Total Persentase";
                    result.Add(totalPersentaseModel);
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return Ok(result);
        }

        [HttpGet("GetRekomendasiLatihanByLatihanId")]
        public async Task<IActionResult> GetRekomendasiLatihanByLatihanId(string id, string port, int year, CancellationToken cancellationToken)
        {
            RekomendasiLatihan result = await _dbOMNI.RekomendasiLatihan.Where(b => b.IsDeleted == GeneralConstants.NO && b.Latihan.Id == int.Parse(id) && b.Port == port && b.Year == year && b.RekomendasiType.Id == 1).Include(b => b.Latihan).Include(b => b.RekomendasiType).FirstOrDefaultAsync(cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            LatihanTrxModel result = new LatihanTrxModel();
            var data = await _dbOMNI.LatihanTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == id)
                .Include(b => b.Latihan)
                .OrderBy(b => b.CreatedAt).FirstOrDefaultAsync(cancellationToken);
            if (data != null)
            {
                result.Id = data.Id;
                result.Latihan = data.Latihan != null ? data.Latihan.Id.ToString() : "0";
                result.Port = data.Port;
                result.TanggalPelaksanaan = data.TanggalPelaksanaan != null ? data.TanggalPelaksanaan.ToString("MM/dd/yyyy") : null;
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit([FromForm] LatihanTrxModel model, CancellationToken cancellationToken)
        {
            DateTime nullDate = new DateTime();
            LatihanTrx data = new LatihanTrx();

            if (model.Id > 0)
            {
                data.Id = model.Id;
                data = await _dbOMNI.LatihanTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == model.Id)
                 .Include(b => b.Latihan)
                 .OrderBy(b => b.CreatedAt).FirstOrDefaultAsync(cancellationToken);

                data.Latihan = await _dbOMNI.Latihan.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == int.Parse(model.Latihan)).FirstOrDefaultAsync(cancellationToken);
                data.Port = model.Port;
                data.Year = model.Year;
                data.TanggalPelaksanaan = !string.IsNullOrEmpty(model.TanggalPelaksanaan) ? DateTime.ParseExact(model.TanggalPelaksanaan, "MM/dd/yyyy", null) : nullDate;
                data.UpdatedAt = DateTime.Now;
                data.UpdatedBy = "admin";
                _dbOMNI.LatihanTrx.Update(data);
                await _dbOMNI.SaveChangesAsync(cancellationToken);
            }
            else
            {
                data.Latihan = await _dbOMNI.Latihan.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == int.Parse(model.Latihan)).FirstOrDefaultAsync(cancellationToken);
                data.Port = model.Port;
                data.Year = model.Year;
                data.TanggalPelaksanaan = !string.IsNullOrEmpty(model.TanggalPelaksanaan) ? DateTime.ParseExact(model.TanggalPelaksanaan, "MM/dd/yyyy", null) : nullDate;
                data.CreatedAt = DateTime.Now;
                data.CreatedBy = "admin";
                await _dbOMNI.LatihanTrx.AddAsync(data, cancellationToken);
                await _dbOMNI.SaveChangesAsync(cancellationToken);
            }

            if (model.Files != null)
            {
                if (model.Files.Count() > 0)
                {
                    for (int i = 0; i < model.Files.Count(); i++)
                    {
                        await UploadFileWithReturn(path: $"OMNI/Latihan/{data.Id}/Files/", createBy: data.CreatedBy, trxId: data.Id, file: model.Files[i], Flag: GeneralConstants.OMNI_LATIHAN, isUpdate: model.Files != null, remark: null);
                    }

                }
            }

            return Ok(new ReturnJson { });
        }

        //// DELETE api/<ValuesController>/5
        [HttpDelete("{id:int}")]
        public async Task<LatihanTrx> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            LatihanTrx data = await _dbOMNI.LatihanTrx.Where(b => b.Id == id).Include(b => b.Latihan).FirstOrDefaultAsync(cancellationToken);
            data.IsDeleted = GeneralConstants.YES;
            data.UpdatedBy = "admin";
            data.UpdatedAt = DateTime.Now;
            _dbOMNI.LatihanTrx.Update(data);
            await _dbOMNI.SaveChangesAsync(cancellationToken);

            return data;
        }
    }
}
