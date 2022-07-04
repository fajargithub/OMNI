using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMNI.Utilities.Base
{
    public class BaseAppSettings
    {
        public IDictionary<string, string> ConnectionStrings { get; set; }
        public IDictionary<string, string> DataBase { get; set; }
        public bool IsProduction { get; set; }
    }
}
