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
    public class LLPHistoryStatusController : BaseController
    {
        public LLPHistoryStatusController(OMNIDbContext dbOMNI, MinioClient mc) : base(dbOMNI, mc)
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
            List<LLPHistoryStatusModel> result = new List<LLPHistoryStatusModel>();
           
            try
            {
                var list = await _dbOMNI.LLPHistoryStatus.Where(b => b.IsDeleted == GeneralConstants.NO && b.LLPTrx.Port == port && b.LLPTrx.Year == year)
                .Include(b => b.LLPTrx).Include(b => b.LLPTrx.SpesifikasiJenis).Include(b => b.LLPTrx.SpesifikasiJenis.PeralatanOSR)
                .Include(b => b.LLPTrx.SpesifikasiJenis.Jenis)
                .OrderByDescending(b => b.Id).ToListAsync(cancellationToken);
                if (list.Count() > 0)
                {
                    for (int i = 0; i < list.Count(); i++)
                    {
                        LLPHistoryStatusModel temp = new LLPHistoryStatusModel();
                        temp.Id = list[i].Id;
                        temp.LLPTrx = list[i].LLPTrx.Id.ToString();
                        temp.PeralatanOSR = list[i].LLPTrx.SpesifikasiJenis.PeralatanOSR.Name;
                        temp.Jenis = list[i].LLPTrx.SpesifikasiJenis.Jenis.Name;
                        temp.DetailExisting = list[i].LLPTrx.DetailExisting.ToString();
                        temp.Satuan = list[i].LLPTrx.SpesifikasiJenis.Jenis.Satuan;
                        temp.NoAsset = list[i].LLPTrx.QRCodeText;
                        temp.PortFrom = list[i].PortFrom;
                        temp.PortTo = list[i].PortTo;
                        temp.StartDate = list[i].StartDate.HasValue ? list[i].StartDate.Value.ToString("dd MMM yyyy") : "-";
                        temp.EndDate = list[i].EndDate.HasValue ? list[i].EndDate.Value.ToString("dd MMM yyyy") : "-";
                        temp.Remark = list[i].Remark;
                        temp.CreatedBy = list[i].CreatedBy;
                        temp.CreateDate = list[i].CreatedAt.ToString("dd MMM yyyy");
                        result.Add(temp);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return Ok(result);
        }

        
    }
}
