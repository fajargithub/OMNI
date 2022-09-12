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
    public interface IPersonilTrx
    {
        public Task<string> GetContentType(int id);
        public Task<Stream> ReadFile(int id, string flag);
        public Task<List<FilesModel>> GetAllFiles(int trxId);
        public Task<string> DeleteFile(int id);
        public Task<List<PersonilTrxModel>> GetAllPersonilTrx(string port, int year);
        public Task<PersonilTrxModel> GetById(int id);
        public Task<RekomendasiPersonil> GetRekomendasiPersonilByPersonilId(int id, string port, int year);
        public Task<BaseJson<PersonilTrxModel>> AddEdit(PersonilTrxModel model);
        public Task<PersonilTrx> Delete(int id);
    }
}
