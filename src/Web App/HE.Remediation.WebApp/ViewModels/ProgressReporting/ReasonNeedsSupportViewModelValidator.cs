using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ReasonNeedsSupportViewModelValidator : AbstractValidator<ReasonNeedsSupportViewModel>
{
    public ReasonNeedsSupportViewModelValidator()
    {
        RuleFor(x => x.SupportNeededReason)
            .NotNull()
            .WithMessage("Enter an explanation")
            .MaximumLength(500)
            .WithMessage("Explanation cannot exceed 500 characters");
    }
}
