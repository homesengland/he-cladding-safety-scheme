using FluentValidation;
using HE.Remediation.Core.Extensions;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class ProjectDatesViewModelValidator : AbstractValidator<ProjectDatesViewModel>
{
    private DateTime EarliestDate = new(2022, 1, 1);
    private const int MaxYear = 2040;

    public ProjectDatesViewModelValidator()
    {
        #region ProjectStartDate

        RuleFor(x => x.ProjectStartDateMonth)
            .NotNull()
            .WithMessage("Please provide a start month")
            .NotEmpty()
            .WithMessage("Please provide a start month")
            .InclusiveBetween(1, 12)
            .WithMessage("Please provide a start month");

        RuleFor(x => x.ProjectStartDateYear)
            .NotNull()
            .WithMessage("Please provide a start year")
            .NotEmpty()
            .WithMessage("Please provide a start year")
            .LessThanOrEqualTo(MaxYear)
            .WithMessage($"Please provide a start year no later than {MaxYear}")
            .DependentRules(() =>
            {
                RuleFor(x => x)
                    .Must(x => BeOnAfter(x.ProjectStartDateMonth, x.ProjectStartDateYear, EarliestDate.Month, EarliestDate.Year))
                    .WithName("ProjectStartDateMonth")
                    .WithMessage($"The start date cannot be earlier than 01/2022")
                    .Must(x => AfterThisMonth(x.ProjectStartDateMonth, x.ProjectStartDateYear))
                    .WithName(x => nameof(x.ProjectStartDateMonth))
                    .WithMessage("Start date must be in the future")
                    .When(x => x.ProjectStartDateMonth.HasValue && x.ProjectStartDateYear.HasValue, ApplyConditionTo.AllValidators);
            });

        #endregion

        #region ProjectEndDate

        RuleFor(x => x.ProjectEndDateMonth)
            .NotNull()
            .WithMessage("Please provide an end month")
            .NotEmpty()
            .WithMessage("Please provide an end month")
            .InclusiveBetween(1, 12)
            .WithMessage("Please provide an end month");

        RuleFor(x => x.ProjectEndDateYear)
            .NotNull()
            .WithMessage("Please provide an end year")
            .NotEmpty()
            .WithMessage("Please provide an end year")
            .LessThanOrEqualTo(MaxYear)
            .WithMessage($"Please provide an end year no later than {MaxYear}")
            .DependentRules(() =>
            {
                RuleFor(x => x)
                    .Must(x => BeOnAfter(x.ProjectEndDateMonth, x.ProjectEndDateYear, x.ProjectStartDateMonth, x.ProjectStartDateYear))
                    .WithName("ProjectEndDateMonth")
                    .WithMessage(x => $"The end date must be no earlier than {x.ProjectStartDateMonth}/{x.ProjectStartDateYear}")
                    .When(x => x.ProjectStartDateMonth.HasValue && x.ProjectStartDateYear.HasValue &&
                               x.ProjectEndDateMonth.HasValue && x.ProjectEndDateYear.HasValue);
            });

        #endregion
    }

    private bool BeOnAfter(int? actualMonth, int? actualYear, int? validMonth, int? validYear)
    {
        if (actualMonth is null or < 1 or > 12) return false;
        if (actualYear is null or < 1 or > MaxYear) return false;
        if (validMonth is null or < 1 or > 12) return false;
        if (validYear is null or < 1 or > MaxYear) return false;

        var date = new DateTime(actualYear.Value, actualMonth.Value, 1);
        var validDate = new DateTime(validYear.Value, validMonth.Value, 1).AddDays(-1);

        return date.Date > validDate;
    }

    private bool AfterThisMonth(int? month, int? year)
    {
        if (month is null or < 1 or > 12)
        {
            return false;
        }

        if (year is null or < 1 or > MaxYear)
        {
            return false;
        }

        var today = DateTime.Today;
        var date = new DateTime(year.Value, month.Value, 1);

        var validDate = today.FirstOfMonth().AddMonths(1);

        return date >= validDate;
    }
}
