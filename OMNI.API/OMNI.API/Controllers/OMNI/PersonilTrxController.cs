using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minio;
using Minio.DataModel;
using Minio.Exceptions;
using OMNI.API.Model.OMNI;
using OMNI.API.Services.Interfaces;
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
        protected IPersonilTrx _personilTrxService;
        public PersonilTrxController(IPersonilTrx personilTrxService, OMNIDbContext dbOMNI, MinioClient mc) : base(dbOMNI, mc)
        {
            _dbOMNI = dbOMNI;
            _mc = mc;
            _personilTrxService = personilTrxService;
        }

        public class CountData
        {
            public int TrxId { get; set; }
            public decimal TotalCount { get; set; }
        }

        // GET: api/<ValuesController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(string port, int year, int isUpdate, CancellationToken cancellationToken)
        {
            int lastPersonilId = 0;
            decimal totalDetailExisting = 0;
            List<CountData> countTotalDetailExisting = new List<CountData>();

            List<PersonilTrxModel> result = new List<PersonilTrxModel>();
            List<RekomendasiPersonil> rekomenPersonilList = await _dbOMNI.RekomendasiPersonil.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port && b.Year == year && b.RekomendasiType.Id == 1).Include(b => b.Personil).Include(b => b.RekomendasiType).ToListAsync(cancellationToken);

            var list = await _dbOMNI.PersonilTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port && b.Year == year)
                .Include(b => b.Personil)
                .OrderBy(b => b.Personil.Id).ToListAsync(cancellationToken);

            if (list.Count() > 0)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    //COUNT TOTAL DETAIL EXISTING 
                    if (i == 0)
                    {
                        if (i == (list.Count() - 1))
                        {
                            CountData tempTotalDetailExisting = new CountData();
                            tempTotalDetailExisting.TrxId = list[i].Personil.Id;
                            tempTotalDetailExisting.TotalCount = 1;
                            countTotalDetailExisting.Add(tempTotalDetailExisting);
                        }
                        else
                        {
                            lastPersonilId = list[i].Personil.Id;
                            totalDetailExisting += 1;
                        }
                    }
                    else
                    {
                        if (lastPersonilId == list[i].Personil.Id)
                        {
                            if (i == (list.Count() - 1))
                            {
                                totalDetailExisting += 1;
                                CountData tempTotalDetailExisting1 = new CountData();
                                tempTotalDetailExisting1.TrxId = lastPersonilId;
                                tempTotalDetailExisting1.TotalCount = totalDetailExisting;
                                countTotalDetailExisting.Add(tempTotalDetailExisting1);
                            }
                            else
                            {
                                totalDetailExisting += 1;
                            }
                        }
                        else
                        {
                            CountData tempTotalDetailExisting = new CountData();
                            tempTotalDetailExisting.TrxId = lastPersonilId;
                            tempTotalDetailExisting.TotalCount = totalDetailExisting;
                            countTotalDetailExisting.Add(tempTotalDetailExisting);

                            totalDetailExisting = 0;

                            if (i == (list.Count() - 1))
                            {
                                lastPersonilId = list[i].Personil.Id;
                                totalDetailExisting = 1;

                                CountData tempTotalDetailExisting_2 = new CountData();
                                tempTotalDetailExisting_2.TrxId = lastPersonilId;
                                tempTotalDetailExisting_2.TotalCount = 1;
                                countTotalDetailExisting.Add(tempTotalDetailExisting_2);
                            }
                            else
                            {
                                lastPersonilId = list[i].Personil.Id;
                                totalDetailExisting += 1;
                            }
                        }
                    }

                    PersonilTrxModel temp = new PersonilTrxModel();
                    
                    var findRekomendasiHubla = rekomenPersonilList.Find(b => b.Personil.Id == list[i].Personil.Id);

                    if (findRekomendasiHubla != null)
                    {
                        temp.RekomendasiHubla = findRekomendasiHubla.Value;
                    }

                    temp.Id = list[i].Id;
                    temp.Personil = list[i].Personil != null ? list[i].Personil.Name : "-";
                    temp.PersonilId = list[i].Personil != null ? list[i].Personil.Id : 0;
                    temp.Satuan = list[i].Personil != null ? list[i].Personil.Satuan : "-";
                    temp.Name = list[i].Name;
                    //temp.TotalDetailExisting = list[i].TotalDetailExisting;
                    temp.TanggalPelatihan = list[i].TanggalPelatihan != null ? list[i].TanggalPelatihan.ToString("dd/MM/yyyy") : "-";

                    int diffDays = 0;

                    if (temp.Personil.Contains("IMO Level - 1"))
                    {
                        var tglExpired = list[i].TanggalPelatihan.AddYears(5);
                        temp.TanggalExpired = tglExpired.ToString("dd/MM/yyyy");

                        if (tglExpired != null)
                        {
                            if (!tglExpired.ToString("d/M/yyyy").Contains("1/1/0001"))
                            {
                                diffDays = (tglExpired - list[i].TanggalPelatihan).Days;
                            }
                        }
                    } else
                    {
                        temp.TanggalExpired = "-";
                    }

                    temp.SisaMasaBerlaku = diffDays;
                    temp.PersentasePersonil = list[i].PersentasePersonil;
                    temp.Port = list[i].Port;
                    temp.CreateDate = list[i].CreatedAt.ToString("dd MMM yyyy");
                    temp.CreatedBy = list[i].CreatedBy;
                    result.Add(temp);
                }

                if (countTotalDetailExisting.Count() > 0)
                {
                    for (int j = 0; j < result.Count(); j++)
                    {
                        var index = j;
                        var find = countTotalDetailExisting.Find(b => b.TrxId == result[j].PersonilId);
                        if (find != null)
                        {
                            result[j].TotalDetailExisting = find.TotalCount;
                            result[j].SelisihHubla = find.TotalCount - result[j].RekomendasiHubla;

                            if (result[j].SelisihHubla >= 0)
                            {
                                result[j].KesesuaianPM58 = "TERPENUHI";
                            }
                            else
                            {
                                result[j].KesesuaianPM58 = "KURANG";
                            }

                            if (result[j].RekomendasiHubla > 0)
                            {
                                result[j].PersentasePersonil = Math.Round(find.TotalCount / result[j].RekomendasiHubla * 100, 2);
                            } else
                            {
                                result[j].KesesuaianPM58 = "TIDAK TERPENUHI";
                            }
                            
                            if(result[j].PersentasePersonil > 100)
                            {
                                result[j].PersentasePersonil = 100;
                            }
                        }
                    }
                }

                PersonilTrxModel totalModel = new PersonilTrxModel();
                totalModel.Personil = "Total Persentase";
                result.Add(totalModel);
            }

            //if(isUpdate == 1)
            //{
            //    await UpdatePersonilTrx(result, cancellationToken);
            //}

            return Ok(result);
        }

        [HttpGet("GetRekomendasiPersonilByPersonilId")]
        public async Task<IActionResult> GetRekomendasiPersonilByPersonilId(string id, string port, int year, CancellationToken cancellationToken)
        {
            RekomendasiPersonil result = await _dbOMNI.RekomendasiPersonil.Where(b => b.IsDeleted == GeneralConstants.NO && b.Personil.Id == int.Parse(id) && b.Port == port && b.RekomendasiType.Id == 1 && b.Year == year).Include(b => b.Personil).Include(b => b.RekomendasiType).FirstOrDefaultAsync(cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            PersonilTrxModel result = new PersonilTrxModel();
            var data = await _dbOMNI.PersonilTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == id)
                .Include(b => b.Personil)
                .OrderBy(b => b.CreatedAt).FirstOrDefaultAsync(cancellationToken);
            if (data != null)
            {
                result.Id = data.Id;
                result.Personil = data.Personil != null ? data.Personil.Id.ToString() : "0";
                result.Port = data.Port;
                result.Name = data.Name;
                result.Satuan = data.Personil != null ? data.Personil.Satuan : "";
                result.Year = data.Year;
                //result.TotalDetailExisting = data.TotalDetailExisting;
                result.TanggalPelatihan = data.TanggalPelatihan != null ? data.TanggalPelatihan.ToString("MM/dd/yyyy") : null;
                result.TanggalExpired = data.TanggalPelatihan != null ? data.TanggalPelatihan.ToString("MM/dd/yyyy") : null;
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit([FromForm] PersonilTrxModel model, CancellationToken cancellationToken)
        {
            DateTime nullDate = new DateTime();
            PersonilTrx data = new PersonilTrx();

            if (model.Id > 0)
            {
                data.Id = model.Id;
                data = await _dbOMNI.PersonilTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == model.Id)
                 .Include(b => b.Personil)
                 .OrderBy(b => b.CreatedAt).FirstOrDefaultAsync(cancellationToken);

                data.Personil = await _dbOMNI.Personil.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == int.Parse(model.Personil)).FirstOrDefaultAsync(cancellationToken);
                data.Port = model.Port;
                data.Name = model.Name;
                data.TanggalPelatihan = !string.IsNullOrEmpty(model.TanggalPelatihan) ? DateTime.ParseExact(model.TanggalPelatihan, "MM/dd/yyyy", null) : nullDate;
                data.TanggalExpired = !string.IsNullOrEmpty(model.TanggalExpired) ? DateTime.ParseExact(model.TanggalExpired, "MM/dd/yyyy", null) : nullDate;
                data.UpdatedAt = DateTime.Now;
                data.Year = model.Year;
                data.UpdatedBy = model.UpdatedBy;
                _dbOMNI.PersonilTrx.Update(data);
                await _dbOMNI.SaveChangesAsync(cancellationToken);
                //Add History
                await AddHistory(data.Id, model, "UPDATE", cancellationToken);

            }
            else
            {
                data.Personil = await _dbOMNI.Personil.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == int.Parse(model.Personil)).FirstOrDefaultAsync(cancellationToken);
                data.Port = model.Port;
                data.Name = model.Name;
                data.Year = model.Year;
                //data.TotalDetailExisting = model.TotalDetailExisting;
                data.TanggalPelatihan = !string.IsNullOrEmpty(model.TanggalPelatihan) ? DateTime.ParseExact(model.TanggalPelatihan, "MM/dd/yyyy", null) : nullDate;
                data.TanggalExpired = !string.IsNullOrEmpty(model.TanggalExpired) ? DateTime.ParseExact(model.TanggalExpired, "MM/dd/yyyy", null) : nullDate;
                data.CreatedAt = DateTime.Now;
                data.CreatedBy = model.CreatedBy;
                await _dbOMNI.PersonilTrx.AddAsync(data, cancellationToken);
                await _dbOMNI.SaveChangesAsync(cancellationToken);

                //Add History
                await AddHistory(data.Id, model, "ADD", cancellationToken);
            }

            if(model.Files != null)
            {
                if (model.Files.Count() > 0)
                {
                    for (int i = 0; i < model.Files.Count(); i++)
                    {
                        await UploadFileWithReturn(path: $"OMNI/PERSONIL/{data.Id}/Files/", createBy: data.CreatedBy, trxId: data.Id, file: model.Files[i], Flag: GeneralConstants.OMNI_PERSONIL, isUpdate: model.Files != null, remark: null);
                    }

                }
            }

            //await GetAll(data.Port, data.Year, 1, cancellationToken);

            return Ok(new ReturnJson { });
        }

        //// DELETE api/<ValuesController>/5
        [HttpDelete("{id:int}")]
        public async Task<PersonilTrx> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            PersonilTrx data = await _dbOMNI.PersonilTrx.Where(b => b.Id == id).Include(b => b.Personil).FirstOrDefaultAsync(cancellationToken);
            data.IsDeleted = GeneralConstants.YES;
            data.UpdatedBy = "admin";
            data.UpdatedAt = DateTime.Now;
            _dbOMNI.PersonilTrx.Update(data);
            await _dbOMNI.SaveChangesAsync(cancellationToken);

            return data;
        }

        public async Task<string> AddHistory(int TrxId, PersonilTrxModel model, string TrxStatus, CancellationToken cancellationToken)
        {
            DateTime nullDate = new DateTime();

            HistoryPersonilTrx history = new HistoryPersonilTrx();
            history.PersonilTrxId = TrxId;
            history.Personil = await _dbOMNI.Personil.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == int.Parse(model.Personil)).FirstOrDefaultAsync(cancellationToken);
            history.Port = model.Port;
            history.Name = model.Name;
            history.TanggalPelatihan = !string.IsNullOrEmpty(model.TanggalPelatihan) ? DateTime.ParseExact(model.TanggalPelatihan, "MM/dd/yyyy", null) : nullDate;
            history.TanggalExpired = !string.IsNullOrEmpty(model.TanggalExpired) ? DateTime.ParseExact(model.TanggalExpired, "MM/dd/yyyy", null) : nullDate;
            history.Year = model.Year;
            history.CreatedBy = model.CreatedBy;
            history.CreatedAt = DateTime.Now;
            history.UpdatedBy = model.UpdatedBy;
            history.UpdatedAt = DateTime.Now;
            history.TrxStatus = TrxStatus;
            await _dbOMNI.HistoryPersonilTrx.AddAsync(history, cancellationToken);
            await _dbOMNI.SaveChangesAsync(cancellationToken);

            return "OK";
        }

        [HttpGet("DeleteAllPersonilTrx")]
        public async Task<IActionResult> DeleteAllPersonilTrx(string port, int year, CancellationToken cancellationToken)
        {
            var status = "FAILED";

            try
            {
                var list = await _dbOMNI.PersonilTrx.Where(b => b.Port == port && b.Year == year).ToListAsync(cancellationToken);
                if (list.Count() > 0)
                {
                    for (int i = 0; i < list.Count(); i++)
                    {
                        list[i].IsDeleted = GeneralConstants.YES;
                        list[i].UpdatedAt = DateTime.Now;
                        _dbOMNI.PersonilTrx.Update(list[i]);
                        await _dbOMNI.SaveChangesAsync(cancellationToken);
                    }
                }

                status = "SUCCESS";
            }
            catch (Exception ex)
            {

            }


            return Ok(status);
        }

        [HttpGet("GetCopyDataYearPersonilTrx")]
        public async Task<IActionResult> GetCopyDataYearPersonilTrx(string port, CancellationToken cancellationToken)
        {
            List<int> yearList = new List<int>();

            var list = await _dbOMNI.PersonilTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port).ToListAsync(cancellationToken);

            if (list.Count() > 0)
            {
                var group = list.GroupBy(b => b.Year).ToList();
                if (group.Count() > 0)
                {
                    for (int i = 0; i < group.Count(); i++)
                    {
                        yearList.Add(group[i].Key);
                    }
                }
            }
            return Ok(yearList);
        }

        [HttpGet("GetPersonilTrxByIdSP")]
        public IActionResult GetPersonilTrxByIdSP(int id)
        {
            var list = _personilTrxService.GetPersonilTrxById(id);

            return Ok(list);
        }

        [HttpGet("GetAllPersonilTrxSP")]
        public IActionResult GetAllPersonilTrxSP()
        {
            var list = _personilTrxService.GetAllPersonilTrx();

            return Ok(list);
        }

        //[HttpGet("CopyDataPersonilTrx")]
        //public async Task<IActionResult> CopyDataPersonilTrx(string port, int year, int targetYear, CancellationToken cancellationToken)
        //{
        //    List<int> ListId = new List<int>();

        //    DateTime nullDate = new DateTime();

        //    try
        //    {
        //        var list = await _dbOMNI.PersonilTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port && b.Year == year).Include(b => b.Personil).ToListAsync(cancellationToken);

        //        if (list.Count() > 0)
        //        {
        //            for (int i = 0; i < list.Count(); i++)
        //            {
        //                PersonilTrx data = new PersonilTrx();

        //                data.Personil = list[i].Personil;
        //                data.Port =  list[i].Port;
        //                data.Name =  list[i].Name;
        //                data.Year =  targetYear;
        //                data.TanggalPelatihan = list[i].TanggalPelatihan != null ? list[i].TanggalPelatihan : nullDate;
        //                data.TanggalExpired = list[i].TanggalExpired != null ? list[i].TanggalExpired : nullDate;
        //                data.CreatedAt = DateTime.Now;
        //                data.CreatedBy =  list[i].CreatedBy;
        //                await _dbOMNI.PersonilTrx.AddAsync(data, cancellationToken);
        //                await _dbOMNI.SaveChangesAsync(cancellationToken);

        //                HistoryPersonilTrx history = new HistoryPersonilTrx();
        //                history.PersonilTrxId = data.Id;
        //                history.Personil = data.Personil;
        //                history.Port = data.Port;
        //                history.Name = data.Name;
        //                history.TanggalPelatihan = data.TanggalPelatihan != null ? data.TanggalPelatihan : nullDate;
        //                history.TanggalExpired = data.TanggalExpired != null ? data.TanggalExpired : nullDate;
        //                history.Year = data.Year;
        //                history.CreatedBy = data.CreatedBy;
        //                history.CreatedAt = DateTime.Now;
        //                history.UpdatedBy = data.UpdatedBy;
        //                history.UpdatedAt = DateTime.Now;
        //                history.TrxStatus = "ADD";
        //                await _dbOMNI.HistoryPersonilTrx.AddAsync(history, cancellationToken);
        //                await _dbOMNI.SaveChangesAsync(cancellationToken);

        //                ListId.Add(data.Id);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }

        //    return Ok(ListId);
        //}
    }
}
