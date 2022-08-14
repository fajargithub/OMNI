using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json.Serialization;

namespace OMNI.Web.Models
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

        [JsonIgnore]
        public IFormFileCollection Files { get; set; }

        public string File { get; set; }
        public string FileKTP { get; set; }
        public string FileBPJS { get; set; }
        public string FileNPWP { get; set; }
        public string FileMaps { get; set; }
        public string FileKK { get; set; }
        public string FileName { get; set; }
        public string DivisionName { get; set; }
        public int IsPublic { get; set; }
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
