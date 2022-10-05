using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace OMNI.Utilities.Base
{
    public class ReturnJson
    {
        public int Id { get; set; }
        public int Code { get; set; } = (int)HttpStatusCode.OK;
        public bool IsSuccess { get; set; } = true;

        public string ErrorMsg { get; set; }

        public dynamic Payload { get; set; }
    }
}
