using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ProgressSupportViewModelValidator : AbstractValidator<ProgressSupportViewModel>
{
    public ProgressSupportViewModelValidator()
    {
        RuleFor(x => x.SupportNeededReason)
            .NotEmpty()
            .WithMessage("Enter what help you need")
            .MaximumLength(1000)
            .WithMessage("Help needed cannot exceed 1000 characters");
    }
}