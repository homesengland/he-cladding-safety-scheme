using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class FundingViewModelValidator : AbstractValidator<FundingViewModel>
{
    public FundingViewModelValidator()
    {
        RuleFor(x => x.HasFunding)
            .NotNull()
            .WithMessage("Select an option");

        When(x => x.HasFunding == true, () =>
        {
            RuleFor(x => x.HasFundingType)
                .NotNull()
                .WithMessage("Select an option")
                .Must(x => x is EFraFundingType.PaidForByThirdParty
                    or EFraFundingType.PaidForFromExistingServiceChargeFunds)
                .WithMessage("Select a valid option");
        });

        When(x => x.HasFunding == false, () =>
        {
            RuleFor(x => x.HasNoFundingType)
                .NotNull()
                .WithMessage("Select an option")
                .Must(x => x is EFraFundingType.RequestToBeMadeToThirdParty
                    or EFraFundingType.ServiceChargesFollowingS20
                    or EFraFundingType.CostOfWorksYetToBeSecured)
                .WithMessage("Select a valid option");
        });
    }
}