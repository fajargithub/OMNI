using System;
using System.Collections.Generic;
using System.Text;

namespace OMNI.Data.ViewModel.OMNI.Account
{
    public class AccountCreate
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public int RegionAreaId { get; set; }
        public List<string> Roles { get; set; }
    }
}
