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

        public class CountData
        {
            public int TrxId { get; set; }
            public decimal TotalCount { get; set; }
        }

        // GET: api/<ValuesController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(string port, CancellationToken cancellationToken)
        {
            int lastPeralatanOSRId = 0;
            decimal totalKesesuaianHubla = 0;

            List<CountData> countTotalKesesuaianHublaList = new List<CountData>();
            List<LLPTrxModel> result = new List<LLPTrxModel>();
            List<RekomendasiJenis> rekomenJenisList = await _dbOMNI.RekomendasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port).Include(b => b.SpesifikasiJenis).Include(b => b.SpesifikasiJenis.PeralatanOSR).ToListAsync(cancellationToken);

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

                    if(rekomenJenisList.Count() > 0 && list[i].SpesifikasiJenis != null)
                    {
                        var findRekomenJenis = rekomenJenisList.FindAll(b => b.SpesifikasiJenis.Id == list[i].SpesifikasiJenis.Id).FirstOrDefault();
                        if(findRekomenJenis != null)
                        {
                            temp.RekomendasiHubla = findRekomenJenis.Value;

                            if (i == 0)
                            {
                                lastPeralatanOSRId = list[i].SpesifikasiJenis.PeralatanOSR.Id;
                                
                                if (i == (list.Count() - 1))
                                {
                                    CountData tempKesesuaianHubla = new CountData();
                                    tempKesesuaianHubla.TrxId = lastPeralatanOSRId;
                                    tempKesesuaianHubla.TotalCount = findRekomenJenis.Value;
                                    countTotalKesesuaianHublaList.Add(tempKesesuaianHubla);
                                } else
                                {
                                    totalKesesuaianHubla += findRekomenJenis.Value;
                                }
                            } else
                            {
                                if(lastPeralatanOSRId == list[i].SpesifikasiJenis.PeralatanOSR.Id)
                                {
                                    totalKesesuaianHubla += findRekomenJenis.Value;
                                } else
                                {
                                    CountData tempKesesuaianHubla1 = new CountData();
                                    tempKesesuaianHubla1.TrxId = lastPeralatanOSRId;
                                    tempKesesuaianHubla1.TotalCount = totalKesesuaianHubla;
                                    countTotalKesesuaianHublaList.Add(tempKesesuaianHubla1);

                                    if (i == (list.Count() - 1))
                                    {
                                        lastPeralatanOSRId = list[i].SpesifikasiJenis.PeralatanOSR.Id;
                                        totalKesesuaianHubla = findRekomenJenis.Value;

                                        CountData tempKesesuaianHubla2 = new CountData();
                                        tempKesesuaianHubla2.TrxId = lastPeralatanOSRId;
                                        tempKesesuaianHubla2.TotalCount = findRekomenJenis.Value;
                                        countTotalKesesuaianHublaList.Add(tempKesesuaianHubla2);
                                    }
                                    else
                                    {
                                        lastPeralatanOSRId = list[i].SpesifikasiJenis.PeralatanOSR.Id;
                                        totalKesesuaianHubla = findRekomenJenis.Value;
                                    }
                                }
                            }
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

            if (countTotalKesesuaianHublaList.Count() > 0)
            {
                for (int i = 0; i < result.Count(); i++)
                {
                    var find = countTotalKesesuaianHublaList.Find(b => b.TrxId == result[i].PeralatanOSRId);
                    if(find != null)
                    {
                        result[i].TotalKebutuhanHubla = find.TotalCount;
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
                result.KesesuaianMP58 = data.KesesuaianMP58;
                result.PersentaseHubla = data.PersentaseHubla;
                result.TotalKebutuhanOSCP = data.TotalKebutuhanOSCP;
                result.SelisihOSCP = data.SelisihOSCP;
                result.KesesuaianOSCP = data.KesesuaianOSCP;
                result.PersentaseOSCP = data.PersentaseOSCP;
            }
            return Ok(result);
        }

        public async Task<int> CountTotalExistingKeseluruhan(string port, CancellationToken cancellationToken)
        {
            decimal totalExistingKeseluruhan = 0;
            int lastPeralatanOSRId = 0;
            List<CountData> countDataList = new List<CountData>();
            var llpTrxList = await _dbOMNI.LLPTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port).Include(b => b.SpesifikasiJenis).Include(b => b.SpesifikasiJenis.PeralatanOSR).ToListAsync(cancellationToken);
            if (llpTrxList.Count() > 0)
            {
                CountData temp = new CountData();
                for (int i=0; i < llpTrxList.Count(); i++)
                {
                    if (i == 0)
                    {
                        if (llpTrxList.Count() == 1)
                        {
                            llpTrxList[i].TotalExistingKeseluruhan = llpTrxList[i].DetailExisting;
                            _dbOMNI.LLPTrx.Update(llpTrxList[i]);
                            await _dbOMNI.SaveChangesAsync(cancellationToken);
                        }
                        else
                        {
                            lastPeralatanOSRId = llpTrxList[i].SpesifikasiJenis.PeralatanOSR.Id;
                            totalExistingKeseluruhan += llpTrxList[i].DetailExisting;
                        }
                    } 
                    else
                    {
                        if (lastPeralatanOSRId == llpTrxList[i].SpesifikasiJenis.PeralatanOSR.Id)
                        {
                            if (i == (llpTrxList.Count() - 1))
                            {
                                totalExistingKeseluruhan += llpTrxList[i].DetailExisting;

                                temp.TrxId = lastPeralatanOSRId;
                                temp.TotalCount = totalExistingKeseluruhan;
                                countDataList.Add(temp);
                            }
                            else
                            {
                                totalExistingKeseluruhan += llpTrxList[i].DetailExisting;
                            }
                        }
                        else
                        {

                            temp.TrxId = lastPeralatanOSRId;
                            temp.TotalCount = totalExistingKeseluruhan;
                            countDataList.Add(temp);

                            if (i == (llpTrxList.Count() - 1))
                            {
                                lastPeralatanOSRId = llpTrxList[i].SpesifikasiJenis.PeralatanOSR.Id;
                                totalExistingKeseluruhan = llpTrxList[i].DetailExisting;

                                temp.TrxId = lastPeralatanOSRId;
                                temp.TotalCount = totalExistingKeseluruhan;
                                countDataList.Add(temp);
                            }
                            else
                            {
                                lastPeralatanOSRId = llpTrxList[i].SpesifikasiJenis.PeralatanOSR.Id;
                                totalExistingKeseluruhan = llpTrxList[i].DetailExisting;
                            }
                        }
                    }
                }

                if (countDataList.Count() > 0)
                {
                    for (int i = 0; i < countDataList.Count(); i++)
                    {
                        var findLLPTrx = llpTrxList.FindAll(b => b.SpesifikasiJenis.PeralatanOSR.Id == countDataList[i].TrxId).ToList();
                        if (findLLPTrx.Count() > 0)
                        {
                            for (int j = 0; j < findLLPTrx.Count(); j++)
                            {
                                findLLPTrx[j].TotalExistingKeseluruhan = countDataList[i].TotalCount;
                                _dbOMNI.LLPTrx.Update(findLLPTrx[j]);
                                await _dbOMNI.SaveChangesAsync(cancellationToken);
                            }
                        }
                    }
                }
            }

            return 0;
        }

        public async Task<int> CountTotalExistingJenis(string port, CancellationToken cancellationToken)
        {
            decimal totalDetailExisting = 0;
            int lastSpesifikasiJenisId = 0;
            List<CountData> countDataList = new List<CountData>();
            var llpTrxList = await _dbOMNI.LLPTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port).Include(b => b.SpesifikasiJenis).ToListAsync(cancellationToken);
            if(llpTrxList.Count() > 0)
            {
                CountData temp = new CountData();
                for(int i=0; i < llpTrxList.Count(); i++)
                {
                    if(i == 0)
                    {
                        if(llpTrxList.Count() == 1)
                        {
                            llpTrxList[i].TotalExistingJenis = llpTrxList[i].DetailExisting;
                            _dbOMNI.LLPTrx.Update(llpTrxList[i]);
                            await _dbOMNI.SaveChangesAsync(cancellationToken);
                        } else
                        {
                            lastSpesifikasiJenisId = llpTrxList[i].SpesifikasiJenis.Id;
                            totalDetailExisting += llpTrxList[i].DetailExisting;
                        }
                    } else
                    {
                        if(lastSpesifikasiJenisId == llpTrxList[i].SpesifikasiJenis.Id)
                        {
                            if(i == (llpTrxList.Count() - 1))
                            {
                                totalDetailExisting += llpTrxList[i].DetailExisting;

                                temp.TrxId = lastSpesifikasiJenisId;
                                temp.TotalCount = totalDetailExisting;
                                countDataList.Add(temp);
                            } else
                            {
                                totalDetailExisting += llpTrxList[i].DetailExisting;
                            }
                        } else
                        {

                            temp.TrxId = lastSpesifikasiJenisId;
                            temp.TotalCount = totalDetailExisting;
                            countDataList.Add(temp);

                            if (i == (llpTrxList.Count() - 1))
                            {
                                lastSpesifikasiJenisId = llpTrxList[i].SpesifikasiJenis.Id;
                                totalDetailExisting = llpTrxList[i].DetailExisting;

                                temp.TrxId = lastSpesifikasiJenisId;
                                temp.TotalCount = totalDetailExisting;
                                countDataList.Add(temp);
                            } else
                            {
                                lastSpesifikasiJenisId = llpTrxList[i].SpesifikasiJenis.Id;
                                totalDetailExisting = llpTrxList[i].DetailExisting;
                            }
                        }
                    }
                }

                if (countDataList.Count() > 0)
                {
                    for (int i = 0; i < countDataList.Count(); i++)
                    {
                        var findLLPTrx = llpTrxList.FindAll(b => b.SpesifikasiJenis.Id == countDataList[i].TrxId).ToList();
                        if (findLLPTrx.Count() > 0)
                        {
                            for (int j = 0; j < findLLPTrx.Count(); j++)
                            {
                                findLLPTrx[j].TotalExistingJenis = countDataList[i].TotalCount;
                                _dbOMNI.LLPTrx.Update(findLLPTrx[j]);
                                await _dbOMNI.SaveChangesAsync(cancellationToken);
                            }
                        }
                    }
                }
            }
            return 0;
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

            await CountTotalExistingJenis(model.Port, cancellationToken);
            await CountTotalExistingKeseluruhan(model.Port, cancellationToken);


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

            await CountTotalExistingJenis(data.Port, cancellationToken);
            await CountTotalExistingKeseluruhan(data.Port, cancellationToken);

            return data;
        }
    }
}
