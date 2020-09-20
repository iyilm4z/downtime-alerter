using DowntimeAlerter.Application.Services.Dto;

namespace DowntimeAlerter.Authorization.Users.Dto
{
    public class UserEditDto : EntityDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public UserRole Role { get; set; }
    }
}
