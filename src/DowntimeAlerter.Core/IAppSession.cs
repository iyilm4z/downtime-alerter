using DowntimeAlerter.Authorization.Users;

namespace DowntimeAlerter
{
    public interface IAppSession
    {
        IUserInfo User { get; set; }
    }
}
