using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMNI.Web.Configurations
{
    [UsedImplicitly]
    public class AppSettings
    {
        public IDictionary<string, string> ConnectionStrings { get; set; }
        public IDictionary<string, string> DataBase { get; set; }
        public bool IsProduction { get; set; }
        public IDictionary<string, string> Secret { get; set; }
        public IDictionary<string, string> UrlAPI { get; set; }
        public IDictionary<string, string> Default { get; set; }
    }
}
