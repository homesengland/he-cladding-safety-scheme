using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;
public class ReasonForNonCompetitiveTenderViewModelValidator : AbstractValidator<ReasonForNonCompetitiveTenderViewModel>
{
    public ReasonForNonCompetitiveTenderViewModelValidator()
    {
        RuleFor(x => x.ReasonForNonCompetitiveTender)
            .NotEmpty().WithMessage("Please provide a reason for a non-competitive tender")
            .MaximumLength(1000).WithMessage("The maximum character limit of 1000 has been exceeded");
    }
}
