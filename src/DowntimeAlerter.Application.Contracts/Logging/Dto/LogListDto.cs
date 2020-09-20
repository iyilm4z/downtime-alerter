using System;
using DowntimeAlerter.Application.Services.Dto;
using DowntimeAlerter.Authorization.Users.Dto;

namespace DowntimeAlerter.Logging.Dto
{
    public class LogListDto : EntityDto
    {
        public string ShortMessage { get; set; }

        public string FullMessage { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public LogLevel LogLevel { get; set; }

        public UserListDto User { get; set; }
    }
}
