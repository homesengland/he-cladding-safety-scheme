using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class IneligibleCostViewModelValidator : AbstractValidator<IneligibleCostViewModel>
{
    public IneligibleCostViewModelValidator()
    {
        RuleFor(x => x.IneligibleCosts)
            .NotNull()
            .WithMessage("Please select Yes or No");
    }
}