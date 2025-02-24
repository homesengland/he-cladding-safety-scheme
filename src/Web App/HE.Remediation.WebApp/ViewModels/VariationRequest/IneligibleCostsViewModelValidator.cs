using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class IneligibleCostsViewModelValidator : AbstractValidator<IneligibleCostsViewModel>
{
    public IneligibleCostsViewModelValidator()
    {
        RuleFor(x => x.HasVariationIneligibleCosts)
            .NotNull()
            .WithMessage("Select an option");
    }
}
