using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class ReviewPaymentRequestViewModelValidator : AbstractValidator<ReviewPaymentRequestViewModel>
{
    public ReviewPaymentRequestViewModelValidator()
    {
        RuleFor(x => x.ReasonForChange)
            .NotEmpty()
            .WithMessage("Please enter a reason for change")
            .MaximumLength(1000)
            .WithMessage("Reason cannot exceed 1000 characters")
            .When(x => x.ChangeToMonthlyCost);        
    }
}
