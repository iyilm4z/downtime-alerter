using System;
using DowntimeAlerter.Authorization.Users;
using DowntimeAlerter.Domain.Entities;

namespace DowntimeAlerter.Logging
{
    public class Log : Entity
    {
        public string ShortMessage { get; set; }

        public string FullMessage { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public LogLevel LogLevel { get; set; }

        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
