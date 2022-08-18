using OMNI.Utilities.Base;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models;
using OMNI.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Trx.Interface
{
    public interface ILatihanTrx
    {
        public Task<string> GetContentType(int id);
        public Task<Stream> ReadFile(int id, string flag);
        public Task<List<FilesModel>> GetAllFiles(int trxId);
        public Task<string> DeleteFile(int id);
        public Task<List<LatihanTrxModel>> GetAllLatihanTrx(string port);
        public Task<LatihanTrxModel> GetById(int id);
        public Task<RekomendasiLatihan> GetRekomendasiLatihanByLatihanId(int id, string port);
        public Task<BaseJson<LatihanTrxModel>> AddEdit(LatihanTrxModel model);
        public Task<LatihanTrx> Delete(int id);
    }
}
