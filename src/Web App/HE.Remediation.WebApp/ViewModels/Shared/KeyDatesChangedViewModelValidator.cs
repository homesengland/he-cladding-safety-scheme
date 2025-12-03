using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.Shared;

public abstract class KeyDatesChangedViewModelValidator<T> : AbstractValidator<T> where T : KeyDatesChangedViewModel
{
    protected KeyDatesChangedViewModelValidator()
    {
        RuleFor(x => x.ChangeTypeId)
            .NotNull()
            .WithMessage("Reason for change is required");

        RuleFor(x => x.ChangeReason)
            .NotEmpty()
            .WithMessage("Details about the slippage is required")
            .MaximumLength(1000)
            .WithMessage("Details about the slippage cannot be more than 1000 characters");
    }
}