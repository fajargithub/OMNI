using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.API.Model.OMNI
{
    public class LLPHistoryStatusModel : BaseModel
    {
        public string LLPTrx { get; set; }
        public string PeralatanOSR { get; set; }
        public string Satuan { get; set; }
        public string DetailExisting { get; set; }
        public string NoAsset { get; set; }
        public string Jenis { get; set; }
        public string PortFrom { get; set; }
        public string PortTo { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
