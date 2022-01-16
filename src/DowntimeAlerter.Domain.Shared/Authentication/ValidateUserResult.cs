namespace DowntimeAlerter.Authentication
{
    public enum ValidateUserResult
    {
        Successful = 1,

        UserNotExist = 2,

        WrongPassword = 3,

        NotRegistered = 4
    }
}
