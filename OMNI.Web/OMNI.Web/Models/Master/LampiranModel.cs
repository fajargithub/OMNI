using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Models.Master
{
    public class LampiranModel : BaseModel
    {
        public string Name { get; set; }
        public string Port { get; set; }
        public string LampiranType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
