using DowntimeAlerter.Configuration;
using DowntimeAlerter.Web.Models.Authentication;
using FluentValidation;

namespace DowntimeAlerter.Web.Validators.Authentication
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator(AppConfig appConfig)
        {
            if (!appConfig.UsernamesEnabledForLogin)
            {
                RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
                RuleFor(x => x.Email).EmailAddress().WithMessage("Email is wrong");
            }
        }
    }
}
