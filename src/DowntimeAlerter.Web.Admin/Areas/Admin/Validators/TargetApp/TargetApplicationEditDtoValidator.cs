using DowntimeAlerter.Configuration;
using DowntimeAlerter.Monitoring.Dto;
using FluentValidation;

namespace DowntimeAlerter.Web.Areas.Admin.Validators.TargetApp
{
    public class TargetApplicationEditDtoValidator : AbstractValidator<TargetApplicationEditDto>
    {
        public TargetApplicationEditDtoValidator(AppConfig appConfig)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Url).NotEmpty().WithMessage("Url is required");
            RuleFor(x => x.Interval).NotEmpty().WithMessage("Interval is required");
        }
    }
}
