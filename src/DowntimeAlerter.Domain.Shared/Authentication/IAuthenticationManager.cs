using DowntimeAlerter.Authorization.Users;

namespace DowntimeAlerter.Authentication
{
    public interface IAuthenticationManager
    {
        void SignIn(IUserInfo user, bool isPersistent);

        void SignOut();

        IUserInfo GetAuthenticatedUser();

        ValidateUserResult ValidateUser(string usernameOrEmail, string password);
    }
}