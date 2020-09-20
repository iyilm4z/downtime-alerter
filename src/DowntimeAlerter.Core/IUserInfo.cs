using System;

namespace DowntimeAlerter
{
    public interface IUserInfo
    {
        int Id { get; set; }

        Guid UserGuid { get; set; }

        string Name { get; set; }

        string Surname { get; set; }

        string Fullname { get; }

        string UserName { get; set; }

        string Email { get; set; }
    }
}
