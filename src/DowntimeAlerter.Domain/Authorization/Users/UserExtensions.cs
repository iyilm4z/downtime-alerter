namespace DowntimeAlerter.Authorization.Users
{
    public static class UserExtensions
    {
        public static bool IsRegistered(this User user)
            => user.Role == UserRole.Admin || user.Role == UserRole.Registered;
    }
}