using Microsoft.AspNetCore.Mvc;
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
    public interface ILLPTrx
    {
        public Task<string> GetContentType(int id);
        public Task<Stream> ReadFile(int id, string flag);
        public Task<List<FilesModel>> GetAllFiles(int trxId);
        public Task<string> DeleteFile(int id);
        public Task<List<LLPTrxModel>> GetAllLLPTrx(string port);
        public Task<LLPTrxModel> GetById(int id);
        public Task<BaseJson<LLPTrxModel>> AddEdit(LLPTrxModel model);
        public Task<LLPTrx> Delete(int id);

    }
}
