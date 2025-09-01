using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageFireRiskAssessment;

public class FundingViewModelValidator : AbstractValidator<FundingViewModel>
{
    public FundingViewModelValidator()
    {
        RuleFor(x => x.HasFunding)
            .NotNull()
            .WithMessage("Select an option");

        RuleFor(x => x.HasFundingType)
            .NotNull()
            .When(x => x.HasFunding == true)
            .WithMessage("Select an option");

        RuleFor(x => x.HasNoFundingType)
            .NotNull()
            .When(x => x.HasFunding == false)
            .WithMessage("Select an option");
    }
}