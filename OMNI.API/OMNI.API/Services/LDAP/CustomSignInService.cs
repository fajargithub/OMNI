using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using OMNI.API.Configurations;
using OMNI.API.Services.LDAP.Interfaces;
using OMNI.Migrations.Data.Dao;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.API.Services.LDAP
{
    public class CustomSignInService : SignInManager<ApplicationUser>, ICustomSignInService
    {
        private readonly IConfiguration _configuration;

        public CustomSignInService(UserManager<ApplicationUser> userManager,
                                   IHttpContextAccessor contextAccessor,
                                   IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
                                   IOptions<IdentityOptions> optionsAccessor,
                                   ILogger<SignInManager<ApplicationUser>> logger,
                                   IAuthenticationSchemeProvider schemes,
                                   IUserConfirmation<ApplicationUser> confirmation,
                                   IConfiguration configuration) : base(userManager,
                                                                        contextAccessor,
                                                                        claimsFactory,
                                                                        optionsAccessor,
                                                                        logger,
                                                                        schemes,
                                                                        confirmation)
        {
            _configuration = configuration;
        }

        //public CustomSignInService(
        //    UserManager<ApplicationUser> userManager,
        //    IHttpContextAccessor contextAccessor,
        //    IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
        //    IOptions<IdentityOptions> optionsAccessor,
        //    ILogger<SignInManager<ApplicationUser>> logger,
        //    IAuthenticationSchemeProvider schemes, IConfiguration configuration) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        //{
        //    _configuration = configuration;
        //}
        private IDictionary<string, string> ApiSettings() => _configuration.Get<AppSettings>().LDAPAuth;

        public Task<SignInResult> LdapSignIn(string userName, string password, bool isPersistent)
        {
            try
            {
                //const string LDAP_PATH = "LDAP://10.239.129.145:389";
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain, ApiSettings()["Domain"]))
                {
                    if (context.ValidateCredentials(userName, password))
                    {
                        ApplicationUser appuser = UserManager.Users.SingleOrDefault(b => b.Email == userName);
                        if (appuser == null) throw new System.Exception(message: $"AppUser with username {userName} Not Found!");
                        SignInAsync(appuser, isPersistent);
                        return Task.FromResult(SignInResult.Success);
                    }
                }
                Logger.LogWarning($"User with username {userName} failed login.");
                return Task.FromResult(SignInResult.Failed);
            }
            catch (System.Exception e)
            {
                Logger.LogWarning(e.InnerException?.Message ?? e.Message);
                return Task.FromResult(SignInResult.Failed);
            }
        }

        public Task<SignInResult> Authenticate(string userName, string password, bool isPersistent, bool autoSignIn = true)
        {
            using (var ldapConnection = new LdapConnection() { SecureSocketLayer = false })
            {

                try
                {
                    ldapConnection.Connect("10.239.129.145", 389);
                    ldapConnection.Bind(userName.Replace("@mitrakerja.pertamina.com", "@pertamina.com"), password);

                    ApplicationUser appuser = UserManager.Users.SingleOrDefault(b => b.Email == userName);
                    if (appuser == null) throw new System.Exception(message: $"AppUser with username {userName} Not Found!");
                    if (autoSignIn)
                        SignInAsync(appuser, isPersistent);
                    return Task.FromResult(SignInResult.Success);
                }
                catch (System.Exception e)
                {
                    Logger.LogWarning(e.InnerException?.Message ?? e.Message);
                    return Task.FromResult(SignInResult.Failed);
                }
            }
        }
    }
}
