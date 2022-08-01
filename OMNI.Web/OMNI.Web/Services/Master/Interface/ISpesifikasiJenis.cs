using OMNI.Utilities.Base;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Master.Interface
{
    public interface ISpesifikasiJenis 
    {
        public Task<List<SpesifikasiJenisModel>> GetAll();

        public Task<List<SpesifikasiJenisModel>> GetAllSpesifikasiJenisByPeralatanOSR(int id);
        public Task<SpesifikasiJenisModel> GetById(int id);
        public Task<BaseJson<SpesifikasiJenisModel>> AddEdit(SpesifikasiJenisModel model);
        public Task<SpesifikasiJenis> Delete(int id);
    }
}
