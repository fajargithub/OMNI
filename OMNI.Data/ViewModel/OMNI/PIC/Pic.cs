using OMNI.Utilities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace OMNI.Data.ViewModel.OMNI.PIC
{
    public class Pic : BaseDao
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        public Area Area { get; set; }
    }
}
