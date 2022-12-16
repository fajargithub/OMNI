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
        private readonly CorePTKContext _corePTKDb;
        public LLPHistoryStatusController(CorePTKContext corePTKDb, OMNIDbContext dbOMNI, MinioClient mc) : base(dbOMNI, mc)
        {
            _corePTKDb = corePTKDb;
            _dbOMNI = dbOMNI;
            _mc = mc;
        }

        public class CountData
        {
            public int TrxId { get; set; }
            public decimal TotalCount { get; set; }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            LLPHistoryStatusModel result = new LLPHistoryStatusModel();
            var temp = await _dbOMNI.LLPHistoryStatus.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == id).FirstOrDefaultAsync(cancellationToken);
            if (temp != null)
            {
                result.Id = temp.Id;
                result.Remark = temp.Remark;
            }
            return Ok(result);
        }

        [HttpGet("{GetLastHistoryByTrxId}")]
        public async Task<IActionResult> GetLastHistoryByTrxId(int id, CancellationToken cancellationToken)
        {
            LLPHistoryStatusModel result = new LLPHistoryStatusModel();
            
            var temp = await _dbOMNI.LLPHistoryStatus.Where(b => b.IsDeleted == GeneralConstants.NO && b.LLPTrx.Id == id).Include(b => b.LLPTrx).OrderByDescending(b => b.CreatedAt).FirstOrDefaultAsync(cancellationToken);
            if (temp != null)
            {
                result.Id = temp.Id;
                result.Status = temp.Status;
                result.StartDate = temp.StartDate.HasValue ? temp.StartDate.Value.ToString("dd MMM yyyy") : "-";
                result.EndDate = temp.EndDate.HasValue ? temp.EndDate.Value.ToString("dd MMM yyyy") : "-";
                result.PortFrom = temp.PortFrom;
                result.PortTo = temp.PortTo;
                result.Remark = temp.Remark;
            }
            return Ok(result);
        }

        // GET: api/<ValuesController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(string port, int year, CancellationToken cancellationToken)
        {
            List<LLPHistoryStatusModel> result = new List<LLPHistoryStatusModel>();
            var portList = await _corePTKDb.Port.Where(b => b.IsDeleted == GeneralConstants.NO && b.PAreaSub.Id > 0).Include(b => b.PAreaSub).OrderBy(b => b.Id).ToListAsync(cancellationToken);
            try
            {
                var list = await _dbOMNI.LLPHistoryStatus.Where(b => b.IsDeleted == GeneralConstants.NO && b.LLPTrx.IsDeleted == GeneralConstants.NO && b.PortFrom == port && b.LLPTrx.Year == year)
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
                        temp.Status = list[i].Status;
                        temp.PortFrom = !string.IsNullOrEmpty(list[i].PortFrom) ? portList.Find(b => b.Id == int.Parse(list[i].PortFrom)).Name : "-";
                        temp.PortTo = !string.IsNullOrEmpty(list[i].PortTo) ? portList.Find(b => b.Id == int.Parse(list[i].PortTo)).Name : "-";
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
