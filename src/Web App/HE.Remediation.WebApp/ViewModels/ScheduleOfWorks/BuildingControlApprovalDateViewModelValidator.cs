using FluentValidation;

using HE.Remediation.Core.Extensions;
namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class BuildingControlApprovalDateViewModelValidator
    : AbstractValidator<BuildingControlApprovalDateViewModel>
{
    private const int SqlMinYear = 1753;

    public BuildingControlApprovalDateViewModelValidator()
    {
        var currentYear = DateTime.Today.Year;

        RuleFor(x => x.ApprovalDateDay)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Please enter day")
            .InclusiveBetween(1, 31)
            .When(x => x.ApprovalDateDay != null, ApplyConditionTo.CurrentValidator)
            .WithMessage("Please enter a valid day");

        RuleFor(x => x.ApprovalDateMonth)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Please enter month")
            .InclusiveBetween(1, 12)
            .When(x => x.ApprovalDateMonth != null, ApplyConditionTo.CurrentValidator)
            .WithMessage("Please enter a valid month");

        RuleFor(x => x.ApprovalDateYear)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Please enter year")
            .GreaterThanOrEqualTo(SqlMinYear)
                .When(x => x.ApprovalDateYear != null, ApplyConditionTo.CurrentValidator)
                .WithMessage("Please enter a valid year")
            .LessThanOrEqualTo(currentYear)
                .When(x => x.ApprovalDateYear != null, ApplyConditionTo.CurrentValidator)
                .WithMessage($"Year cannot be later than {currentYear}");

        // Only run these if day/month/year exist AND each is in its own basic range.
        RuleFor(x => new { x.ApprovalDateDay, x.ApprovalDateMonth, x.ApprovalDateYear })
            .Must(x => BeAValidDayInTheMonth(x.ApprovalDateDay, x.ApprovalDateMonth, x.ApprovalDateYear))
            .When(m => PartsReadyForDateValidation(m, currentYear))
            .WithName(x => nameof(x.ApprovalDateDay))
            .WithMessage("Please enter a valid day for the selected month")
            .Must(x => BeInThePast(x.ApprovalDateDay, x.ApprovalDateMonth, x.ApprovalDateYear))
            .When(m => PartsReadyForDateValidation(m, currentYear))
            .WithName(x => nameof(x.ApprovalDateYear))
            .WithMessage("Must be a date in the past");
    }

    private static bool PartsReadyForDateValidation(BuildingControlApprovalDateViewModel m, int currentYear)
    {
        if (!m.ApprovalDateDay.HasValue || !m.ApprovalDateMonth.HasValue || !m.ApprovalDateYear.HasValue)
            return false;

        var day = m.ApprovalDateDay.Value;
        var month = m.ApprovalDateMonth.Value;
        var year = m.ApprovalDateYear.Value;

        if (day < 1 || day > 31) return false;
        if (month < 1 || month > 12) return false;
        if (year < SqlMinYear || year > currentYear) return false;

        return true;
    }

    private bool BeAValidDayInTheMonth(int? day, int? month, int? year)
    {
        var currentYear = DateTime.Today.Year;
        if (month == null || month < 1 || month > 12) return false;
        if (year == null || year < SqlMinYear || year > currentYear) return false;

        return day.HasValue && day.Value >= 1 && day.Value <= DateTime.DaysInMonth(year.Value, month.Value);
    }

    private static bool BeInThePast(int? day, int? month, int? year)
    {
        var currentYear = DateTime.Today.Year;
        if (day == null || day < 1 || day > 31) return false;
        if (month == null || month < 1 || month > 12) return false;
        if (year == null || year < SqlMinYear || year > currentYear) return false;
        if (day.Value > DateTime.DaysInMonth(year.Value, month.Value)) return false;

        return new DateTime(year.Value, month.Value, day.Value).Date <= DateTime.Today;
    }
}