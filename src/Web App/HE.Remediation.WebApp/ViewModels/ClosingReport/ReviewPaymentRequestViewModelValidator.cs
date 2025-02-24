using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class ReviewPaymentRequestViewModelValidator : AbstractValidator<ReviewPaymentRequestViewModel>
{
    public ReviewPaymentRequestViewModelValidator()
    {
        RuleFor(x => x.ReasonForChange)
            .NotEmpty()
            .When(x => x.ChangeToMonthlyCost)
            .WithMessage("Reason for cost change required")
            .MaximumLength(1000)
            .WithMessage("Reason for cost change must be less than 1000 characters");
    }
}
