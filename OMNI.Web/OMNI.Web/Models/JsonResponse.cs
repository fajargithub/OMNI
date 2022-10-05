using OMNI.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Models
{
    public class JsonResponse
    {
        public int Id { get; set; }
        public string Status { get; set; } = GeneralConstants.SUCCESS;
        public string ErrorMsg { get; set; }
        public object Data { get; set; }
    }
}
