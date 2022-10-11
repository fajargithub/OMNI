using OMNI.Utilities.Base;
using OMNI.Web.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Services.Trx.Interface
{
    public interface ILogin
    {
        public Task<BaseJson<LoginModel>> SignIn(LoginModel model);
    }
}
