using DowntimeAlerter.Authorization.Users;

namespace DowntimeAlerter.Authorization
{
    public interface IAuthenticationManager
    {
        void SignIn(User user, bool isPersistent);

        void SignOut();

        User GetAuthenticatedUser();

        UserLoginResults ValidateUser(string usernameOrEmail, string password);
    }
}