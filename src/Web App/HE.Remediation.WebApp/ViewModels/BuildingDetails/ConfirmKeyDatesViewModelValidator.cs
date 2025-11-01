using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class ConfirmKeyDatesViewModelValidator : AbstractValidator<ConfirmKeyDatesViewModel>
{
    private const int SqlMinYear = 1753;

    public ConfirmKeyDatesViewModelValidator()
    {

        #region StartDate

        RuleFor(x => x.StartDateMonth)
                .NotNull()
                .WithMessage("Please enter a month")
                .NotEmpty()
                .WithMessage("Please enter a month")
                .InclusiveBetween(1, 12)
                .WithMessage("Please enter a valid month (1–12)");

        RuleFor(x => x.StartDateYear)
                .NotNull()
                .WithMessage("Please enter a year")
                .NotEmpty()
                .WithMessage("Please enter a year")
                .GreaterThanOrEqualTo(SqlMinYear)
                .When(x => x.StartDateYear != null, ApplyConditionTo.CurrentValidator)
                .WithMessage("Please enter a valid year")
                .DependentRules(() =>
                {
                    RuleFor(x => new { x.StartDateMonth, x.StartDateYear })
                    .Must(x => BeInThePast(x.StartDateMonth, x.StartDateYear))
                    .WithName("StartDateMonth")
                    .WithMessage($"The start date must be in the past")
                    .When(x => x.StartDateMonth.HasValue && x.StartDateYear.HasValue);
                });

        #endregion

        #region UnsafeCladdingRemovalDate

        #region UnsafeCladdingRemovalDate (required)

        RuleFor(x => x.UnsafeCladdingRemovalDateMonth)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Please enter a month")
            .InclusiveBetween(1, 12).WithMessage("Please enter a valid month (1–12)");

        RuleFor(x => x.UnsafeCladdingRemovalDateYear)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Please enter a year")
                .GreaterThanOrEqualTo(SqlMinYear)
                .When(x => x.UnsafeCladdingRemovalDateYear != null, ApplyConditionTo.CurrentValidator)
                .WithMessage("Please enter a valid year");

        RuleFor(x => new
        {
            x.UnsafeCladdingRemovalDateMonth,
            x.UnsafeCladdingRemovalDateYear
        })
                .Must(p => BeInThePast(p.UnsafeCladdingRemovalDateMonth, p.UnsafeCladdingRemovalDateYear))
                .WithName("UnsafeCladdingRemovalDateMonth")
                .WithMessage("The removal date must be in the past")
                .When(x => x.UnsafeCladdingRemovalDateMonth.HasValue && x.UnsafeCladdingRemovalDateYear.HasValue);

        RuleFor(x => x)
                    .Must(x => BeAfter(x.UnsafeCladdingRemovalDateMonth, x.UnsafeCladdingRemovalDateYear,
                                       x.StartDateMonth, x.StartDateYear))
                    .WithName("UnsafeCladdingRemovalDateMonth")
                    .WithMessage("The removal date must be after the start date")
                    .When(x => x.StartDateMonth.HasValue && x.StartDateYear.HasValue
                               && x.UnsafeCladdingRemovalDateMonth.HasValue && x.UnsafeCladdingRemovalDateYear.HasValue);

        #endregion


        #endregion

        #region ExpectedDateForCompletion (required)

        RuleFor(x => x.ExpectedDateForCompletionMonth)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Please enter a month")
                    .InclusiveBetween(1, 12).WithMessage("Please enter a valid month (1–12)");

        RuleFor(x => x.ExpectedDateForCompletionYear)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Please enter a year")
                    .GreaterThanOrEqualTo(SqlMinYear)
                    .When(x => x.ExpectedDateForCompletionYear != null, ApplyConditionTo.CurrentValidator)
                    .WithMessage("Please enter a valid year");

        // Must be in the past
        RuleFor(x => new { x.ExpectedDateForCompletionMonth, x.ExpectedDateForCompletionYear })
                    .Must(p => BeInThePastOrCurrentMonth(p.ExpectedDateForCompletionMonth, p.ExpectedDateForCompletionYear))
                    .WithName("ExpectedDateForCompletionMonth")
                    .WithMessage("The completion date must be in the past")
                    .When(x => x.ExpectedDateForCompletionMonth.HasValue && x.ExpectedDateForCompletionYear.HasValue);

        // Must be after the start date
        RuleFor(x => x)
            .Must(x => BeAfter(x.ExpectedDateForCompletionMonth, x.ExpectedDateForCompletionYear,
                               x.StartDateMonth, x.StartDateYear))
            .WithName("ExpectedDateForCompletionMonth")
            .WithMessage("The completion date must be after the start date")
            .When(x => x.StartDateMonth.HasValue && x.StartDateYear.HasValue
                       && x.ExpectedDateForCompletionMonth.HasValue && x.ExpectedDateForCompletionYear.HasValue);

        #endregion
    }

    private bool BeInThePast(int? month, int? year)
    {
        var minDate = new DateTime(1753, 1, 1);

        if (month is null or < 1 or > 12) return false;
        if (year is null || year < minDate.Year) return false;

        var date = new DateTime(year.Value, month.Value, 1);

        if (date < minDate) return false;

        var currentMonthStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

        return date < currentMonthStart;
    }


    private bool BeInThePastOrCurrentMonth(int? month, int? year)
    {
        var minDate = new DateTime(SqlMinYear, 1, 1);

        if (month is null or < 1 or > 12) return false;
        if (year is null || year < minDate.Year) return false;

        var date = new DateTime(year.Value, month.Value, 1);
        if (date < minDate) return false;

        var currentMonthStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        return date <= currentMonthStart; 
    }

    private bool BeAfter(int? actualMonth, int? actualYear, int? validMonth, int? validYear)
    {
        if (actualMonth is null or < 1 or > 12) return false;
        if (actualYear is null or < 1) return false;
        if (validMonth is null or < 1 or > 12) return false;
        if (validYear is null or < 1) return false;

        var date = new DateTime(actualYear.Value, actualMonth.Value, 1);
        var validDate = new DateTime(validYear.Value, validMonth.Value, 1);

        return date.Date > validDate;
    }
}