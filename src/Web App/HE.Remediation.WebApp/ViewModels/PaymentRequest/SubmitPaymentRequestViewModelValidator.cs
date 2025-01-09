using FluentValidation;
using HE.Remediation.WebApp.ViewModels.Shared;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class SubmitPaymentRequestViewModelValidator : AbstractValidator<SubmitPaymentRequestViewModel>
{
    public SubmitPaymentRequestViewModelValidator()
    {
        RuleFor(x => x.Costs.UnprofiledGrantFunding)
            .GreaterThanOrEqualTo(0)
            .WithMessage("You have over allocated your funding. Please update your monthly cost profile to match your approved grant funding.");
    }
}
