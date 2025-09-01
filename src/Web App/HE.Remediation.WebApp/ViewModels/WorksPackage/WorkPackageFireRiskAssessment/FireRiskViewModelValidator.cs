using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageFireRiskAssessment;

public class FireRiskViewModelValidator : AbstractValidator<FireRiskViewModel>
{
    public FireRiskViewModelValidator()
    {
        RuleFor(x => x.FireRiskRating)
            .NotNull()
            .WithMessage("Select an option");

        RuleFor(x => x.HasInternalFireSafetyRisks)
            .NotNull()
            .WithMessage("Select an option");
    }
}