using System;
using DowntimeAlerter.Authorization.Users;

namespace DowntimeAlerter.Logging
{
    public interface ILogger
    {
        bool IsEnabled(LogLevel level);

        void Info(string message, Exception exception = null, IUserInfo user = null);

        void Warning(string message, Exception exception = null, IUserInfo user = null);

        void Error(string message, Exception exception = null, IUserInfo user = null);
    }
}