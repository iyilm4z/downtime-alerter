using DowntimeAlerter.Configuration;
using DowntimeAlerter.Web.Models.Home;

namespace DowntimeAlerter.Web.Factories.Home
{
    public class HomeModelFactory : IHomeModelFactory
    {
        private readonly AppConfig _appConfig;

        public HomeModelFactory(AppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public LoginModel PrepareLoginModel()
        {
            var model = new LoginModel
            {
                UsernamesEnabled = _appConfig.UsernamesEnabledForLogin
            };

            return model;
        }
    }
}
