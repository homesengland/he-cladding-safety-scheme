using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class PlanningPermissionDetailsViewModelValidator : AbstractValidator<PlanningPermissionDetailsViewModel>
{
    private const int MaxYear = 2040;

    public PlanningPermissionDetailsViewModelValidator()
    {
        RuleFor(x => x.PlanningPermissionSubmittedMonth)
            .NotNull()
            .WithMessage("Please provide a submitted month")
            .InclusiveBetween(1, 12)
            .WithMessage("Please provide a submitted month");

        RuleFor(x => x.PlanningPermissionSubmittedYear)
            .NotNull()
            .WithMessage("Please provide a submitted year")
            .NotEmpty()
            .WithMessage("Please provide a submitted year")
            .LessThanOrEqualTo(MaxYear)
            .WithMessage($"Please provide a submitted year no later than {MaxYear}")
            .DependentRules(() =>
            {
                RuleFor(x => x)
                    .Must(x => NotInTheFuture(x.PlanningPermissionSubmittedMonth, x.PlanningPermissionSubmittedYear))
                    .WithName("PlanningPermissionSubmittedMonth")
                    .WithMessage(x => $"The submitted date cannot be in the future")
                    .When(x => x.PlanningPermissionSubmittedMonth.HasValue && x.PlanningPermissionSubmittedYear.HasValue);
            });

        RuleFor(x => x.PlanningPermissionApprovedMonth)
            .NotNull()
            .When(x => x.PlanningPermissionApprovedYear.HasValue)
            .WithMessage("Please provide a approved month")
            .InclusiveBetween(1, 12)
            .WithMessage("Please provide a approved month");

        RuleFor(x => x.PlanningPermissionApprovedYear)
            .NotNull()
            .When(x => x.PlanningPermissionApprovedMonth.HasValue)
            .WithMessage("Please provide a approved year")
            .NotEmpty()
            .When(x => x.PlanningPermissionApprovedMonth.HasValue)
            .WithMessage("Please provide a approved year")
            .LessThanOrEqualTo(MaxYear)
            .WithMessage($"Please provide a approved year no later than {MaxYear}")
            .DependentRules(() =>
            {
                RuleFor(x => x)
                    .Must(x => NotInTheFuture(x.PlanningPermissionApprovedMonth, x.PlanningPermissionApprovedYear))
                    .WithName("PlanningPermissionApprovedMonth")
                    .WithMessage(x => $"The approved date cannot be in the future")
                    .When(x => x.PlanningPermissionApprovedMonth.HasValue && x.PlanningPermissionApprovedYear.HasValue)
                    .Must(x => SecondDateIsAfterOrSameAsFirst(x.PlanningPermissionSubmittedMonth, x.PlanningPermissionSubmittedYear, x.PlanningPermissionApprovedMonth, x.PlanningPermissionApprovedYear))
                    .WithName("PlanningPermissionApprovedMonth")
                    .WithMessage(x => $"The approved date cannot be before the submitted date")
                    .When(x => x.PlanningPermissionApprovedMonth.HasValue && x.PlanningPermissionApprovedYear.HasValue);
            });
    }

    private bool NotInTheFuture(int? dateMonth, int? dateYear)
    {
        if (dateMonth is null or < 1 or > 12) return false;
        if (dateYear is null or < 1 or > MaxYear) return false;

        var date = new DateTime(dateYear.Value, dateMonth.Value, 1).AddMonths(1).AddDays(-1);
        var lastDateOfCurrentMonth = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, 1).AddMonths(1).AddDays(-1);

        return date <= lastDateOfCurrentMonth;
    }

    private bool SecondDateIsAfterOrSameAsFirst(int? firstDateMonth, int? firstDateYear, int? secondDateMonth, int? secondDateYear)
    {
        if (firstDateMonth is null or < 1 or > 12) return false;
        if (firstDateYear is null or < 1 or > MaxYear) return false;
        if (secondDateMonth is null or < 1 or > 12) return false;
        if (secondDateYear is null or < 1 or > MaxYear) return false;

        var firstDateForComparison = new DateTime(firstDateYear.Value, firstDateMonth.Value, 1);
        var secondDateForComparison = new DateTime(secondDateYear.Value, secondDateMonth.Value, 1);

        return secondDateForComparison.Date >= firstDateForComparison;
    }
}
