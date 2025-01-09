using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class CladdingRemovedViewModelValidator : AbstractValidator<CladdingRemovedViewModel>
{
    public CladdingRemovedViewModelValidator()
    {
        RuleFor(x => x.UnsafeCladdingRemoved)
            .NotNull()
            .WithMessage("Select an option");        
    }
}
