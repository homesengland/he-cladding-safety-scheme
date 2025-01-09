using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class ConfirmRemoveVariationReasonViewModelValidator : AbstractValidator<ConfirmRemoveVariationReasonViewModel>
{
    public ConfirmRemoveVariationReasonViewModelValidator()
    {
        RuleFor(x => x.Proceed)
           .NotNull()
           .WithMessage("Please select Yes or No");
    }
}
