using System;
using DowntimeAlerter.Authorization;
using DowntimeAlerter.Authorization.Users;
using DowntimeAlerter.Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace DowntimeAlerter.Web
{
    public class AppSession : IAppSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IRepository<User> _userRepository;

        private IUserInfo _cachedUser;

        public AppSession(IHttpContextAccessor httpContextAccessor,
            IAuthenticationManager authenticationManager,
            IRepository<User> userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _authenticationManager = authenticationManager;
            _userRepository = userRepository;
        }

        private string GetUserCookie()
        {
            var cookieName = $"{AppDefaults.Cookie.Prefix}{AppDefaults.Cookie.User}";

            return _httpContextAccessor.HttpContext?.Request?.Cookies[cookieName];
        }

        private void SetUserCookie(Guid userGuid)
        {
            if (_httpContextAccessor.HttpContext?.Response == null)
                return;

            var cookieName = $"{AppDefaults.Cookie.Prefix}{AppDefaults.Cookie.User}";
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookieName);

            var cookieExpiresDate = DateTime.Now.AddHours(24);

            if (userGuid == Guid.Empty)
                cookieExpiresDate = DateTime.Now.AddMonths(-1);

            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = cookieExpiresDate
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookieName, userGuid.ToString(), options);
        }

        public IUserInfo User
        {
            get
            {
                if (_cachedUser != null)
                    return _cachedUser;

                IUserInfo user = _authenticationManager.GetAuthenticatedUser();

                if (user == null)
                {
                    var userCookie = GetUserCookie();
                    if (!string.IsNullOrEmpty(userCookie))
                    {
                        if (Guid.TryParse(userCookie, out var customerGuid))
                        {
                            var userByGuid = _userRepository.GetUserByGuid(customerGuid);
                            if (userByGuid != null && !userByGuid.IsRegistered())
                                user = userByGuid;
                        }
                    }
                }

                user ??= _userRepository.InsertGuestUser();

                SetUserCookie(user.UserGuid);

                _cachedUser = user;

                return _cachedUser;
            }
            set
            {
                SetUserCookie(value.UserGuid);
                _cachedUser = value;
            }
        }
    }
}
