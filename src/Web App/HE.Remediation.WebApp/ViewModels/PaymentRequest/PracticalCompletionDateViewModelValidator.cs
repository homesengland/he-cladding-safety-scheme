using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest
{
    public class PracticalCompletionDateViewModelValidator : AbstractValidator<PracticalCompletionDateViewModel>
    {
        private const int MaxYear = 2040;

        public PracticalCompletionDateViewModelValidator()
        {
            // MONTH: required
            RuleFor(x => x.ExpectedPracticalDateMonth)
                .NotNull().WithMessage("Please enter the expected practical completion month");

            // MONTH: range (only if provided)
            RuleFor(x => x.ExpectedPracticalDateMonth)
                .InclusiveBetween(1, 12).WithMessage("Please enter a valid month")
                .When(x => x.ExpectedPracticalDateMonth.HasValue);

            // YEAR: required
            RuleFor(x => x.ExpectedPracticalDateYear)
                .NotNull().WithMessage("Please enter the expected practical completion year");

            // YEAR: range (only if provided)
            RuleFor(x => x.ExpectedPracticalDateYear)
                .GreaterThan(0).WithMessage("Please enter a valid year")
                .LessThanOrEqualTo(MaxYear).WithMessage($"Please enter a year no later than {MaxYear}")
                .When(x => x.ExpectedPracticalDateYear.HasValue);

            // Combined future check: only when both present
            When(x => x.ExpectedPracticalDateMonth.HasValue &&
                    x.ExpectedPracticalDateYear.HasValue &&
                    x.ExpectedPracticalDateMonth is >= 1 and <= 12 &&
                    x.ExpectedPracticalDateYear is >= 1 and <= MaxYear, () =>
            {
                RuleFor(x => x)
                    .Must(x => IsFutureMonthYear(x.ExpectedPracticalDateMonth!.Value, x.ExpectedPracticalDateYear!.Value))
                    .WithMessage("Expected practical date completion must be in the future")
                    .OverridePropertyName("ExpectedPracticalDateMonth");
            });
        }

        private bool IsFutureMonthYear(int month, int year)
        {
            if (month < 1 || month > 12) return false;
            if (year < 1 || year > MaxYear) return false;

            var candidate = new DateTime(year, month, 1);
            var startOfCurrentMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            return candidate > startOfCurrentMonth; // use >= to allow the current month
        }

    }
}
