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
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public List<string> Roles { get; set; }
    }
}
