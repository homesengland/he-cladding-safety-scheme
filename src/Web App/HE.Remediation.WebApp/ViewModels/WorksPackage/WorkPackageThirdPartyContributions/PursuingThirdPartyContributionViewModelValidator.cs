using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageThirdPartyContributions;

public class PursuingThirdPartyContributionViewModelValidator : AbstractValidator<PursuingThirdPartyContributionViewModel>
{
    public PursuingThirdPartyContributionViewModelValidator()
    {
        RuleFor(x => x.PursuingThirdPartyContribution)
            .NotNull()
            .WithMessage("Select yes if you are pursuing any third party contributions");
    }
}