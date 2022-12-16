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
        private CorePTKContext _corePTKDb;
        public LLPTrxController(CorePTKContext dbCorePTK, OMNIDbContext dbOMNI, MinioClient mc) : base(dbOMNI, mc)
        {
            _dbOMNI = dbOMNI;
            _corePTKDb = dbCorePTK;
            _mc = mc;
        }

        public class CountData
        {
            public int TrxId { get; set; }
            public decimal TotalCount { get; set; }
            public decimal SelisihHubla { get; set; }
            public string KesesuaianPM58 { get; set; }
        }

        [HttpGet("GetLastNoAsset")]
        public async Task<IActionResult> GetLastNoAsset(string inventoryNumber, int primaryId, CancellationToken cancellationToken)
        {
            int newNumber = 1;
            List<int> listNumber = new List<int>();
            List<string> result = await _dbOMNI.LLPTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.QRCodeText.Contains(inventoryNumber)).Select(b => b.QRCodeText).ToListAsync(cancellationToken);

            if (result.Count() > 0)
            {
                for(int i=0; i < result.Count(); i++)
                {
                    var arrTemp = result[i].Split("-");
                    listNumber.Add(int.Parse(arrTemp[3]));
                }

                if(listNumber.Count() > 0)
                {
                    int maxNumber = listNumber.Max();
                    newNumber = maxNumber + newNumber;
                }
            }

            return Ok(newNumber);
        }

        public async Task<int> GetLastNoAssetStr(string inventoryNumber, int primaryId, CancellationToken cancellationToken)
        {
            int newNumber = 1;
            List<int> listNumber = new List<int>();
            List<string> result = await _dbOMNI.LLPTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.QRCodeText.Contains(inventoryNumber)).Select(b => b.QRCodeText).ToListAsync(cancellationToken);

            if (result.Count() > 0)
            {
                for (int i = 0; i < result.Count(); i++)
                {
                    var arrTemp = result[i].Split("-");
                    listNumber.Add(int.Parse(arrTemp[3]));
                }

                if (listNumber.Count() > 0)
                {
                    int maxNumber = listNumber.Max();
                    newNumber = maxNumber + newNumber;
                }
            }

            return newNumber;
        }

        [HttpGet("GetCopyDataYear")]
        public async Task<IActionResult> GetCopyDataYear(string port, CancellationToken cancellationToken)
        {
            List<int> yearList = new List<int>();

            var list = await _dbOMNI.LLPTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port).ToListAsync(cancellationToken);

            if(list.Count() > 0)
            {
                var group = list.GroupBy(b => b.Year).ToList();
                if(group.Count() > 0)
                {
                    for(int i=0; i < group.Count(); i++)
                    {
                        yearList.Add(group[i].Key);
                    }
                }
            }
            return Ok(yearList);
        }

        [HttpGet("CopyData")]
        public async Task<IActionResult> CopyData(string port, int year, int targetYear, CancellationToken cancellationToken)
        {
            List<int> ListId = new List<int>();

            try
            {
                var list = await _dbOMNI.LLPTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port && b.Year == year).Include(b => b.SpesifikasiJenis).Include(b => b.SpesifikasiJenis.Jenis).ToListAsync(cancellationToken);

                if (list.Count() > 0)
                {
                    for (int i = 0; i < list.Count(); i++)
                    {
                        LLPTrx data = new LLPTrx();

                        data.SpesifikasiJenis = list[i].SpesifikasiJenis;
                        data.Port = list[i].Port;
                        //data.QRCode = list[i].QRCode;
                        data.QRCodeText = list[i].QRCodeText;
                        //if (!string.IsNullOrWhiteSpace(list[i].QRCodeText))
                        //{
                        //    var arrTemp = list[i].QRCodeText.Split("-");
                        //    var newNoAsset = await GetLastNoAssetStr(arrTemp[0], 0, cancellationToken);
                        //    var newInv = arrTemp[0] + "-" + targetYear + "-" + arrTemp[2] + "-" + newNoAsset;

                        //    data.QRCodeText = newInv;
                        //}
                        
                        data.DetailExisting = list[i].DetailExisting;
                        data.Kondisi = list[i].Kondisi;
                        data.TotalExistingJenis = list[i].TotalExistingJenis;
                        data.TotalExistingKeseluruhan = list[i].TotalExistingKeseluruhan;
                        data.TotalKebutuhanHubla = list[i].TotalKebutuhanHubla;
                        data.SelisihHubla = list[i].SelisihHubla;
                        data.PersentaseHubla = list[i].PersentaseHubla;
                        data.TotalKebutuhanOSCP = list[i].TotalKebutuhanOSCP;
                        data.SelisihOSCP = list[i].SelisihOSCP;
                        data.KesesuaianOSCP = list[i].KesesuaianOSCP;
                        data.PersentaseOSCP = list[i].PersentaseOSCP;
                        data.Year = targetYear;
                        data.Brand = list[i].Brand;
                        data.NoAsset = list[i].NoAsset;
                        data.Status = list[i].Status;
                        data.SerialNumber = list[i].SerialNumber;
                        data.Remark = list[i].Remark;
                        data.CreatedAt = DateTime.Now;
                        data.CreatedBy = list[i].CreatedBy;
                        await _dbOMNI.LLPTrx.AddAsync(data, cancellationToken);
                        await _dbOMNI.SaveChangesAsync(cancellationToken);

                        LLPTrxModel tempModel = new LLPTrxModel();

                        tempModel.Jenis = data.SpesifikasiJenis.Jenis.Id.ToString();
                        tempModel.Port = data.Port;
                        //tempModel.QRCode = data.QRCode;
                        tempModel.QRCodeText = data.QRCodeText;
                        tempModel.DetailExisting = data.DetailExisting;
                        tempModel.Kondisi = data.Kondisi;
                        tempModel.TotalExistingJenis = data.TotalExistingJenis;
                        tempModel.TotalExistingKeseluruhan = data.TotalExistingKeseluruhan;
                        tempModel.TotalKebutuhanHubla = data.TotalKebutuhanHubla;
                        tempModel.SelisihHubla = data.SelisihHubla;
                        tempModel.PersentaseHubla = data.PersentaseHubla;
                        tempModel.TotalKebutuhanOSCP = data.TotalKebutuhanOSCP;
                        tempModel.SelisihOSCP = data.SelisihOSCP;
                        tempModel.KesesuaianOSCP = data.KesesuaianOSCP;
                        tempModel.PersentaseOSCP = data.PersentaseOSCP;
                        tempModel.Year = targetYear;
                        tempModel.Brand = data.Brand;
                        tempModel.NoAsset = data.NoAsset;
                        tempModel.Status = data.Status;
                        tempModel.SerialNumber = data.SerialNumber;
                        tempModel.Remark = data.Remark;
                        tempModel.CreatedAt = DateTime.Now;
                        tempModel.CreatedBy = data.CreatedBy;

                        //Add LLPTrx History
                        await AddHistory(data.Id, tempModel, "ADD", cancellationToken);

                        ListId.Add(data.Id);
                    }
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            return Ok(ListId);
        }

        [HttpGet("DeleteAllLLPTrx")]
        public async Task<IActionResult> DeleteAllLLPTrx(string port, int year, CancellationToken cancellationToken)
        {
            var status = "FAILED";

            try
            {
                var list = await _dbOMNI.LLPTrx.Where(b => b.Port == port && b.Year == year).Include(b => b.SpesifikasiJenis).ToListAsync(cancellationToken);
                if (list.Count() > 0)
                {
                    for (int i = 0; i < list.Count(); i++)
                    {
                        list[i].IsDeleted = GeneralConstants.YES;
                        list[i].UpdatedAt = DateTime.Now;
                        _dbOMNI.LLPTrx.Update(list[i]);
                        await _dbOMNI.SaveChangesAsync(cancellationToken);
                    }
                }

                status = "SUCCESS";
            } catch(Exception ex)
            {

            }
            

            return Ok(status);
        }


        // GET: api/<ValuesController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(string port, int year, int isUpdate, CancellationToken cancellationToken)
        {
            int lastSpesifikasiJenisId = 0;
            int lastSpesifikasiJenisId_2 = 0;
            int lastSpesifikasiJenisId_3 = 0;
            int lastPeralatanOSRId = 0;
            int lastPeralatanOSRId_2 = 0;
            int lastPeralatanOSRId_3 = 0;
            decimal totalKesesuaianHubla = 0;
            decimal totalKesesuaianOSCP = 0;
            decimal totalExistingKeseluruhan = 0;
            decimal totalDetailExisting = 0;

            List<CountData> countTotalExistingJenis = new List<CountData>();
            List<CountData> countTotalKesesuaianHubla = new List<CountData>();
            List<CountData> countTotalKesesuaianOSCP = new List<CountData>();
            List<CountData> countTotalExistingKeseluruhan = new List<CountData>();

            List<LLPTrxModel> result = new List<LLPTrxModel>();
            List<RekomendasiJenis> rekomenJenisList = await _dbOMNI.RekomendasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port && b.Year == year).Include(b => b.SpesifikasiJenis).Include(b => b.SpesifikasiJenis.PeralatanOSR).Include(b => b.RekomendasiType).ToListAsync(cancellationToken);

            try
            {
                var list = await _dbOMNI.LLPTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port && b.Year == year)
               .Include(b => b.SpesifikasiJenis)
               .Include(b => b.SpesifikasiJenis.PeralatanOSR)
               .Include(b => b.SpesifikasiJenis.Jenis)
               .OrderBy(b => b.SpesifikasiJenis.PeralatanOSR).OrderBy(b => b.SpesifikasiJenis.Jenis).ToListAsync(cancellationToken);
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

                        if (list[i].SpesifikasiJenis != null)
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

                                if (i == (list.Count() - 1))
                                {
                                    CountData tempExistingKeseluruhan1 = new CountData();
                                    tempExistingKeseluruhan1.TrxId = lastPeralatanOSRId;
                                    tempExistingKeseluruhan1.TotalCount = totalExistingKeseluruhan;
                                    countTotalExistingKeseluruhan.Add(tempExistingKeseluruhan1);
                                }
                                else
                                {
                                    totalExistingKeseluruhan += temp.RekomendasiHubla;
                                }
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
                            var findRekomendasiOSCP = rekomenJenisList.FindAll(b => b.SpesifikasiJenis.Id == list[i].SpesifikasiJenis.Id && b.RekomendasiType.Name.Contains("OSCP")).FirstOrDefault();

                            //COUNT TOTAL KETUBUHAN HUBLA
                            if (findRekomendasiHubla != null)
                            {
                                temp.RekomendasiHubla = findRekomendasiHubla.Value;
                                if (i == 0)
                                {
                                    lastPeralatanOSRId_2 = list[i].SpesifikasiJenis.PeralatanOSR.Id;

                                    if (i == (list.Count() - 1))
                                    {
                                        CountData tempTotalKesesuaianHubla = new CountData();
                                        tempTotalKesesuaianHubla.TrxId = lastPeralatanOSRId_2;
                                        tempTotalKesesuaianHubla.TotalCount = findRekomendasiHubla.Value;
                                        countTotalKesesuaianHubla.Add(tempTotalKesesuaianHubla);
                                    }
                                    else
                                    {
                                        lastPeralatanOSRId_2 = list[i].SpesifikasiJenis.PeralatanOSR.Id;
                                        totalKesesuaianHubla += findRekomendasiHubla.Value;
                                    }

                                    lastSpesifikasiJenisId_2 = list[i].SpesifikasiJenis.Id;
                                }
                                else
                                {
                                    if (lastPeralatanOSRId_2 == list[i].SpesifikasiJenis.PeralatanOSR.Id)
                                    {
                                        if (i == (list.Count() - 1))
                                        {
                                            if (lastSpesifikasiJenisId_2 != list[i].SpesifikasiJenis.Id)
                                            {
                                                totalKesesuaianHubla += findRekomendasiHubla.Value;
                                                lastSpesifikasiJenisId_2 = list[i].SpesifikasiJenis.Id;
                                            }

                                            CountData tempTotalKesesuaianHubla = new CountData();
                                            tempTotalKesesuaianHubla.TrxId = lastPeralatanOSRId_2;
                                            tempTotalKesesuaianHubla.TotalCount = totalKesesuaianHubla;
                                            countTotalKesesuaianHubla.Add(tempTotalKesesuaianHubla);
                                        }
                                        else
                                        {
                                            if (lastSpesifikasiJenisId_2 != list[i].SpesifikasiJenis.Id)
                                            {
                                                totalKesesuaianHubla += findRekomendasiHubla.Value;
                                                lastSpesifikasiJenisId_2 = list[i].SpesifikasiJenis.Id;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        CountData tempTotalKesesuaianHubla = new CountData();
                                        tempTotalKesesuaianHubla.TrxId = lastPeralatanOSRId_2;
                                        tempTotalKesesuaianHubla.TotalCount = totalKesesuaianHubla;
                                        countTotalKesesuaianHubla.Add(tempTotalKesesuaianHubla);

                                        if (i == (list.Count() - 1))
                                        {
                                            lastPeralatanOSRId_2 = list[i].SpesifikasiJenis.PeralatanOSR.Id;
                                            //totalKesesuaianHubla = findRekomendasiHubla.Value;
                                            if (lastSpesifikasiJenisId_2 != list[i].SpesifikasiJenis.Id)
                                            {
                                                totalKesesuaianHubla = findRekomendasiHubla.Value;
                                                lastSpesifikasiJenisId_2 = list[i].SpesifikasiJenis.Id;
                                            }

                                            CountData tempTotalKesesuaianHubla2 = new CountData();
                                            tempTotalKesesuaianHubla2.TrxId = lastPeralatanOSRId_2;
                                            tempTotalKesesuaianHubla2.TotalCount = findRekomendasiHubla.Value;
                                            countTotalKesesuaianHubla.Add(tempTotalKesesuaianHubla2);
                                        }
                                        else
                                        {
                                            lastPeralatanOSRId_2 = list[i].SpesifikasiJenis.PeralatanOSR.Id;
                                            if (lastSpesifikasiJenisId_2 != list[i].SpesifikasiJenis.Id)
                                            {
                                                totalKesesuaianHubla = findRekomendasiHubla.Value;
                                                lastSpesifikasiJenisId_2 = list[i].SpesifikasiJenis.Id;
                                            }
                                            //totalKesesuaianHubla = findRekomendasiHubla.Value;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (i == (list.Count() - 1))
                                {
                                    CountData tempTotalKesesuaianHubla = new CountData();
                                    tempTotalKesesuaianHubla.TrxId = lastPeralatanOSRId_2;
                                    tempTotalKesesuaianHubla.TotalCount = totalKesesuaianHubla;
                                    countTotalKesesuaianHubla.Add(tempTotalKesesuaianHubla);
                                }
                            }


                            //COUNT TOTAL KETUBUHAN SESUAI OSCP
                            if (findRekomendasiOSCP != null)
                            {
                                temp.RekomendasiOSCP = findRekomendasiOSCP.Value;
                                if (i == 0)
                                {
                                    lastPeralatanOSRId_3 = list[i].SpesifikasiJenis.PeralatanOSR.Id;

                                    if (i == (list.Count() - 1))
                                    {
                                        CountData tempTotalKesesuaianOSCP = new CountData();
                                        tempTotalKesesuaianOSCP.TrxId = lastPeralatanOSRId_3;
                                        tempTotalKesesuaianOSCP.TotalCount = findRekomendasiOSCP.Value;
                                        countTotalKesesuaianOSCP.Add(tempTotalKesesuaianOSCP);
                                    }
                                    else
                                    {
                                        lastPeralatanOSRId_3 = list[i].SpesifikasiJenis.PeralatanOSR.Id;
                                        totalKesesuaianOSCP += findRekomendasiOSCP.Value;
                                    }

                                    lastSpesifikasiJenisId_3 = list[i].SpesifikasiJenis.Id;
                                }
                                else
                                {
                                    if (lastPeralatanOSRId_3 == list[i].SpesifikasiJenis.PeralatanOSR.Id)
                                    {
                                        if (i == (list.Count() - 1))
                                        {
                                            if (lastSpesifikasiJenisId_3 != list[i].SpesifikasiJenis.Id)
                                            {
                                                totalKesesuaianOSCP = findRekomendasiOSCP.Value;
                                                lastSpesifikasiJenisId_3 = list[i].SpesifikasiJenis.Id;
                                            }

                                            CountData tempTotalKesesuaianOSCP = new CountData();
                                            tempTotalKesesuaianOSCP.TrxId = lastPeralatanOSRId_3;
                                            tempTotalKesesuaianOSCP.TotalCount = totalKesesuaianOSCP;
                                            countTotalKesesuaianOSCP.Add(tempTotalKesesuaianOSCP);
                                        }
                                        else
                                        {
                                            if(lastSpesifikasiJenisId_3 != list[i].SpesifikasiJenis.Id) {
                                                totalKesesuaianOSCP += findRekomendasiOSCP.Value;
                                                lastSpesifikasiJenisId_3 = list[i].SpesifikasiJenis.Id;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        CountData tempTotalKesesuaianOSCP = new CountData();
                                        tempTotalKesesuaianOSCP.TrxId = lastPeralatanOSRId_3;
                                        tempTotalKesesuaianOSCP.TotalCount = totalKesesuaianOSCP;
                                        countTotalKesesuaianOSCP.Add(tempTotalKesesuaianOSCP);

                                        if (i == (list.Count() - 1))
                                        {
                                            lastPeralatanOSRId_3 = list[i].SpesifikasiJenis.PeralatanOSR.Id;
                                            //totalKesesuaianOSCP = findRekomendasiOSCP.Value;
                                            if (lastSpesifikasiJenisId_3 != list[i].SpesifikasiJenis.Id)
                                            {
                                                totalKesesuaianOSCP = findRekomendasiOSCP.Value;
                                                lastSpesifikasiJenisId_3 = list[i].SpesifikasiJenis.Id;
                                            }

                                            CountData tempTotalKesesuaianOSCP2 = new CountData();
                                            tempTotalKesesuaianOSCP2.TrxId = lastPeralatanOSRId_3;
                                            tempTotalKesesuaianOSCP2.TotalCount = findRekomendasiOSCP.Value;
                                            countTotalKesesuaianOSCP.Add(tempTotalKesesuaianOSCP2);
                                        }
                                        else
                                        {
                                            lastPeralatanOSRId_3 = list[i].SpesifikasiJenis.PeralatanOSR.Id;
                                            if (lastSpesifikasiJenisId_3 != list[i].SpesifikasiJenis.Id)
                                            {
                                                totalKesesuaianOSCP = findRekomendasiOSCP.Value;
                                                lastSpesifikasiJenisId_3 = list[i].SpesifikasiJenis.Id;
                                            }
                                            //totalKesesuaianOSCP = findRekomendasiOSCP.Value;
                                        }
                                    }
                                }
                            } else
                            {
                                if (i == (list.Count() - 1))
                                {
                                    CountData tempTotalKesesuaianOSCP = new CountData();
                                    tempTotalKesesuaianOSCP.TrxId = lastPeralatanOSRId_3;
                                    tempTotalKesesuaianOSCP.TotalCount = totalKesesuaianOSCP;
                                    countTotalKesesuaianOSCP.Add(tempTotalKesesuaianOSCP);
                                }
                            }
                        }
                        else
                        {
                            temp.RekomendasiHubla = 0;
                            temp.RekomendasiOSCP = 0;
                            temp.TotalExistingKeseluruhan = 0;
                            temp.TotalKebutuhanOSCP = 0;
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
                        temp.Brand = list[i].Brand;
                        temp.SerialNumber = list[i].SerialNumber;
                        temp.Remark = list[i].Remark;
                        temp.NoAsset = list[i].NoAsset;
                        temp.Status = list[i].Status;
                        temp.CreateDate = list[i].CreatedAt.ToString("dd MMM yyyy");
                        temp.CreatedBy = list[i].CreatedBy;
                        result.Add(temp);
                    }

                    LLPTrxModel totalPersentase = new LLPTrxModel();
                    totalPersentase.PeralatanOSR = "Total Persentase";
                    result.Add(totalPersentase);
                }

                if (countTotalExistingJenis.Count() > 0)
                {
                    for (int i = 0; i < result.Count(); i++)
                    {
                        var find = countTotalExistingJenis.Find(b => b.TrxId == result[i].SpesifikasiJenisId);
                        if (find != null)
                        {
                            result[i].TotalExistingJenis = find.TotalCount;

                            if(result[i].RekomendasiHubla > 0)
                            {
                                result[i].PersentaseHubla = Math.Round(find.TotalCount / result[i].RekomendasiHubla * 100, 2);
                                if (result[i].PersentaseHubla > 100)
                                {
                                    result[i].PersentaseHubla = 100;
                                }
                            }
                           
                            if(result[i].RekomendasiOSCP > 0)
                            {
                                result[i].PersentaseOSCP = Math.Round(find.TotalCount / result[i].RekomendasiOSCP * 100, 2);
                                if (result[i].PersentaseOSCP > 100)
                                {
                                    result[i].PersentaseOSCP = 100;
                                }
                            }

                            result[i].SelisihHubla = find.TotalCount - result[i].RekomendasiHubla;
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

                if (countTotalKesesuaianHubla.Count() > 0)
                {
                    for (int i = 0; i < result.Count(); i++)
                    {
                        var find = countTotalKesesuaianHubla.Find(b => b.TrxId == result[i].PeralatanOSRId);
                        if (find != null)
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

                if (countTotalKesesuaianOSCP.Count() > 0)
                {
                    for (int i = 0; i < result.Count(); i++)
                    {
                        var find = countTotalKesesuaianOSCP.Find(b => b.TrxId == result[i].PeralatanOSRId);
                        if (find != null)
                        {
                            result[i].TotalKebutuhanOSCP = find.TotalCount;
                        }
                    }
                }

                //COUNT SELISIH OSCP
                if (countTotalExistingJenis.Count() > 0)
                {
                    for (int j = 0; j < result.Count(); j++)
                    {
                        var findTotalExistingJenis = countTotalExistingJenis.Find(b => b.TrxId == result[j].SpesifikasiJenisId);
                        if (findTotalExistingJenis != null)
                        {
                            result[j].SelisihOSCP = findTotalExistingJenis.TotalCount - result[j].RekomendasiOSCP;
                            if (result[j].SelisihOSCP >= 0)
                            {
                                result[j].KesesuaianOSCP = "TERPENUHI";
                            }
                            else
                            {
                                result[j].KesesuaianOSCP = "KURANG";
                            }
                        }
                    }
                }
            } catch (Exception Ex)
            {
                Console.WriteLine(Ex);
            }

            //if(isUpdate == 1)
            //{
            //    if(result.Count() > 0)
            //    {
            //        await UpdateLLPTrx(result, cancellationToken);
            //    }
            //}
           
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
                result.PeralatanOSRName = data.SpesifikasiJenis != null ? data.SpesifikasiJenis.PeralatanOSR.Name : "-";
                result.Jenis = data.SpesifikasiJenis != null ? data.SpesifikasiJenis.Id.ToString() : "-";
                result.JenisName = data.SpesifikasiJenis != null ? data.SpesifikasiJenis.Jenis.Name : "-";
                result.SatuanJenis = data.SpesifikasiJenis != null ? data.SpesifikasiJenis.Jenis.Satuan : "-";
                result.KodeInventory = data.SpesifikasiJenis != null ? data.SpesifikasiJenis.Jenis.KodeInventory : "-";
                result.Port = data.Port;
                result.QRCode = data.QRCode;
                result.QRCodeText = data.QRCodeText;
                result.Status = data.Status;
                result.DetailExisting = data.DetailExisting;
                result.Kondisi = data.Kondisi;
                result.TotalExistingJenis = data.TotalExistingJenis;
                result.TotalExistingKeseluruhan = data.TotalExistingKeseluruhan;
                result.TotalKebutuhanHubla = data.TotalKebutuhanHubla;
                result.SelisihHubla = data.SelisihHubla;
                result.Brand = data.Brand;
                result.NoAsset = data.NoAsset;
                result.SerialNumber = data.SerialNumber;
                result.Remark = data.Remark;
                result.PersentaseHubla = data.PersentaseHubla;
                result.TotalKebutuhanOSCP = data.TotalKebutuhanOSCP;
                result.SelisihOSCP = data.SelisihOSCP;
                result.KesesuaianOSCP = data.KesesuaianOSCP;
                result.PersentaseOSCP = data.PersentaseOSCP;
            }
            return Ok(result);
        }

        [HttpPost("UpdateQRCode")]
        public async Task<IActionResult> UpdateQRCode(QRCodeModel model, CancellationToken cancellationToken)
        {
            LLPTrx data = await _dbOMNI.LLPTrx.Where(b => b.Id == model.PrimaryId).FirstOrDefaultAsync(cancellationToken);
            data.QRCode = model.QRCode;
            _dbOMNI.LLPTrx.Update(data);
            await _dbOMNI.SaveChangesAsync(cancellationToken);

            //Update History LLPTrx
            HistoryLLPTrx history = await _dbOMNI.HistoryLLPTrx.Where(b => b.LLPTrxId == model.PrimaryId).OrderByDescending(b => b.Id).FirstOrDefaultAsync(cancellationToken);
            history.QRCode = model.QRCode;
            _dbOMNI.HistoryLLPTrx.Update(history);
            await _dbOMNI.SaveChangesAsync(cancellationToken);

            return Ok(new ReturnJson { Payload = data });
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit([FromForm] LLPTrxModel model, CancellationToken cancellationToken)
        {
            LLPTrx data = new LLPTrx();

            if (model.Id > 0)
            {
                //bool enableUpdate = false;
                bool enableUpdate = true;

                data = await _dbOMNI.LLPTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == model.Id)
                 .Include(b => b.SpesifikasiJenis)
                 .Include(b => b.SpesifikasiJenis.PeralatanOSR)
                 .Include(b => b.SpesifikasiJenis.Jenis)
                 .OrderBy(b => b.CreatedAt).FirstOrDefaultAsync(cancellationToken);

                var checkNoAsset = await _dbOMNI.LLPTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.QRCodeText == model.QRCodeText).FirstOrDefaultAsync(cancellationToken);
                if (checkNoAsset == null)
                {
                    data.QRCodeText = !string.IsNullOrWhiteSpace(model.QRCodeText) ? model.QRCodeText : "";
                }

                if (enableUpdate)
                {
                    data.SpesifikasiJenis = await _dbOMNI.SpesifikasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == int.Parse(model.Jenis)).FirstOrDefaultAsync(cancellationToken);

                    data.Port = model.Port;
                    //data.QRCodeText = !string.IsNullOrWhiteSpace(model.QRCodeText) ? model.QRCodeText : "";
                    data.DetailExisting = model.DetailExisting;
                    data.Kondisi = model.Kondisi;
                    data.TotalExistingJenis = model.TotalExistingJenis;
                    data.TotalExistingKeseluruhan = model.TotalExistingKeseluruhan;
                    data.TotalKebutuhanHubla = model.TotalKebutuhanHubla;
                    data.SelisihHubla = model.SelisihHubla;
                    data.PersentaseHubla = model.PersentaseHubla;
                    data.TotalKebutuhanOSCP = model.TotalKebutuhanOSCP;
                    data.SelisihOSCP = model.SelisihOSCP;
                    data.KesesuaianOSCP = model.KesesuaianOSCP;
                    data.PersentaseOSCP = model.PersentaseOSCP;
                    data.Year = model.Year;
                    data.Brand = model.Brand;
                    data.Status = model.Status;
                    data.NoAsset = model.NoAsset;
                    data.SerialNumber = model.SerialNumber;
                    data.Remark = model.Remark;
                    data.UpdatedAt = DateTime.Now;
                    data.UpdatedBy = model.UpdatedBy;
                    _dbOMNI.LLPTrx.Update(data);
                    await _dbOMNI.SaveChangesAsync(cancellationToken);

                    if (model.Files != null)
                    {
                        if (model.Files.Count() > 0)
                        {
                            for (int i = 0; i < model.Files.Count(); i++)
                            {
                                await UploadFileWithReturn(path: $"OMNI/{data.Id}/Files/", createBy: data.CreatedBy, trxId: data.Id, file: model.Files[i], Flag: GeneralConstants.OMNI_LLP, isUpdate: model.Files != null, remark: null);
                            }

                        }
                    }

                    //Add LLPTrx History
                    await AddHistory(data.Id, model, "UPDATE", cancellationToken);

                    await GetAll(data.Port, data.Year, 1, cancellationToken);

                    return Ok(new ReturnJson { Id = data.Id });
                } else
                {
                    return Ok(new ReturnJson { IsSuccess = false, ErrorMsg = "No Asset already exist" });
                }
            }
            else
            { 
                var checkNoAsset = await _dbOMNI.LLPTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.QRCodeText == model.QRCodeText).FirstOrDefaultAsync(cancellationToken);
                if (checkNoAsset != null)
                {
                    return Ok(new ReturnJson { IsSuccess = false, ErrorMsg = "No Asset already exist" });
                }
                else
                {
                    data.SpesifikasiJenis = await _dbOMNI.SpesifikasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == int.Parse(model.Jenis)).FirstOrDefaultAsync(cancellationToken);

                    data.Port = model.Port;
                    data.QRCodeText = !string.IsNullOrWhiteSpace(model.QRCodeText) ? model.QRCodeText : "";
                    data.DetailExisting = model.DetailExisting;
                    data.Kondisi = model.Kondisi;
                    data.TotalExistingJenis = model.TotalExistingJenis;
                    data.TotalExistingKeseluruhan = model.TotalExistingKeseluruhan;
                    data.TotalKebutuhanHubla = model.TotalKebutuhanHubla;
                    data.SelisihHubla = model.SelisihHubla;
                    data.PersentaseHubla = model.PersentaseHubla;
                    data.TotalKebutuhanOSCP = model.TotalKebutuhanOSCP;
                    data.SelisihOSCP = model.SelisihOSCP;
                    data.KesesuaianOSCP = model.KesesuaianOSCP;
                    data.PersentaseOSCP = model.PersentaseOSCP;
                    data.Year = model.Year;
                    data.Brand = model.Brand;
                    data.NoAsset = model.NoAsset;
                    data.Status = model.Status;
                    data.SerialNumber = model.SerialNumber;
                    data.Remark = model.Remark;
                    data.CreatedAt = DateTime.Now;
                    data.CreatedBy = model.CreatedBy;
                    await _dbOMNI.LLPTrx.AddAsync(data, cancellationToken);
                    await _dbOMNI.SaveChangesAsync(cancellationToken);

                    if (model.Files != null)
                    {
                        if (model.Files.Count() > 0)
                        {
                            for (int i = 0; i < model.Files.Count(); i++)
                            {
                                await UploadFileWithReturn(path: $"OMNI/{data.Id}/Files/", createBy: data.CreatedBy, trxId: data.Id, file: model.Files[i], Flag: GeneralConstants.OMNI_LLP, isUpdate: model.Files != null, remark: null);
                            }

                        }
                    }

                    //Add LLPTrx History
                    await AddHistory(data.Id, model, "ADD", cancellationToken);

                    await GetAll(data.Port, data.Year, 1, cancellationToken);

                    return Ok(new ReturnJson { Id = data.Id });
                }
            }
        }

        //// DELETE api/<ValuesController>/5
        [HttpDelete("{id:int}")]
        public async Task<LLPTrx> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            LLPTrx data = await _dbOMNI.LLPTrx.Where(b => b.Id == id).Include(b => b.SpesifikasiJenis).FirstOrDefaultAsync(cancellationToken);
            data.IsDeleted = GeneralConstants.YES;
            data.UpdatedAt = DateTime.Now;
            _dbOMNI.LLPTrx.Update(data);
            await _dbOMNI.SaveChangesAsync(cancellationToken);

            return data;
        }

        [HttpPost("AddEditFiles")]
        public async Task<IActionResult> AddEditFiles([FromForm] FilesModel model, CancellationToken cancellationToken)
        {

            FilesModel data = new FilesModel();
            data = model;
            data.CreatedBy = "admin";

            if (model.Files.Count() > 0)
            {
                for (int i = 0; i < model.Files.Count(); i++)
                {
                    await UploadFileWithReturn(path: $"OMNI/{data.TrxId}/Files/", createBy: data.CreatedBy, trxId: int.Parse(data.TrxId), file: model.Files[i], Flag: model.Flag, isUpdate: model.Files != null, remark: null);
                }

            }

            return Ok(new ReturnJson { });
        }

        [HttpPost("AddEditLLPHistoryStatus")]
        public async Task<IActionResult> AddEditLLPHistoryStatus(LLPHistoryStatusModel model, CancellationToken cancellationToken)
        {
            LLPHistoryStatus data = new LLPHistoryStatus();

            LLPTrx llpTrx = await _dbOMNI.LLPTrx.Where(b => b.Id == int.Parse(model.LLPTrx)).FirstOrDefaultAsync(cancellationToken);
            data.LLPTrx = llpTrx;
            data.PortFrom = model.PortFrom;
            data.PortTo = model.PortTo;
            data.Status = model.Status;

            if (!string.IsNullOrEmpty(model.StartDate))
            {
                data.StartDate = string.IsNullOrEmpty(model.StartDate) ? (DateTime?)null : DateTime.ParseExact(model.StartDate, "MM/dd/yyyy", null);
            } else
            {
                data.StartDate = (DateTime?)null;
            }

            if (!string.IsNullOrEmpty(model.EndDate))
            {
                data.EndDate = string.IsNullOrEmpty(model.EndDate) ? (DateTime?)null : DateTime.ParseExact(model.EndDate, "MM/dd/yyyy", null);
            }
            else
            {
                data.EndDate = (DateTime?)null;
            }
            
            data.Remark = model.Remark;
            data.CreatedAt = DateTime.Now;
            data.CreatedBy = model.CreatedBy;
            _dbOMNI.LLPHistoryStatus.Add(data);
            await _dbOMNI.SaveChangesAsync(cancellationToken);

            if (data.Id > 0)
            {
                llpTrx.Port = model.PortTo;
                llpTrx.Status = model.Status;
                llpTrx.UpdatedAt = DateTime.Now;
                llpTrx.UpdatedBy = model.UpdatedBy;
                _dbOMNI.LLPTrx.Update(llpTrx);
                await _dbOMNI.SaveChangesAsync(cancellationToken);
            }

            return Ok(new ReturnJson { });
        }

        public async Task<string> AddHistory(int TrxId, LLPTrxModel model, string TrxStatus, CancellationToken cancellationToken)
        {
            HistoryLLPTrx history = new HistoryLLPTrx();
            history.LLPTrxId = TrxId;
            history.SpesifikasiJenis = await _dbOMNI.SpesifikasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == int.Parse(model.Jenis)).FirstOrDefaultAsync(cancellationToken);
            history.Port = model.Port;
            history.QRCodeText = !string.IsNullOrWhiteSpace(model.QRCodeText) ? model.QRCodeText : "";
            history.DetailExisting = model.DetailExisting;
            history.Kondisi = model.Kondisi;
            history.TotalExistingJenis = model.TotalExistingJenis;
            history.TotalExistingKeseluruhan = model.TotalExistingKeseluruhan;
            history.TotalKebutuhanHubla = model.TotalKebutuhanHubla;
            history.SelisihHubla = model.SelisihHubla;
            history.PersentaseHubla = model.PersentaseHubla;
            history.TotalKebutuhanOSCP = model.TotalKebutuhanOSCP;
            history.SelisihOSCP = model.SelisihOSCP;
            history.KesesuaianOSCP = model.KesesuaianOSCP;
            history.PersentaseOSCP = model.PersentaseOSCP;
            history.Year = model.Year;
            history.Brand = model.Brand;
            history.NoAsset = model.NoAsset;
            history.Status = model.Status;
            history.SerialNumber = model.SerialNumber;
            history.Remark = model.Remark;
            history.CreatedAt = DateTime.Now;
            history.CreatedBy = model.CreatedBy;
            history.UpdatedAt = DateTime.Now;
            history.UpdatedBy = model.UpdatedBy;
            history.TrxStatus = TrxStatus;
            await _dbOMNI.HistoryLLPTrx.AddAsync(history, cancellationToken);
            await _dbOMNI.SaveChangesAsync(cancellationToken);

            return "OK";
        }

        //[HttpGet("GeneratePort")]
        //public async Task<IActionResult> GeneratePort(CancellationToken cancellationToken)
        //{
        //    var portList = await _corePTKDb.Port.Where(b => b.IsDeleted == GeneralConstants.NO && b.PAreaSub.Id > 0).Include(b => b.PAreaSub).OrderBy(b => b.Id).ToListAsync(cancellationToken);
        //    var dataList = await _dbOMNI.RekomendasiPersonil.Where(b => b.IsDeleted == GeneralConstants.NO).ToListAsync(cancellationToken);
        //    if(dataList.Count() > 0 && portList.Count() > 0)
        //    {
        //        foreach(var port in portList)
        //        {
        //            var findData = dataList.FindAll(b => b.Port == port.Name).ToList();
        //            if(findData.Count() > 0)
        //            {
        //                foreach(var data in findData)
        //                {
        //                    data.Port = port.Id.ToString();
        //                    _dbOMNI.RekomendasiPersonil.Update(data);
        //                    await _dbOMNI.SaveChangesAsync(cancellationToken);
        //                }
        //            }
        //        }
        //    }
        //    return Ok("OK");
        //} 
    }
}
