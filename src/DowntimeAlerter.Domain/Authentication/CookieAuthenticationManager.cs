using System;
using System.Collections.Generic;
using System.Security.Claims;
using DowntimeAlerter.Authorization.Users;
using DowntimeAlerter.Configuration;
using DowntimeAlerter.Domain.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace DowntimeAlerter.Authentication
{
    public class CookieAuthenticationManager : IAuthenticationManager
    {
        private readonly AppConfig _appConfig;
        private readonly IRepository<User> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private IUserInfo _cachedUser;

        public CookieAuthenticationManager(AppConfig appConfig,
            IRepository<User> userRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _appConfig = appConfig;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async void SignIn(IUserInfo user, bool isPersistent)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(user.UserName))
                claims.Add(new Claim(ClaimTypes.Name, user.UserName, ClaimValueTypes.String, AppDefaults.Authentication.ClaimsIssuer));

            if (!string.IsNullOrEmpty(user.Email))
                claims.Add(new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.Email, AppDefaults.Authentication.ClaimsIssuer));

            var userIdentity = new ClaimsIdentity(claims, AppDefaults.Authentication.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent,
                IssuedUtc = DateTime.UtcNow
            };

            await _httpContextAccessor.HttpContext.SignInAsync(AppDefaults.Authentication.AuthenticationScheme, userPrincipal, authenticationProperties);

            _cachedUser = user;
        }

        public async void SignOut()
        {
            _cachedUser = null;

            await _httpContextAccessor.HttpContext.SignOutAsync(AppDefaults.Authentication.AuthenticationScheme);
        }

        public IUserInfo GetAuthenticatedUser()
        {
            if (_cachedUser != null)
                return _cachedUser;

            var authenticateResult = _httpContextAccessor.HttpContext.AuthenticateAsync(AppDefaults.Authentication.AuthenticationScheme).Result;
            if (!authenticateResult.Succeeded)
                return null;

            User user = null;

            if (_appConfig.UsernamesEnabledForLogin)
            {
                var usernameClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Name
                    && claim.Issuer.Equals(AppDefaults.Authentication.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));
                if (usernameClaim != null)
                    user = _userRepository.GetUserByUsername(usernameClaim.Value);
            }
            else
            {
                var emailClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Email
                    && claim.Issuer.Equals(AppDefaults.Authentication.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));
                if (emailClaim != null)
                    user = _userRepository.GetUserByEmail(emailClaim.Value);
            }

            if (user == null)
                return null;

            _cachedUser = user;

            return _cachedUser;
        }

        public ValidateUserResult ValidateUser(string usernameOrEmail, string password)
        {
            var user = _appConfig.UsernamesEnabledForLogin ?
                _userRepository.GetUserByUsername(usernameOrEmail) :
                _userRepository.GetUserByEmail(usernameOrEmail);

            if (user == null)
                return ValidateUserResult.UserNotExist;

            if (!user.IsRegistered())
                return ValidateUserResult.NotRegistered;

            if (!user.Password.Equals(password))
            {
                user.FailedLoginAttempts++;

                _userRepository.Update(user);

                return ValidateUserResult.WrongPassword;
            }

            user.LastLoginDateUtc = DateTime.UtcNow;
            _userRepository.Update(user);

            return ValidateUserResult.Successful;
        }
    }
}