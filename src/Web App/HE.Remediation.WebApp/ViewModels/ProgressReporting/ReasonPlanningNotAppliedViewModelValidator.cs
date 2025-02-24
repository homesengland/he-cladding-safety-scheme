using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ReasonPlanningNotAppliedViewModelValidator : AbstractValidator<ReasonPlanningNotAppliedViewModel>
{
    public ReasonPlanningNotAppliedViewModelValidator()
    {
        RuleFor(x => x.ReasonPlanningPermissionNotApplied)
            .NotNull()
            .WithMessage("Please provide a reason")
            .MaximumLength(150)
            .WithMessage("Reason must be less than 150 characters");

        RuleFor(x => x.PlanningPermissionNeedsSupport)
            .NotNull()
            .WithMessage("Please select Yes or No");

    }
}
