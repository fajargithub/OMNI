using System;
using System.Collections.Generic;
using System.Text;

namespace OMNI.Data.ViewModel.OMNI.Account
{
    public class Account
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string RegionAreaId { get; set; }
        public List<string> Roles { get; set; }
    }
}
