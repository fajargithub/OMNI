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

        #region HISTORY LLPTRX
        [HttpGet("GetAllHistoryLLPTrx")]
        public async Task<IActionResult> GetAllHistoryLLPTrx(int trxId, string port, int year, CancellationToken cancellationToken)
        {
            List<HistoryLLPTrxModel> result = new List<HistoryLLPTrxModel>();
            //List<RekomendasiJenis> rekomenJenisList = await _dbOMNI.RekomendasiJenis.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port).Include(b => b.SpesifikasiJenis).Include(b => b.SpesifikasiJenis.PeralatanOSR).Include(b => b.RekomendasiType).ToListAsync(cancellationToken);

            try
            {
                var list = await _dbOMNI.HistoryLLPTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port && b.Year == year && b.LLPTrxId == trxId)
               .Include(b => b.SpesifikasiJenis)
               .Include(b => b.SpesifikasiJenis.PeralatanOSR)
               .Include(b => b.SpesifikasiJenis.Jenis)
               .OrderByDescending(b => b.Id).ToListAsync(cancellationToken);
                if (list.Count() > 0)
                {
                    for (int i = 0; i < list.Count(); i++)
                    {
                        HistoryLLPTrxModel temp = new HistoryLLPTrxModel();
                        temp.Id = list[i].Id;
                        temp.PeralatanOSR = list[i].SpesifikasiJenis != null ? list[i].SpesifikasiJenis.PeralatanOSR.Name : "-";
                        temp.Jenis = list[i].SpesifikasiJenis != null ? list[i].SpesifikasiJenis.Jenis.Name : "-";
                        temp.SatuanJenis = list[i].SpesifikasiJenis != null ? list[i].SpesifikasiJenis.Jenis.Satuan : "-";
                        temp.Port = list[i].Port;
                        temp.QRCode = list[i].QRCode;
                        temp.DetailExisting = list[i].DetailExisting;
                        temp.Kondisi = list[i].Kondisi;
                        temp.Brand = list[i].Brand;
                        temp.SerialNumber = list[i].SerialNumber;
                        temp.Remark = list[i].Remark;
                        temp.NoAsset = list[i].NoAsset;
                        temp.Status = list[i].Status;
                        temp.CreateDate = list[i].CreatedAt.ToString("dd MMM yyyy");
                        temp.CreatedBy = list[i].CreatedBy;
                        temp.CreateDate = list[i].UpdatedAt.ToString("dd MMM yyyy");
                        temp.CreatedBy = list[i].UpdatedBy;
                        temp.TrxStatus = list[i].TrxStatus;
                        result.Add(temp);
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
        [HttpGet("GetHistoryLLPTrx")]
        public async Task<IActionResult> GetHistoryLLPTrx(int id, CancellationToken cancellationToken)
        {
            HistoryLLPTrxModel result = new HistoryLLPTrxModel();
            var data = await _dbOMNI.HistoryLLPTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == id)
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
            }

            return Ok(result);
        }
        #endregion

        #region HISTORY PERSONIL TRX
        [HttpGet("GetAllHistoryPersonilTrx")]
        public async Task<IActionResult> GetAllHistoryPersonilTrx(int trxId, string port, int year, CancellationToken cancellationToken)
        {
            List<HistoryPersonilTrxModel> result = new List<HistoryPersonilTrxModel>();
            List<RekomendasiPersonil> rekomenPersonilList = await _dbOMNI.RekomendasiPersonil.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port && b.RekomendasiType.Id == 1).Include(b => b.RekomendasiType).ToListAsync(cancellationToken);

            var list = await _dbOMNI.HistoryPersonilTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Port == port && b.Year == year && b.PersonilTrxId == trxId)
                .Include(b => b.Personil)
                .OrderByDescending(b => b.Personil.Id).ToListAsync(cancellationToken);

            if (list.Count() > 0)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    HistoryPersonilTrxModel temp = new HistoryPersonilTrxModel();
                    int diffDays = 0;

                    diffDays = (list[i].TanggalExpired - list[i].TanggalPelatihan).Days;

                    temp.Id = list[i].Id;
                    temp.Personil = list[i].Personil != null ? list[i].Personil.Name : "-";
                    temp.Satuan = list[i].Personil != null ? list[i].Personil.Satuan : "-";
                    temp.Name = list[i].Name;
                    temp.TanggalPelatihan = list[i].TanggalPelatihan != null ? list[i].TanggalPelatihan.ToString("dd/MM/yyyy") : "-";
                    temp.TanggalExpired = list[i].TanggalExpired != null ? list[i].TanggalExpired.ToString("dd/MM/yyyy") : "-";
                    temp.SisaMasaBerlaku = diffDays;
                    temp.PersentasePersonil = list[i].PersentasePersonil;
                    temp.Port = list[i].Port;
                    temp.CreateDate = list[i].CreatedAt.ToString("dd MMM yyyy");
                    temp.CreatedBy = list[i].CreatedBy;
                    temp.UpdateDate = list[i].UpdatedAt.ToString("dd MMM yyyy");
                    temp.UpdatedBy = list[i].UpdatedBy;
                    temp.TrxStatus = list[i].TrxStatus;
                    result.Add(temp);
                }
            }

            return Ok(result);
        }

        #endregion
    }
}
