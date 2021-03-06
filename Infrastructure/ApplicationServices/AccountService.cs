﻿using ApplicationCore.ApplicationServices;
using ApplicationCore.DomainModel;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin.Security;
using System.Security.Claims;
using ApplicationCore.DomainServices;
using System.Web;
using Infrastructure.Helpers;

namespace Infrastructure.ApplicationServices
{
    public class AccountService : IAccountService
    {
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        IUserRepository userRepository;
        public AccountService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public bool CreateUser(string username, string password)
        {
            //Here must be test if user with the same name in DB already exists.

            var hash = SecurePasswordHasher.Hash(password);
            OperationResult result = userRepository.CreateUser(username, hash, roleId: 2);
            userRepository.Dispose();

            return result.Succeded;
        }

        public int GetCurrentUserId()
        {
            int currentUserId;
            var claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = claimsIdentity.Claims;
            var NameIdentifier = claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
            int.TryParse(NameIdentifier, out currentUserId);
            return currentUserId;
        }
        public void Logout()
        {
            AuthenticationManager.SignOut();
        }
        public void Login(int userId, string username, string role)
        {
            ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie",
                                                              ClaimsIdentity.DefaultNameClaimType,
                                                              ClaimsIdentity.DefaultRoleClaimType);

            claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));
            claim.AddClaim(new Claim(ClaimTypes.Name, username));
            if (role != null)
                claim.AddClaim(new Claim(
                                      ClaimsIdentity.DefaultRoleClaimType,
                                      role,
                                      ClaimValueTypes.String));

            claim.AddClaim(new Claim(
            "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                                  "OWIN Provider",
                                  ClaimValueTypes.String));

            AuthenticationManager.SignOut();
            AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = true
            }, claim);
        }
        public User GetUser(string username, string password)
        {
            User user = userRepository.GetUser(username);
            userRepository.Dispose();

            if (user != null)
            {
                bool result = SecurePasswordHasher.Verify(password, user.Password);
                if (result)
                    return user;
            }
            return null;
        }
    }
}
