using DowntimeAlerter.Authentication;
using DowntimeAlerter.Authorization.Users;
using DowntimeAlerter.Configuration;
using DowntimeAlerter.Domain.Repositories;
using DowntimeAlerter.Web.Factories.Home;
using DowntimeAlerter.Web.Models.Home;
using DowntimeAlerter.Web.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DowntimeAlerter.Web.Controllers
{
    public class AuthenticationController : AppControllerBase
    {
        private readonly IHomeModelFactory _homeModelFactory;
        private readonly AppConfig _appConfig;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IRepository<User> _userRepository;

        public AuthenticationController(IHomeModelFactory homeModelFactory,
            AppConfig appConfig,
            IAuthenticationManager authenticationManager,
            IRepository<User> userRepository)
        {
            _homeModelFactory = homeModelFactory;
            _appConfig = appConfig;
            _authenticationManager = authenticationManager;
            _userRepository = userRepository;
        }

        public IActionResult Login()
        {
            var model = _homeModelFactory.PrepareLoginModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_appConfig.UsernamesEnabledForLogin && model.Username != null)
                {
                    model.Username = model.Username.Trim();
                }

                var loginResult =
                    _authenticationManager.ValidateUser(
                        _appConfig.UsernamesEnabledForLogin ? model.Username : model.Email, model.Password);
                switch (loginResult)
                {
                    case ValidateUserResult.Successful:
                    {
                        var user = _appConfig.UsernamesEnabledForLogin
                            ? _userRepository.GetUserByUsername(model.Username)
                            : _userRepository.GetUserByEmail(model.Email);

                        _authenticationManager.SignIn(user, model.RememberMe);

                        if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                        {
                            return RedirectToAction("Index", "Home");
                        }

                        return Redirect(returnUrl);
                    }
                    case ValidateUserResult.UserNotExist:
                        ModelState.AddModelError("", "User not exist");
                        break;
                    case ValidateUserResult.NotRegistered:
                        ModelState.AddModelError("", "User not registered");
                        break;
                    case ValidateUserResult.WrongPassword:
                        // ignored
                        break;
                    default:
                        ModelState.AddModelError("", "Wrong credentials");
                        break;
                }
            }

            model = _homeModelFactory.PrepareLoginModel();

            return View(model);
        }

        public IActionResult Logout()
        {
            _authenticationManager.SignOut();

            return RedirectToAction(nameof(Login));
        }
    }
}