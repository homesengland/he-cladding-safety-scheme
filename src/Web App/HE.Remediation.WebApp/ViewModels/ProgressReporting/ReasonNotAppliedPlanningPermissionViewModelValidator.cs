using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;
public class ReasonNotAppliedPlanningPermissionViewModelValidator : AbstractValidator<ReasonNotAppliedPlanningPermissionViewModel>
{
    public ReasonNotAppliedPlanningPermissionViewModelValidator()
    {
        RuleFor(x => x.ReasonNotAppliedPlanningPermission)
            .NotEmpty().WithMessage("Please provide a reason for not applying for planning permission.")
                        .MaximumLength(150)
            .WithMessage("The maximum character limit of 150 has been exceeded");

        RuleFor(x => x.PlannedMonthToSubmitApplication)
            .NotNull()
            .WithMessage("Enter the month you expect to submit your application")
            .InclusiveBetween(1, 12)
            .WithMessage("Enter a valid month for when you expect to submit your application");

        RuleFor(x => x.PlannedYearToSubmitApplication)
            .NotNull()
            .WithMessage("Enter the year you expect to submit your application")
            .InclusiveBetween(2000, 3000)
            .WithMessage("Enter a valid year for when you expect to submit your application");

        RuleFor(x => x.PlanToSubmitDate)
            .Must(BeInFuture)
            .When(x => x.PlanToSubmitDate.HasValue)
            .OverridePropertyName(nameof(ReasonNotAppliedPlanningPermissionViewModel.PlannedYearToSubmitApplication))
            .WithMessage("Date must be in the future");
    }

    private bool BeInFuture(DateTime? date)
    {
        var today = DateTime.Today;
        return !date.HasValue || ((date.Value.Month >= today.Month && date.Value.Year == today.Year) || date.Value.Year > today.Year);
    }
}
