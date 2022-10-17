using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Models
{
    public class FilesModel : BaseModel
    {
        public string UploadedBy { get; set; }
        public string Base64 { get; set; }
    }
}
