using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;
public class PlanningPermissionViewModelValidator : AbstractValidator<PlanningPermissionViewModel>
{
    public PlanningPermissionViewModelValidator()
    {
        RuleFor(x => x.WorksNeedPlanningPermission)
            .NotEmpty().WithMessage("Please select an option.");
    }
}
