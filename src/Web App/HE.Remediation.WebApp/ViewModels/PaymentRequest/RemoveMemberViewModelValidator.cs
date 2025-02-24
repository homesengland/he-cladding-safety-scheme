using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class RemoveMemberViewModelValidator : AbstractValidator<RemoveMemberViewModel>
{
    public RemoveMemberViewModelValidator()
    {
        RuleFor(x => x.Confirm)
            .NotNull()
            .WithMessage("Select an option");
    }
}
