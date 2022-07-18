using OMNI.Data.Data;
using OMNI.Data.Data.Dao;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OMNI.Domain.AppLogRepo.Impl
{
    public class AppLogRepoImpl : IAppLogRepo
    {
        private readonly OMNIDbContext _db;

        public AppLogRepoImpl(OMNIDbContext db)
        {
            _db = db;
        }

        public async Task SaveAppLog(string controllerName, string methodName, string userName, string trxId, string status, CancellationToken cancellationToken, string remark = null, string errorMessage = null, string info = null)
        {
            await _db.OMNIActivityLog.AddAsync(new OMNIActivityLog
            {
                //Controller = ControllerContext.RouteData.Values["controller"].ToString().ToUpper(),
                Controller = controllerName,
                Method = methodName,
                TrxId = trxId,
                UserName = userName,
                Status = status,
                ErrorMessage = errorMessage,
                Remark = remark,
                Info = info
            }, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
