using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Models.Master
{
    public class GuestRegionModel : BaseModel
    {
        public int UserId { get; set; }
        public string GuestCategory { get; set; }
        public string Email { get; set; }
        public string Region { get; set; }
    }
}
