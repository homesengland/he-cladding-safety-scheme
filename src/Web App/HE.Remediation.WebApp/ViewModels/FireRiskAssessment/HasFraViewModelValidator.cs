using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class HasFraViewModelValidator : AbstractValidator<HasFraViewModel>
{
    public HasFraViewModelValidator()
    {
        RuleFor(x => x.HasFra)
            .NotNull()
            .WithMessage("Select an option");
    }
}