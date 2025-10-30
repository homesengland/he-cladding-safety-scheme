using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class ReasonNoThirdPartyContributionsViewModelValidator : AbstractValidator<ReasonNoThirdPartyContributionsViewModel>
{
    public ReasonNoThirdPartyContributionsViewModelValidator()
    {
        RuleFor(x => x.ReasonNoThirdPartyContributions)
            .NotEmpty()
            .WithMessage("Please provide an Explanation")
            .MaximumLength(250)
            .WithMessage("Explanation must be 250 characters or less");
    }
}
