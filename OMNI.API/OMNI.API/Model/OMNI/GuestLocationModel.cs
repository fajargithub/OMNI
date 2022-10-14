using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.API.Model.OMNI
{
    public class GuestLocationModel : BaseModel
    {
        public int UserId { get; set; }
        public string GuestCategory { get; set; }
        public string StrPortList { get; set; }
        public List<string> PortList { get; set; }
        public string Email { get; set; }
    }
}
