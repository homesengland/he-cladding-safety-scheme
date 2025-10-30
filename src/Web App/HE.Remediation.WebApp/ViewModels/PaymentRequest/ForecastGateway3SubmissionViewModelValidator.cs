using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest
{
    public class ForecastGateway3SubmissionViewModelValidator : AbstractValidator<ForecastGateway3SubmissionViewModel>
    {
        private const int MaxYear = 2040;

        public ForecastGateway3SubmissionViewModelValidator()
        {
            // If month is provided, it must be valid (1..12)
            RuleFor(x => x.ExpectedSubmissionDateMonth)
                .InclusiveBetween(1, 12)
                .WithMessage("Please enter a valid month")
                .When(x => x.ExpectedSubmissionDateMonth.HasValue);

            // If year is provided, it must be valid (1..MaxYear)
            RuleFor(x => x.ExpectedSubmissionDateYear)
                .GreaterThan(0).WithMessage("Please enter a valid year")
                .LessThanOrEqualTo(MaxYear).WithMessage($"Please enter a year no later than {MaxYear}")
                .When(x => x.ExpectedSubmissionDateYear.HasValue);

            // If only one of month/year is provided, require the other
            RuleFor(x => x).Custom((model, ctx) =>
            {
                if (model.ExpectedSubmissionDateMonth.HasValue && !model.ExpectedSubmissionDateYear.HasValue)
                {
                    ctx.AddFailure("ExpectedSubmissionDateYear", "Please enter the expected submission year");
                }

                if (model.ExpectedSubmissionDateYear.HasValue && !model.ExpectedSubmissionDateMonth.HasValue)
                {
                    ctx.AddFailure("ExpectedSubmissionDateMonth", "Please enter the expected submission month");
                }
            });

            // If both provided (and individually valid), the date must be in the future (strictly after current month)
            When(x => x.ExpectedSubmissionDateMonth.HasValue && x.ExpectedSubmissionDateYear.HasValue
                &&  x.ExpectedSubmissionDateMonth is >= 1 and <= 12
                &&  x.ExpectedSubmissionDateYear is >= 1 and <= MaxYear,
                () =>
            {
                RuleFor(x => x)
                    .Must(x => IsFutureMonthYear(x.ExpectedSubmissionDateMonth!.Value, x.ExpectedSubmissionDateYear!.Value))
                    .WithMessage("Expected submission date must be in the future")
                    .WithName("ExpectedSubmissionDateMonth");
            });
        }

        private bool IsFutureMonthYear(int month, int year)
        {
            if (month < 1 || month > 12) return false;
            if (year < 1 || year > MaxYear) return false;

            var candidate = new DateTime(year, month, 1);
            var startOfCurrentMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            return candidate > startOfCurrentMonth;
        }
    }
}
