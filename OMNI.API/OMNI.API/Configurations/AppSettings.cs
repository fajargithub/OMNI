using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMNI.API.Configurations
{
    public class AppSettings : BaseAppSettings
    {
        public IDictionary<string, string> Secret { get; set; }
    }
}
