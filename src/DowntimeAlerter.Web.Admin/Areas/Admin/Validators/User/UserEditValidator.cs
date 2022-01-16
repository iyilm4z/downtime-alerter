using DowntimeAlerter.Authorization.Users.Dto;
using DowntimeAlerter.Configuration;
using FluentValidation;

namespace DowntimeAlerter.Web.Areas.Admin.Validators.User
{
    public class UserEditDtoValidator : AbstractValidator<UserEditDto>
    {
        public UserEditDtoValidator(AppConfig appConfig)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is wrong");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.Role).NotEmpty().WithMessage("Role is required");
        }
    }
}
