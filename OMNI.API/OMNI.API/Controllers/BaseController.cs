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
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace OMNI.API.Controllers
{
    public class BaseController : ControllerBase
    {
        //private readonly IAppLogRepo _appLog;
        private readonly MinioClient _mc;
        private readonly OMNIDbContext _omniDb;

        public BaseController(OMNIDbContext omniDb, MinioClient mc)
        {
            //_appLog = appLog;
            _omniDb = omniDb;
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

                _omniDb.Set<FileUpload>().Update(oldFile);
                await _omniDb.SaveChangesAsync();
            }
        }

        public async Task<string> GetFilePath(int id)
        {
            FileUpload file = await _omniDb.Set<FileUpload>().SingleOrDefaultAsync(b => b.Id == id);
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

        public async Task<string> GetFilePath(FileUpload file)
        {
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

        public async Task<FileStreamResult> ReadFile(int id, bool isFileNameFromDb = false, string fileName = null)
        {
            try
            {
                FileUpload file = await _omniDb.Set<FileUpload>().SingleOrDefaultAsync(b => b.Id == id && b.IsDeleted == GeneralConstants.NO);
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

        public async Task<FileStreamResult> ReadFile(FileUpload file, bool isFileNameFromDb = false, string fileName = null)
        {
            try
            {
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
                    stream.Position = 0;
                }
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

        public async Task<ReadFileModel> ReadFileByte(int id)
        {
            FileUpload file = await _omniDb.Set<FileUpload>().SingleOrDefaultAsync(b => b.Id == id && b.IsDeleted == GeneralConstants.NO);
            return await ReadFileByte(file);
        }

        public async Task<ReadFileModel> ReadFileByte(FileUpload file)
        {
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

            byte[] byteArray = null;
            if (isFileExists)
                using (MemoryStream ms = new MemoryStream())
                {
                    await _mc.GetObjectAsync("uploaded", filePath, b => b.CopyTo(ms));
                    byteArray = ms.ToArray();
                }
            else
                byteArray = await System.IO.File.ReadAllBytesAsync(GeneralConstants.IMG_PLACEHOLDER);

            return new ReadFileModel
            {
                ContentType = isFileExists ? file.ContentType : @"image/png",
                FileContents = byteArray,
                FileName = isFileExists ? file.FileName : "404.jpg"
            };
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
                    FileUpload oldFile = await _omniDb.Set<FileUpload>().SingleOrDefaultAsync(
                        b =>
                        b.TrxId == trxId &&
                        b.Flag == Flag &&
                        b.Remark == remark &&
                        b.IsDeleted == GeneralConstants.YES);
                    await DeleteFile(oldFile, createBy);
                }

                await _omniDb.Set<FileUpload>().AddAsync(uploadFile);
                await _omniDb.SaveChangesAsync();

                if (autoRename)
                {
                    uploadFile.FileName = $"{uploadFile.Id}-{uploadFile.FileName}";
                    fileName = uploadFile.FileName;
                    _omniDb.Set<FileUpload>().Update(uploadFile);
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
                await _omniDb.SaveChangesAsync();
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
                    FileUpload oldFile = await _omniDb.Set<FileUpload>().SingleOrDefaultAsync(
                        b =>
                        b.TrxId == trxId &&
                        b.Flag == Flag &&
                        b.Remark == remark &&
                        b.IsDeleted == GeneralConstants.YES);
                    await DeleteFile(oldFile, createBy);
                }

                await _omniDb.Set<FileUpload>().AddAsync(uploadFile);
                await _omniDb.SaveChangesAsync();

                if (autoRename)
                {
                    uploadFile.FileName = $"{uploadFile.Id}-{uploadFile.FileName}";
                    fileName = uploadFile.FileName;
                    _omniDb.Set<FileUpload>().Update(uploadFile);
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
                await _omniDb.SaveChangesAsync();
                return uploadFile;
            }
            return null;
        }

        public async Task<FileUpload> UploadFileWithReturn(string path, string createBy, int trxId, ReadFileModel file, bool isUpdate = false, string Flag = null, bool autoRename = false, string remark = null, bool isCompress = false)
        {
            if (file != null)
            {
                path = path.EndsWith("/") ? path : path + "/";
                string filePathView = CreatePath(path);
                string fileName = file.FileName.ToLower();
                FileUpload uploadFile = new FileUpload
                {
                    BucketName = "uploaded",
                    FilePath = filePathView,
                    FileName = fileName,
                    ContentType = file.ContentType,
                    Length = file.FileContents.Length,
                    TrxId = trxId,
                    CreatedBy = createBy,
                    Flag = Flag,
                    Remark = remark
                };
                // Delete File First while Update Action
                if (isUpdate)
                {
                    FileUpload oldFile = await _omniDb.Set<FileUpload>().SingleOrDefaultAsync(
                        b =>
                        b.TrxId == trxId &&
                        b.Flag == Flag &&
                        b.Remark == remark &&
                        b.IsDeleted == GeneralConstants.YES);
                    await DeleteFile(oldFile, createBy);
                }

                await _omniDb.Set<FileUpload>().AddAsync(uploadFile);
                await _omniDb.SaveChangesAsync();

                if (autoRename)
                {
                    uploadFile.FileName = $"{uploadFile.Id}-{uploadFile.FileName}";
                    fileName = uploadFile.FileName;
                    _omniDb.Set<FileUpload>().Update(uploadFile);
                }


                if (!await _mc.BucketExistsAsync("uploaded"))
                    await _mc.MakeBucketAsync("uploaded");


                using (var ms = new MemoryStream())
                {
                    if (isCompress)
                    {
                        byte[] compresed = CompressImage(file.FileContents);
                        await ms.WriteAsync(compresed, 0, compresed.Length);
                    }
                    else
                        await ms.WriteAsync(file.FileContents, 0, file.FileContents.Length);

                    ms.Position = 0;
                    await _mc.PutObjectAsync(bucketName: "uploaded", objectName: Path.Combine(filePathView, uploadFile.FileName), data: ms, size: ms.Length, contentType: file.ContentType);
                }
                await _omniDb.SaveChangesAsync();
                return uploadFile;
            }
            return null;
        }
    }
}
