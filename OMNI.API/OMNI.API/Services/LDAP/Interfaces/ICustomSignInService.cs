using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.API.Services.LDAP.Interfaces
{
    public interface ICustomSignInService
    {
        Task<SignInResult> LdapSignIn(string userName, string password, bool isPersistent);
        Task<SignInResult> Authenticate(string userName, string password, bool isPersistent, bool autoSignIn = true);
    }
}
