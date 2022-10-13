using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Models.Master
{
    public class GuestLocationModel : BaseModel
    {
        public int UserId { get; set; }
        public string GuestCategory { get; set; }
        public string Port { get; set; }
    }
}
