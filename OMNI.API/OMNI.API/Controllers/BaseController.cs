using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minio;
using Minio.DataModel;
using Minio.Exceptions;
using OMNI.API.Model.OMNI;
using OMNI.Data.Data;
using OMNI.Domain.AppLogRepo;
using OMNI.Migrations.Data.Dao;
using OMNI.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace OMNI.API.Controllers
{
    public class BaseController : ControllerBase
    {
        //private readonly IAppLogRepo _appLog;
        public MinioClient _mc;
        public OMNIDbContext _dbOMNI;

        public BaseController(OMNIDbContext omniDb, MinioClient mc)
        {
            _dbOMNI = omniDb;
            _mc = mc;
        }
        protected string GetCurrentMethod([CallerMemberName] string methodName = "")
            => methodName.ToUpper();

        //protected async Task SaveAppLog(string methodName, string trxId, string status, CancellationToken cancellationToken, string remark = null, string errorMessage = null, string info = null)
        //    => await _appLog.SaveAppLog(
        //        controllerName: ControllerContext.RouteData.Values["controller"].ToString().ToUpper(),
        //        methodName: methodName,
        //        userName: User?.Identity?.Name ?? null,
        //        trxId: trxId,
        //        status: status,
        //        cancellationToken: cancellationToken,
        //        remark: remark,
        //        errorMessage: errorMessage,
        //        info: info);

        protected string GetUserName() => User.Claims.FirstOrDefault(b => b.Type == ClaimTypes.Name).Value;
        protected string GetEmail() => User.Claims.FirstOrDefault(b => b.Type == ClaimTypes.Email).Value;
        private byte[] CompressImage(IFormFile img)
        {

            byte[] result = null;

            using (MagickImage image = new MagickImage(img.OpenReadStream()))
            {
                //image.Format = image.Format; // Get or Set the format of the image.
                //image.Resize(40, 40); // fit the image into the requested width and height. 
                image.Quality = 50; // This is the Compression level.
                result = image.ToByteArray();
            }
            return result;
            //using (var stream = new FileStream(Path.Combine($"D:/TesUpload/", images.FileName), FileMode.Create))
            //{
            //    await stream.WriteAsync(result, 0, result.Length);
            //}
        }

        private byte[] CompressImage(byte[] img)
        {

            byte[] result = null;

            using (MagickImage image = new MagickImage(img))
            {
                //image.Format = image.Format; // Get or Set the format of the image.
                //image.Resize(40, 40); // fit the image into the requested width and height. 
                image.Quality = 50; // This is the Compression level.
                result = image.ToByteArray();
            }
            return result;
            //using (var stream = new FileStream(Path.Combine($"D:/TesUpload/", images.FileName), FileMode.Create))
            //{
            //    await stream.WriteAsync(result, 0, result.Length);
            //}
        }

        public string RemoveFileExtension(string param)
        {
            string fileName = param;
            int fileExtPos = fileName.LastIndexOf(".");
            if (fileExtPos >= 0)
                fileName = fileName.Substring(0, fileExtPos);

            return fileName;
        }

        [HttpGet("GetQRCodeFiles")]
        public async Task<IActionResult> GetQRCodeFiles(string trxId, string flag, CancellationToken cancellationToken)
        {
            List<FileUpload> fileList = await _dbOMNI.FileUpload.Where(b => b.IsDeleted == GeneralConstants.NO && b.TrxId == int.Parse(trxId) && b.Flag == flag).ToListAsync(cancellationToken);
            List<FilesModel> result = new List<FilesModel>();

            try
            {
                if (fileList != null)
                {
                    for (int i = 0; i < fileList.Count(); i++)
                    {
                        FilesModel temp = new FilesModel();
                        temp.Id = fileList[i].Id;
                        temp.FileName = RemoveFileExtension(fileList[i].FileName);
                        temp.FileType = fileList[i].ContentType;
                        temp.FilePath = fileList[i] != null ? (Path.Combine(fileList[i].FilePath + fileList[i].FileName)) : GeneralConstants.IMG_PLACEHOLDER;

                        //var request = WebRequest.Create("http://www.gravatar.com/avatar/6810d91caff032b202c50701dd3af745?d=identicon&r=PG");

                        //using (var response = request.GetResponse())
                        //using (var stream = response.GetResponseStream())
                        //{
                        //    var image = Bitmap.FromStream(stream);
                        //    System.IO.MemoryStream ms = new MemoryStream();
                        //    image.Save(ms, ImageFormat.Jpeg);
                        //    byte[] byteImage = ms.ToArray();
                        //    var SigBase64 = Convert.ToBase64String(byteImage);
                        //}

                        var base64Type = "";

                        if (temp.FileType == "image/jpeg")
                        {
                            base64Type = "data:image/jpeg;base64,";
                        }
                        else if (temp.FileType == "image/png")
                        {
                            base64Type = "data:image/png;base64,";
                        }


                        MemoryStream stream = new MemoryStream();
                        string filePath = fileList[i] != null ? (Path.Combine(fileList[i].FilePath + fileList[i].FileName)) : GeneralConstants.IMG_PLACEHOLDER;

                        bool isFileExists = false;
                        if (fileList[i] != null)
                        {
                            try
                            {
                                ObjectStat stat = await _mc.StatObjectAsync("uploaded", filePath);
                                isFileExists = true;
                            }
                            catch (ObjectNotFoundException)
                            {

                            }
                        }
                        if (isFileExists && !temp.FileType.Contains("application/pdf"))
                        {
                            await _mc.GetObjectAsync("uploaded", filePath, b => b.CopyTo(stream));

                            var image = Bitmap.FromStream(stream);
                            System.IO.MemoryStream ms = new MemoryStream();
                            image.Save(ms, ImageFormat.Jpeg);
                            byte[] byteImage = ms.ToArray();
                            temp.Base64 = base64Type + Convert.ToBase64String(byteImage);
                        }

                        temp.CreateDate = fileList[i].CreatedAt.ToString("dd/MM/yyyy");
                        temp.CreatedBy = fileList[i].CreatedBy;
                        result.Add(temp);
                    }
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            return Ok(result);
        }

        [HttpGet("GetAllFiles")]
        public async Task<IActionResult> GetAllFiles(string trxId, string flag, CancellationToken cancellationToken) {
            List<FileUpload> fileList = await _dbOMNI.FileUpload.Where(b => b.IsDeleted == GeneralConstants.NO && b.TrxId == int.Parse(trxId) && b.Flag == flag).ToListAsync(cancellationToken);
            List<FilesModel> result = new List<FilesModel>();
            if (fileList != null)
            {
                for (int i = 0; i < fileList.Count(); i++)
                {
                    FilesModel temp = new FilesModel();
                    temp.Id = fileList[i].Id;
                    temp.FileName = fileList[i].FileName;
                    temp.CreateDate = fileList[i].CreatedAt.ToString("dd/MM/yyyy");
                    temp.CreatedBy = fileList[i].CreatedBy;
                    result.Add(temp);
                }
            }

            return Ok(result);
        }

        private string CreatePath(string path) => Path.Combine(path, DateTime.UtcNow.ToString("yyyyMMdd/"));

        public async Task DeleteFile(FileUpload oldFile, string updateBy)
        {
            if (oldFile != null)
            {
                string fileFrom = Path.Combine(oldFile.FilePath, oldFile.FileName);
                bool isFileFromExist = false;
                try
                {
                    ObjectStat stat = await _mc.StatObjectAsync("uploaded", fileFrom);
                    isFileFromExist = true;
                }
                catch (ObjectNotFoundException)
                {

                }

                if (isFileFromExist)
                {
                    if (!await _mc.BucketExistsAsync("deleted"))
                        await _mc.MakeBucketAsync("deleted");
                    await _mc.CopyObjectAsync("uploaded", fileFrom, "deleted", destObjectName: fileFrom, copyConditions: new CopyConditions());
                    await _mc.RemoveObjectAsync("uploaded", fileFrom);
                }

                oldFile.IsDeleted = GeneralConstants.YES;
                oldFile.UpdatedBy = updateBy;
                oldFile.BucketName = "deleted";
                oldFile.UpdatedAt = DateTime.Now;

                _dbOMNI.Set<FileUpload>().Update(oldFile);
                await _dbOMNI.SaveChangesAsync();
            }
        }

        public async Task<string> GetFilePath(int id)
        {
            FileUpload file = await _dbOMNI.Set<FileUpload>().SingleOrDefaultAsync(b => b.Id == id);
            string result = Path.Combine(file.FilePath + file.FileName);
            try
            {
                ObjectStat stat = await _mc.StatObjectAsync("uploaded", result);
            }
            catch (ObjectNotFoundException)
            {
                throw new FileNotFoundException();
            }
            return result;
        }

        [HttpGet("GetFileData")]
        public async Task<FileUpload> GetFileData(string id, CancellationToken cancellationToken)
        {
            FileUpload data = await _dbOMNI.FileUpload.Where(b => b.Id == int.Parse(id)).FirstOrDefaultAsync(cancellationToken);

            return data;
        }

        [HttpDelete("DeleteFile")]
        public async Task<string> DeleteFile(string id, CancellationToken cancellationToken)
        {
            FileUpload data = await _dbOMNI.FileUpload.Where(b => b.Id == int.Parse(id)).FirstOrDefaultAsync(cancellationToken);
            data.IsDeleted = GeneralConstants.YES;
            data.UpdatedBy = "admin";
            data.UpdatedAt = DateTime.Now;
            _dbOMNI.FileUpload.Update(data);
            await _dbOMNI.SaveChangesAsync(cancellationToken);

            return "OK";
        }

        [HttpGet("ReadFile")]
        public async Task<FileStreamResult> ReadFile(string fileName, string flag, bool isFileNameFromDb = false)
        {
            try
            {
                //FileUpload file = await _dbOMNI.Set<FileUpload>().SingleOrDefaultAsync(b => b.FileName.Contains(file) && b.IsDeleted == GeneralConstants.NO && b.Flag == flag);
                FileUpload file = await _dbOMNI.FileUpload.Where(b => b.IsDeleted == GeneralConstants.NO && b.FileName.ToLower().Contains(fileName.ToLower()) && b.Flag == flag).FirstOrDefaultAsync();
                MemoryStream stream = new MemoryStream();
                string filePath = file != null ? (Path.Combine(file.FilePath + file.FileName)) : GeneralConstants.IMG_PLACEHOLDER;

                bool isFileExists = false;
                if (file != null)
                {
                    try
                    {
                        ObjectStat stat = await _mc.StatObjectAsync("uploaded", filePath);
                        isFileExists = true;
                    }
                    catch (ObjectNotFoundException)
                    {

                    }
                }
                if (isFileExists)
                    using (MemoryStream ms = new MemoryStream())
                        await _mc.GetObjectAsync("uploaded", filePath, b => b.CopyTo(stream));

                else
                {
                    byte[] byteArray = await System.IO.File.ReadAllBytesAsync(GeneralConstants.IMG_PLACEHOLDER);
                    stream.Write(byteArray, 0, byteArray.Length);
                }

                stream.Position = 0;
                FileStreamResult result = new FileStreamResult(stream, isFileExists && file != null ? file.ContentType : @"image/png");
                if (isFileNameFromDb)
                {
                    result.FileDownloadName = file?.FileName ?? "404";
                }
                else if (!string.IsNullOrWhiteSpace(fileName))
                {
                    result.FileDownloadName = fileName ?? "404";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UploadFile(string path, string createBy, int trxId, IFormFile file, bool isUpdate = false, string Flag = null, string remark = null, bool autoRename = false, bool isCompress = false)
        {
            if (file != null)
            {
                path = path.EndsWith("/") ? path : path + "/";
                string filePathView = CreatePath(path);
                string fileName = file.FileName.ToLower();

                byte[] compresed = null;
                if (isCompress)
                    compresed = CompressImage(file);

                FileUpload uploadFile = new FileUpload
                {
                    FilePath = filePathView,
                    FileName = fileName,
                    ContentType = file.ContentType,
                    Length = isCompress ? compresed.Length : file.Length,
                    TrxId = trxId,
                    CreatedBy = createBy,
                    Flag = Flag,
                    Remark = remark
                };

                // Delete File First while Update Action
                if (isUpdate)
                {
                    FileUpload oldFile = await _dbOMNI.Set<FileUpload>().SingleOrDefaultAsync(
                        b =>
                        b.TrxId == trxId &&
                        b.Flag == Flag &&
                        b.Remark == remark &&
                        b.IsDeleted == GeneralConstants.YES);
                    await DeleteFile(oldFile, createBy);
                }

                await _dbOMNI.Set<FileUpload>().AddAsync(uploadFile);
                await _dbOMNI.SaveChangesAsync();

                if (autoRename)
                {
                    uploadFile.FileName = $"{uploadFile.Id}-{uploadFile.FileName}";
                    fileName = uploadFile.FileName;
                    _dbOMNI.Set<FileUpload>().Update(uploadFile);
                }

                if (!await _mc.BucketExistsAsync("uploaded"))
                    await _mc.MakeBucketAsync("uploaded");


                using MemoryStream ms = new MemoryStream();
                if (isCompress)
                    await ms.WriteAsync(compresed, 0, compresed.Length);
                else
                    await file.CopyToAsync(ms);

                ms.Position = 0;
                await _mc.PutObjectAsync(
                    bucketName: uploadFile.BucketName,
                    objectName: Path.Combine(filePathView, uploadFile.FileName),
                    data: ms,
                    size: ms.Length,
                    contentType: file.ContentType);
                await _dbOMNI.SaveChangesAsync();
            }
        }

        public async Task<FileUpload> UploadFileWithReturn(string path, string createBy, int trxId, IFormFile file, bool isUpdate = false, string Flag = null, bool autoRename = false, string remark = null, bool isCompress = false)
        {
            if (file != null)
            {
                path = path.EndsWith("/") ? path : path + "/";
                string filePathView = CreatePath(path);
                string fileName = file.FileName.ToLower();

                byte[] compresed = null;
                if (isCompress)
                    compresed = CompressImage(file);

                FileUpload uploadFile = new FileUpload
                {
                    BucketName = "uploaded",
                    FilePath = filePathView,
                    FileName = fileName,
                    ContentType = file.ContentType,
                    Length = isCompress ? compresed.Length : file.Length,
                    TrxId = trxId,
                    CreatedBy = createBy,
                    Flag = Flag,
                    Remark = remark
                };
                // Delete File First while Update Action
                if (isUpdate)
                {
                    FileUpload oldFile = await _dbOMNI.Set<FileUpload>().SingleOrDefaultAsync(
                        b =>
                        b.TrxId == trxId &&
                        b.Flag == Flag &&
                        b.Remark == remark &&
                        b.IsDeleted == GeneralConstants.YES);
                    await DeleteFile(oldFile, createBy);
                }

                await _dbOMNI.Set<FileUpload>().AddAsync(uploadFile);
                await _dbOMNI.SaveChangesAsync();

                if (autoRename)
                {
                    uploadFile.FileName = $"{uploadFile.Id}-{uploadFile.FileName}";
                    fileName = uploadFile.FileName;
                    _dbOMNI.Set<FileUpload>().Update(uploadFile);
                }


                if (!await _mc.BucketExistsAsync("uploaded"))
                    await _mc.MakeBucketAsync("uploaded");


                using (var ms = new MemoryStream())
                {
                    if (isCompress)
                        await ms.WriteAsync(compresed, 0, compresed.Length);
                    else
                        await file.CopyToAsync(ms);

                    ms.Position = 0;
                    await _mc.PutObjectAsync(bucketName: "uploaded", objectName: Path.Combine(filePathView, uploadFile.FileName), data: ms, size: ms.Length, contentType: file.ContentType);
                }
                await _dbOMNI.SaveChangesAsync();
                return uploadFile;
            }
            return null;
        }

        public async Task<string> UpdateLLPTrx(List<LLPTrxModel> paramList, CancellationToken cancellationToken)
        {
            for(int i=0; i < (paramList.Count()-1); i++)
            {
                var data = await _dbOMNI.LLPTrx.Where(b => b.IsDeleted == GeneralConstants.NO && b.Id == paramList[i].Id)
                 .Include(b => b.SpesifikasiJenis)
                 .Include(b => b.SpesifikasiJenis.PeralatanOSR)
                 .Include(b => b.SpesifikasiJenis.Jenis)
                 .OrderBy(b => b.CreatedAt).FirstOrDefaultAsync(cancellationToken);

                data.TotalExistingJenis = paramList[i].TotalExistingJenis;
                data.TotalKebutuhanHubla = paramList[i].TotalKebutuhanHubla;
                data.SelisihHubla = paramList[i].SelisihHubla;
                data.TotalKebutuhanOSCP = paramList[i].TotalKebutuhanOSCP;
                data.SelisihOSCP = paramList[i].SelisihOSCP;
                data.KesesuaianOSCP = paramList[i].KesesuaianOSCP;
                data.PersentaseOSCP = paramList[i].PersentaseOSCP;
                data.KesesuaianMP58 = paramList[i].KesesuaianPM58;
                data.TotalExistingKeseluruhan = paramList[i].TotalExistingKeseluruhan;
                data.PersentaseHubla = paramList[i].PersentaseHubla;
                _dbOMNI.LLPTrx.Update(data);
                await _dbOMNI.SaveChangesAsync(cancellationToken);
            }
            
            return "OK";
        }
    }
}
