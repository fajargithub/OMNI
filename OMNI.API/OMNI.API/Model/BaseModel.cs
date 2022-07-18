using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.API.Model
{
    public class BaseModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Activity { get; set; }
        public string StatusMessage { get; set; }

        public string File { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Length { get; set; }
        public string Flag { get; set; }
        public string Remark { get; set; }
        public string FileType { get; set; }
        public string ContentType { get; set; }
        public string TrxId { get; set; }
        public string UploadDate { get; set; }

        // UserName for API tracking , dont delete it :)
        public string UserName { get; set; }
        public string AspUserId { get; set; }
    }
}
