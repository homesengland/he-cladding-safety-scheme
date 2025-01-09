using FluentValidation;
using HE.Remediation.WebApp.Extensions;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageThirdPartyContributions;

public class ThirdPartyContributionViewModelValidator : AbstractValidator<ThirdPartyContributionViewModel>
{
    public ThirdPartyContributionViewModelValidator()
    {
        RuleFor(x => x.ContributionPursuingTypes)
            .NotEmpty()
            .WithMessage("Select the type of contribution you have received");

        RuleFor(x => x.ContributionAmount)
            .NotEmpty()
            .WithMessage("Value of the contribution must be a number")
            .GreaterThan(0M)
            .WithMessage(x => "Value of the contribution must be a positive number")
            .Must(x => x.HaveNoDecimalsInAmount())
            .WithMessage(x => "Value of the contribution must be a whole number");

        RuleFor(x => x.ContributionNotes)
            .NotEmpty()
            .WithMessage("Please tell us more about the contribution")
            .MaximumLength(500)
            .WithMessage("Tell us more about the contribution cannot exceed 500 characters");
    }
}