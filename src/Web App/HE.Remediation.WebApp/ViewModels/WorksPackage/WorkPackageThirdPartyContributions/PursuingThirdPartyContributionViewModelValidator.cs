using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageThirdPartyContributions;

public class PursuingThirdPartyContributionViewModelValidator : AbstractValidator<PursuingThirdPartyContributionViewModel>
{
    public PursuingThirdPartyContributionViewModelValidator()
    {
        RuleFor(x => x.ThirdPartyContributionPursuitStatusTypeId)
            .NotNull()
            .WithMessage("Please select an option to indicate whether you are pursuing any third party contributions.");
    }
}