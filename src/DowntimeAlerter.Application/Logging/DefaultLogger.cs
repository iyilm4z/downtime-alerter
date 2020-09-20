using System;
using DowntimeAlerter.Authorization.Users;
using DowntimeAlerter.Domain.Repositories;

namespace DowntimeAlerter.Logging
{
    public class DefaultLogger : ILogger
    {
        private readonly IRepository<Log> _logRepository;

        public DefaultLogger(IRepository<Log> logRepository)
        {
            _logRepository = logRepository;
        }

        private void InsertLog(LogLevel logLevel, string shortMessage, string fullMessage = "", IUserInfo user = null)
        {
            var log = new Log
            {
                LogLevel = logLevel,
                ShortMessage = shortMessage,
                FullMessage = fullMessage,
                User = user as User,
                CreatedOnUtc = DateTime.UtcNow
            };

            _logRepository.Insert(log);
        }

        public bool IsEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return false;
                default:
                    return true;
            }
        }

        public void Info(string message, Exception exception = null, IUserInfo user = null)
        {
            if (exception is System.Threading.ThreadAbortException)
                return;

            if (IsEnabled(LogLevel.Information))
                InsertLog(LogLevel.Information, message, exception?.ToString() ?? string.Empty, user);
        }

        public void Warning(string message, Exception exception = null, IUserInfo user = null)
        {
            if (exception is System.Threading.ThreadAbortException)
                return;

            if (IsEnabled(LogLevel.Warning))
                InsertLog(LogLevel.Warning, message, exception?.ToString() ?? string.Empty, user);
        }

        public void Error(string message, Exception exception = null, IUserInfo user = null)
        {
            if (exception is System.Threading.ThreadAbortException)
                return;

            if (IsEnabled(LogLevel.Error))
                InsertLog(LogLevel.Error, message, exception?.ToString() ?? string.Empty, user);
        }
    }
}