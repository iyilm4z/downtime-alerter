namespace DowntimeAlerter.Authorization.Users
{
    public enum UserLoginResults
    {
        Successful = 1,

        UserNotExist = 2,

        WrongPassword = 3,

        NotRegistered = 4
    }
}
