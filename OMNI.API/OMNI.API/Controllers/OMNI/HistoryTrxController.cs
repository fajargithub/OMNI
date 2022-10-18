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
    public class HistoryTrxController : BaseController
    {
        public HistoryTrxController(OMNIDbContext dbOMNI, MinioClient mc) : base(dbOMNI, mc)
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

        [HttpGet("GetAllHistoryHistoryLLPTrx")]
        public async Task<IActionResult> GetAllHistoryHistoryLLPTrx(int trxId, string port, int year, CancellationToken cancellationToken)
        {
            int lastSpesifikasiJenisId = 0;
            int lastSpesifikasiJenisId_2 = 0;
            int lastPeralatanOSRId = 0;
            int lastPeralatanOSRId_2 = 0;
            decimal totalKesesuaianHubla = 0;
            decimal totalKesesuaianOSCP = 0;
            decimal totalExistingKeseluruhan = 0;
            decimal totalDetailExisting = 0;

            List<CountData> countTotalExistingJenis = new List<CountData>();
            List<CountData> countTotalKesesuaianHubla = new List<CountData>();
            List<CountData> countTotalKesesuaianOSCP = new List<CountData>();
            List<CountData> countTotalExistingKeseluruhan = new List<CountData>();

            List<HistoryLLPTrxModel> result = new List<HistoryLLPTrxModel>();
            List<RekomendasiJenis> rekomenJenisList = await _dbOMNI.RekomendasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port).Include(b => b.SpesifikasiJenis).Include(b => b.SpesifikasiJenis.PeralatanOSR).Include(b => b.RekomendasiType).ToListAsync(cancellationToken);

            try
            {
                var list = await _dbOMNI.HistoryLLPTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port && b.Year == year && b.LLPTrxId == trxId)
               .Include(b => b.SpesifikasiJenis)
               .Include(b => b.SpesifikasiJenis.PeralatanOSR)
               .Include(b => b.SpesifikasiJenis.Jenis)
               .OrderBy(b => b.SpesifikasiJenis.PeralatanOSR).ToListAsync(cancellationToken);
                if (list.Count() > 0)
                {
                    for (int i = 0; i < list.Count(); i++)
                    {
                        HistoryLLPTrxModel temp = new HistoryLLPTrxModel();
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

                            if (findRekomendasiHubla != null)
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

                            //COUNT TOTAL KETUBUHAN SESUAI OSCP
                            if (findRekomendasiOSCP != null)
                            {
                                temp.RekomendasiOSCP = findRekomendasiOSCP.Value;
                                if (i == 0)
                                {
                                    lastPeralatanOSRId_2 = list[i].SpesifikasiJenis.PeralatanOSR.Id;

                                    if (i == (list.Count() - 1))
                                    {
                                        CountData tempTotalKesesuaianOSCP = new CountData();
                                        tempTotalKesesuaianOSCP.TrxId = lastPeralatanOSRId_2;
                                        tempTotalKesesuaianOSCP.TotalCount = findRekomendasiOSCP.Value;
                                        countTotalKesesuaianOSCP.Add(tempTotalKesesuaianOSCP);


                                    }
                                    else
                                    {
                                        totalKesesuaianOSCP += findRekomendasiOSCP.Value;
                                    }
                                }
                                else
                                {
                                    if (lastPeralatanOSRId_2 == list[i].SpesifikasiJenis.PeralatanOSR.Id)
                                    {
                                        totalKesesuaianOSCP += findRekomendasiOSCP.Value;
                                    }
                                    else
                                    {
                                        CountData tempTotalKesesuaianOSCP = new CountData();
                                        tempTotalKesesuaianOSCP.TrxId = lastPeralatanOSRId_2;
                                        tempTotalKesesuaianOSCP.TotalCount = totalKesesuaianOSCP;
                                        countTotalKesesuaianOSCP.Add(tempTotalKesesuaianOSCP);

                                        if (i == (list.Count() - 1))
                                        {
                                            lastPeralatanOSRId_2 = list[i].SpesifikasiJenis.PeralatanOSR.Id;
                                            totalKesesuaianOSCP = findRekomendasiOSCP.Value;

                                            CountData tempTotalKesesuaianOSCP2 = new CountData();
                                            tempTotalKesesuaianOSCP2.TrxId = lastPeralatanOSRId_2;
                                            tempTotalKesesuaianOSCP2.TotalCount = findRekomendasiOSCP.Value;
                                            countTotalKesesuaianOSCP.Add(tempTotalKesesuaianOSCP2);
                                        }
                                        else
                                        {
                                            lastPeralatanOSRId_2 = list[i].SpesifikasiJenis.PeralatanOSR.Id;
                                            totalKesesuaianOSCP = findRekomendasiOSCP.Value;
                                        }
                                    }
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

                        //COUNT SELISIH HUBLA
                        if (countTotalExistingJenis.Count() > 0)
                        {
                            for (int j = 0; j < countTotalExistingJenis.Count(); j++)
                            {
                                var findTotalKebutuhanHubla = countTotalKesesuaianHubla.Find(b => b.TrxId == countTotalExistingJenis[j].TrxId);
                                if (findTotalKebutuhanHubla != null)
                                {
                                    countTotalExistingJenis[j].SelisihHubla = countTotalExistingJenis[j].TotalCount - findTotalKebutuhanHubla.TotalCount;
                                }
                                else
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
                        temp.Brand = list[i].Brand;
                        temp.SerialNumber = list[i].SerialNumber;
                        temp.Remark = list[i].Remark;
                        temp.NoAsset = list[i].NoAsset;
                        temp.Status = list[i].Status;
                        temp.CreateDate = list[i].CreatedAt.ToString("dd MMM yyyy");
                        temp.CreatedBy = list[i].CreatedBy;
                        result.Add(temp);
                    }

                    HistoryLLPTrxModel totalPersentase = new HistoryLLPTrxModel();
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

                            if (result[i].RekomendasiHubla > 0)
                            {
                                result[i].PersentaseHubla = Math.Round(find.TotalCount / result[i].RekomendasiHubla * 100, 2);
                                if (result[i].PersentaseHubla > 100)
                                {
                                    result[i].PersentaseHubla = 100;
                                }
                            }

                            if (result[i].RekomendasiOSCP > 0)
                            {
                                result[i].PersentaseOSCP = Math.Round(find.TotalCount / result[i].RekomendasiOSCP * 100, 2);
                                if (result[i].PersentaseOSCP > 100)
                                {
                                    result[i].PersentaseOSCP = 100;
                                }
                            }

                            result[i].SelisihHubla = find.SelisihHubla;
                            if (find.SelisihHubla >= 0)
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
                        var find = countTotalKesesuaianHubla.Find(b => b.TrxId == result[i].SpesifikasiJenisId);
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
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
            }

            return Ok(result);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            LampiranModel result = new LampiranModel();
            var temp = await _dbOMNI.Lampiran.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == id).FirstOrDefaultAsync(cancellationToken);
            if (temp != null)
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
                data.EndDate = string.IsNullOrEmpty(model.EndDate) ? (DateTime?)null : DateTime.ParseExact(model.EndDate, "MM/dd/yyyy", null);
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
                        if (findPengesahan != null)
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
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            string fileFlag = "";

            if (data.LampiranType == "PENILAIAN")
            {
                fileFlag = GeneralConstants.OSMOSYS_PENILAIAN;
            }
            else if (data.LampiranType == "PENGESAHAN")
            {
                fileFlag = GeneralConstants.OSMOSYS_PENGESAHAN;
            }
            else if (data.LampiranType == "VERIFIKASI1" || data.LampiranType == "VERIFIKASI2")
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
