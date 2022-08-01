﻿using OMNI.Utilities.Base;
using OMNI.Web.Data.Dao;
using OMNI.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Trx.Interface
{
    public interface ILLPTrx
    {
        public Task<List<LLPTrxModel>> GetAllLLPTrx(string port);
        public Task<LLPTrxModel> GetById(int id);
        public Task<BaseJson<LLPTrxModel>> AddEdit(LLPTrxModel model);
        public Task<LLPTrx> Delete(int id);
    }
}
