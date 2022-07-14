using OMNI.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace OMNI.Utilities.Base
{
    public class ReturnJsonModel
    {
        public string Status { get; set; } = GeneralConstants.SUCCESS;

        public string ErrorMsg { get; set; }

        public dynamic Response { get; set; }
    }
}
