using DowntimeAlerter.Web.Models.Authentication;

namespace DowntimeAlerter.Web.Factories.Authentication
{
    public interface IHomeModelFactory
    {
        LoginModel PrepareLoginModel();
    }
}