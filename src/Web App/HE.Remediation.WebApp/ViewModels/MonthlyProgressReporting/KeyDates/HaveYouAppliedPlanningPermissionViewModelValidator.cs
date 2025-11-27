using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;
public class HaveYouAppliedPlanningPermissionViewModelValidator : AbstractValidator<HaveYouAppliedPlanningPermissionViewModel>
{
    public HaveYouAppliedPlanningPermissionViewModelValidator()
    {
        RuleFor(x => x.HaveAppliedPlanningPermission)
            .NotEmpty().WithMessage("Please select an option.");
    }
}