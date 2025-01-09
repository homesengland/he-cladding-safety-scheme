using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class PaymentsSummaryViewModelValidator : AbstractValidator<PaymentsSummaryViewModel>
{
    public PaymentsSummaryViewModelValidator()
    {
        RuleFor(x => x.Costs.UnprofiledGrantFunding)
            .Must(x => x is null || (Math.Truncate(x.Value) == 0))
            .WithMessage("Your unprofiled grant funding must be zero. Please update your monthly cost profile to match your approved grant funding.");
    }
}
