using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.AlternativeFundingRoutes;

public class CostRecoveryViewModelValidator : AbstractValidator<CostRecoveryViewModel>
{
    public CostRecoveryViewModelValidator()
    {
        RuleFor(x => x.CostRecoveryType)
            .NotNull()
            .WithMessage("Select an option")
            .IsInEnum()
            .WithMessage("Select a valid option");
    }
}