using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageKeyDates;

public class KeyDatesViewModelValidator : AbstractValidator<KeyDatesViewModel>
{
    private const int MaxYear = 2040;

    public KeyDatesViewModelValidator()
    {
        #region StartDate

        RuleFor(x => x.StartDateMonth)
            .NotNull()
            .WithMessage("Please provide a start month")
            .NotEmpty()
            .WithMessage("Please provide a start month")
            .InclusiveBetween(1, 12)
            .WithMessage("Please provide a start month");

        RuleFor(x => x.StartDateYear)
            .NotNull()
            .WithMessage("Please provide a start year")
            .NotEmpty()
            .WithMessage("Please provide a start year")
            .LessThanOrEqualTo(MaxYear)
            .WithMessage($"Please provide a start year no later than {MaxYear}")
            .DependentRules(() =>
            {
                RuleFor(x => new { x.StartDateMonth, x.StartDateYear })
                    .Must(x => BeInTheFuture(x.StartDateMonth, x.StartDateYear))
                    .WithName("StartDateMonth")
                    .WithMessage($"The start date must be after {DateTime.Today.AddMonths(-1):MM/yyyy}")
                    .When(x => x.StartDateMonth.HasValue && x.StartDateYear.HasValue);
            });

        #endregion

        #region UnsafeCladdingRemovalDate

        RuleFor(x => x.UnsafeCladdingRemovalDateMonth)
            .NotNull()
            .WithMessage("Please provide a removal month")
            .NotEmpty()
            .WithMessage("Please provide a removal month")
            .InclusiveBetween(1, 12)
            .WithMessage("Please provide a removal month")
            .When(x => x.UnsafeCladdingRemovalDateYear.HasValue || x.IsCladdingBeingRemoved);

        RuleFor(x => x.UnsafeCladdingRemovalDateYear)
            .NotNull()
            .WithMessage("Please provide a removal year")
            .NotEmpty()
            .WithMessage("Please provide a removal year")
            .LessThanOrEqualTo(MaxYear)
            .WithMessage($"Please provide a removal year no later than {MaxYear}")
            .When(x => x.UnsafeCladdingRemovalDateMonth.HasValue || x.IsCladdingBeingRemoved)
            .DependentRules(() =>
            {
                RuleFor(x => x)
                    .Must(x => BeAfter(x.UnsafeCladdingRemovalDateMonth, x.UnsafeCladdingRemovalDateYear,
                        x.StartDateMonth, x.StartDateYear))
                    .WithName("UnsafeCladdingRemovalDateMonth")
                    .WithMessage(x => $"The removal date must be after {x.StartDateMonth}/{x.StartDateYear}")
                    .When(x =>
                        x.StartDateMonth.HasValue && x.StartDateYear.HasValue &&
                        x.UnsafeCladdingRemovalDateMonth.HasValue && x.UnsafeCladdingRemovalDateYear.HasValue);
            });


        #endregion

        #region ExpectedDateForCompletion

        RuleFor(x => x.ExpectedDateForCompletionMonth)
            .NotNull()
            .WithMessage("Please provide a completion month")
            .InclusiveBetween(1, 12)
            .WithMessage("Please provide a completion month");

        RuleFor(x => x.ExpectedDateForCompletionYear)
            .NotNull()
            .WithMessage("Please provide a completion year")
            .NotEmpty()
            .WithMessage("Please provide a completion year")
            .LessThanOrEqualTo(MaxYear)
            .WithMessage($"Please provide a completion year no later than {MaxYear}")
            .DependentRules(() =>
            {
                RuleFor(x => x)
                    .Must(x => BeAfter(x.ExpectedDateForCompletionMonth, x.ExpectedDateForCompletionYear,
                        x.StartDateMonth, x.StartDateYear))
                    .WithName("ExpectedDateForCompletionMonth")
                    .WithMessage(x => $"The completion date must be  after {x.StartDateMonth}/{x.StartDateYear}")
                    .When(x => x.StartDateMonth.HasValue && x.StartDateYear.HasValue);
            });

        #endregion
    }

    private bool BeInTheFuture(int? month, int? year)
    {
        if (month is null or < 1 or > 12) return false;
        if (year is null or < 1 or > MaxYear) return false;

        var date = new DateTime(year.Value, month.Value, 1);
        var validDate = (new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)).AddMonths(-1);

        return date.Date > validDate;
    }

    private bool BeAfter(int? actualMonth, int? actualYear, int? validMonth, int? validYear)
    {
        if (actualMonth is null or < 1 or > 12) return false;
        if (actualYear is null or < 1 or > MaxYear) return false;
        if (validMonth is null or < 1 or > 12) return false;
        if (validYear is null or < 1 or > MaxYear) return false;

        var date = new DateTime(actualYear.Value, actualMonth.Value, 1);
        var validDate = new DateTime(validYear.Value, validMonth.Value, 1);

        return date.Date > validDate;
    }
}