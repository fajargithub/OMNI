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
    public class LLPTrxController : BaseController
    {
        public LLPTrxController(OMNIDbContext dbOMNI, MinioClient mc) : base(dbOMNI, mc)
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
            int lastSpesifikasiJenisId = 0;
            int lastSpesifikasiJenisId_2 = 0;
            int lastPeralatanOSRId = 0;
            decimal totalKesesuaianHubla = 0;
            decimal totalExistingKeseluruhan = 0;
            decimal totalDetailExisting = 0;

            List<CountData> countTotalExistingJenis = new List<CountData>();
            List<CountData> countTotalKesesuaianHubla = new List<CountData>();
            List<CountData> countTotalExistingKeseluruhan = new List<CountData>();

            List<LLPTrxModel> result = new List<LLPTrxModel>();
            List<RekomendasiJenis> rekomenJenisList = await _dbOMNI.RekomendasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port).Include(b => b.SpesifikasiJenis).Include(b => b.SpesifikasiJenis.PeralatanOSR).Include(b => b.RekomendasiType).ToListAsync(cancellationToken);

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
                    temp.PeralatanOSRId = list[i].SpesifikasiJenis != null ? list[i].SpesifikasiJenis.PeralatanOSR.Id : 0;
                    temp.PeralatanOSR = list[i].SpesifikasiJenis != null ? list[i].SpesifikasiJenis.PeralatanOSR.Name : "-";
                    temp.Jenis = list[i].SpesifikasiJenis != null ? list[i].SpesifikasiJenis.Jenis.Name : "-";
                    temp.SpesifikasiJenisId = list[i].SpesifikasiJenis != null ? list[i].SpesifikasiJenis.Id : 0;

                    if(list[i].SpesifikasiJenis != null)
                    {
                        // COUNT TOTAL EXISTING JENIS
                        if (i == 0)
                        {
                            if (list.Count() == 1)
                            {
                                CountData tempTotalExistingJenis = new CountData();
                                tempTotalExistingJenis.TrxId = list[i].SpesifikasiJenis.Id;
                                tempTotalExistingJenis.TotalCount = list[i].DetailExisting;
                                countTotalExistingJenis.Add(tempTotalExistingJenis);
                            }
                            else
                            {
                                lastSpesifikasiJenisId = list[i].SpesifikasiJenis.Id;
                                totalDetailExisting += list[i].DetailExisting;
                            }
                        }
                        else
                        {
                            if (lastSpesifikasiJenisId == list[i].SpesifikasiJenis.Id)
                            {
                                if (i == (list.Count() - 1))
                                {
                                    totalDetailExisting += list[i].DetailExisting;

                                    CountData tempDetailExisting = new CountData();
                                    tempDetailExisting.TrxId = lastSpesifikasiJenisId;
                                    tempDetailExisting.TotalCount = totalDetailExisting;
                                    countTotalExistingJenis.Add(tempDetailExisting);
                                }
                                else
                                {
                                    totalDetailExisting += list[i].DetailExisting;
                                }
                            }
                            else
                            {
                                CountData tempDetailExisting1 = new CountData();
                                tempDetailExisting1.TrxId = lastSpesifikasiJenisId;
                                tempDetailExisting1.TotalCount = totalDetailExisting;
                                countTotalExistingJenis.Add(tempDetailExisting1);

                                if (i == (list.Count() - 1))
                                {
                                    lastSpesifikasiJenisId = list[i].SpesifikasiJenis.Id;
                                    totalDetailExisting = list[i].DetailExisting;

                                    CountData tempDetailExisting2 = new CountData();
                                    tempDetailExisting2.TrxId = lastSpesifikasiJenisId;
                                    tempDetailExisting2.TotalCount = totalDetailExisting;
                                    countTotalExistingJenis.Add(tempDetailExisting2);
                                }
                                else
                                {
                                    lastSpesifikasiJenisId = list[i].SpesifikasiJenis.Id;
                                    totalDetailExisting = list[i].DetailExisting;
                                }
                            }
                        }
                    }

                    //COUNT TOTAL EXISTING KESELURUHAN
                    if (i == 0)
                    {
                        lastPeralatanOSRId = list[i].SpesifikasiJenis.PeralatanOSR.Id;

                        if (i == (list.Count() - 1))
                        {
                            CountData tempExistingKeseluruhan = new CountData();
                            tempExistingKeseluruhan.TrxId = lastPeralatanOSRId;
                            tempExistingKeseluruhan.TotalCount = list[i].DetailExisting;
                            countTotalExistingKeseluruhan.Add(tempExistingKeseluruhan);


                        }
                        else
                        {
                            totalExistingKeseluruhan += list[i].DetailExisting;
                        }
                    }
                    else
                    {
                        if (lastPeralatanOSRId == list[i].SpesifikasiJenis.PeralatanOSR.Id)
                        {
                            totalExistingKeseluruhan += list[i].DetailExisting;
                        }
                        else
                        {
                            CountData tempExistingKeseluruhan = new CountData();
                            tempExistingKeseluruhan.TrxId = lastPeralatanOSRId;
                            tempExistingKeseluruhan.TotalCount = totalExistingKeseluruhan;
                            countTotalExistingKeseluruhan.Add(tempExistingKeseluruhan);

                            if (i == (list.Count() - 1))
                            {
                                lastPeralatanOSRId = list[i].SpesifikasiJenis.PeralatanOSR.Id;
                                totalExistingKeseluruhan = list[i].DetailExisting;

                                CountData tempExistingKeseluruhan2 = new CountData();
                                tempExistingKeseluruhan2.TrxId = lastPeralatanOSRId;
                                tempExistingKeseluruhan2.TotalCount = list[i].DetailExisting;
                                countTotalExistingKeseluruhan.Add(tempExistingKeseluruhan2);
                            }
                            else
                            {
                                lastPeralatanOSRId = list[i].SpesifikasiJenis.PeralatanOSR.Id;
                                totalExistingKeseluruhan = list[i].DetailExisting;
                            }
                        }
                    }

                    if (rekomenJenisList.Count() > 0 && list[i].SpesifikasiJenis != null)
                    {
                        var findRekomendasiHubla = rekomenJenisList.FindAll(b => b.SpesifikasiJenis.Id == list[i].SpesifikasiJenis.Id && b.RekomendasiType.Name.Contains("Hubla")).FirstOrDefault();
                        if(findRekomendasiHubla != null)
                        {
                            temp.RekomendasiHubla = findRekomendasiHubla.Value;

                            //COUNT TOTAL KESESUAIAN HUBLA
                            if (i == 0)
                            {
                                if (list.Count() == 1)
                                {
                                    CountData tempTotalKesesuaianHubla = new CountData();
                                    tempTotalKesesuaianHubla.TrxId = findRekomendasiHubla.SpesifikasiJenis.Id;
                                    tempTotalKesesuaianHubla.TotalCount = temp.RekomendasiHubla;
                                    countTotalKesesuaianHubla.Add(tempTotalKesesuaianHubla);
                                }
                                else
                                {
                                    lastSpesifikasiJenisId_2 = findRekomendasiHubla.SpesifikasiJenis.Id;
                                    totalKesesuaianHubla += temp.RekomendasiHubla;
                                }
                            }
                            else
                            {
                                if (lastSpesifikasiJenisId_2 == list[i].SpesifikasiJenis.Id)
                                {
                                    if (i == (list.Count() - 1))
                                    {
                                        CountData tempTotalKesesuaianHubla1 = new CountData();
                                        tempTotalKesesuaianHubla1.TrxId = lastSpesifikasiJenisId_2;
                                        tempTotalKesesuaianHubla1.TotalCount = temp.RekomendasiHubla;
                                        countTotalKesesuaianHubla.Add(tempTotalKesesuaianHubla1);
                                    }
                                    else
                                    {
                                        totalKesesuaianHubla += temp.RekomendasiHubla;
                                    }
                                }
                                else
                                {
                                    CountData tempTotalKesesuaianHubla2 = new CountData();
                                    tempTotalKesesuaianHubla2.TrxId = lastSpesifikasiJenisId_2;
                                    tempTotalKesesuaianHubla2.TotalCount = totalKesesuaianHubla;
                                    countTotalKesesuaianHubla.Add(tempTotalKesesuaianHubla2);

                                    if (i == (list.Count() - 1))
                                    {
                                        lastSpesifikasiJenisId_2 = findRekomendasiHubla.SpesifikasiJenis.Id;
                                        totalKesesuaianHubla = temp.RekomendasiHubla;

                                        CountData tempTotalKesesuaianHubla3 = new CountData();
                                        tempTotalKesesuaianHubla3.TrxId = lastSpesifikasiJenisId_2;
                                        tempTotalKesesuaianHubla3.TotalCount = temp.RekomendasiHubla;
                                        countTotalKesesuaianHubla.Add(tempTotalKesesuaianHubla3);
                                    }
                                    else
                                    {
                                        lastSpesifikasiJenisId_2 = findRekomendasiHubla.SpesifikasiJenis.Id;
                                        totalKesesuaianHubla = temp.RekomendasiHubla;
                                    }
                                }
                            }
                        }
                    } else
                    {
                        temp.RekomendasiHubla = 0;
                        temp.TotalExistingKeseluruhan = 0;
                    }

                    //COUNT SELISIH HUBLA
                    if(countTotalExistingJenis.Count() > 0)
                    {
                        for(int j=0; j < countTotalExistingJenis.Count(); j++)
                        {
                            var findTotalKebutuhanHubla = countTotalKesesuaianHubla.Find(b => b.TrxId == countTotalExistingJenis[j].TrxId);
                            if(findTotalKebutuhanHubla != null)
                            {
                                countTotalExistingJenis[j].SelisihHubla = countTotalExistingJenis[j].TotalCount - findTotalKebutuhanHubla.TotalCount;
                            } else
                            {
                                countTotalExistingJenis[j].SelisihHubla = countTotalExistingJenis[j].TotalCount;
                            }
                        }
                    }

                    temp.SatuanJenis = list[i].SpesifikasiJenis != null ? list[i].SpesifikasiJenis.Jenis.Satuan : "-";
                    temp.Port = list[i].Port;
                    temp.QRCode = list[i].QRCode;
                    temp.DetailExisting = list[i].DetailExisting;
                    temp.Kondisi = list[i].Kondisi;
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

            if (countTotalExistingJenis.Count() > 0)
            {
                for (int i = 0; i < result.Count(); i++)
                {
                    var find = countTotalExistingJenis.Find(b => b.TrxId == result[i].SpesifikasiJenisId);
                    if (find != null)
                    {
                        result[i].TotalExistingJenis = find.TotalCount;
                        result[i].SelisihHubla = find.SelisihHubla;
                        if(find.SelisihHubla >= 0)
                        {
                            result[i].KesesuaianPM58 = "TERPENUHI";
                        } else
                        {
                            result[i].KesesuaianPM58 = "KURANG";
                        }
                    }
                }
            }

            if (countTotalKesesuaianHubla.Count() > 0)
            {
                for (int i = 0; i < result.Count(); i++)
                {
                    var find = countTotalKesesuaianHubla.Find(b => b.TrxId == result[i].SpesifikasiJenisId);
                    if(find != null)
                    {
                        result[i].TotalKebutuhanHubla = find.TotalCount;
                    }
                }
            }

            if (countTotalExistingKeseluruhan.Count() > 0)
            {
                for (int i = 0; i < result.Count(); i++)
                {
                    var find = countTotalExistingKeseluruhan.Find(b => b.TrxId == result[i].PeralatanOSRId);
                    if (find != null)
                    {
                        result[i].TotalExistingKeseluruhan = find.TotalCount;
                    }
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
                //result.KesesuaianPM58 = data.KesesuaianPM58;
                result.PersentaseHubla = data.PersentaseHubla;
                result.TotalKebutuhanOSCP = data.TotalKebutuhanOSCP;
                result.SelisihOSCP = data.SelisihOSCP;
                result.KesesuaianOSCP = data.KesesuaianOSCP;
                result.PersentaseOSCP = data.PersentaseOSCP;
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit([FromForm] LLPTrxModel model, CancellationToken cancellationToken)
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
                //data.KesesuaianPM58 = model.KesesuaianPM58;
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
                //data.KesesuaianPM58 = model.KesesuaianPM58;
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

            if (model.Files.Count() > 0)
            {
                for(int i=0; i < model.Files.Count(); i++)
                {
                    await UploadFileWithReturn(path: $"OMNI/{data.Id}/Files/", createBy: data.CreatedBy, trxId: data.Id, file: model.Files[i], Flag: GeneralConstants.OMNI_LLP, isUpdate: model.Files != null, remark: null);
                }
                
            }
                

            //_dbOMNI.LLPTrx.Update(data);
            //await _dbOMNI.SaveChangesAsync(cancellationToken);

            return Ok(new ReturnJson { });
        }

        //// DELETE api/<ValuesController>/5
        [HttpDelete("{id:int}")]
        public async Task<LLPTrx> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            LLPTrx data = await _dbOMNI.LLPTrx.Where(b => b.Id == id).Include(b => b.SpesifikasiJenis).FirstOrDefaultAsync(cancellationToken);
            data.IsDeleted = GeneralConstants.YES;
            data.UpdatedBy = "admin";
            data.UpdatedAt = DateTime.Now;
            _dbOMNI.LLPTrx.Update(data);
            await _dbOMNI.SaveChangesAsync(cancellationToken);

            return data;
        }
    }
}
