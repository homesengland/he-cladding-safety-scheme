using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

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