using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ReasonNoDesignerViewModelValidator : AbstractValidator<ReasonNoDesignerViewModel>
{
    public ReasonNoDesignerViewModelValidator()
    {
        RuleFor(x => x.LeadDesignerNotAppointedReason)
            .NotNull()
            .WithMessage("Please provide a reason")
            .MaximumLength(150)
            .WithMessage("Reason must be less than 150 characters");

        RuleFor(x => x.LeadDesignerNeedsSupport)
            .NotNull()
            .WithMessage("Please select Yes or No");
    }
}
