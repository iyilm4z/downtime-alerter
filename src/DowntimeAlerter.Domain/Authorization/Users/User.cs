using System;
using DowntimeAlerter.Domain.Entities;

namespace DowntimeAlerter.Authorization.Users
{
    public class User : Entity, IUserInfo
    {
        public User()
        {
            UserGuid = Guid.NewGuid();
        }

        public Guid UserGuid { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Fullname => $"{Name}  {Surname}";

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public UserRole Role { get; set; }

        public DateTime? LastLoginDateUtc { get; set; }

        public int FailedLoginAttempts { get; set; }
    }
}
