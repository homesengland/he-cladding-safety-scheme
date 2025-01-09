using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting
{
    public class BuildingControlDetailsViewModelValidator : AbstractValidator<BuildingControlDetailsViewModel>
    {
        public BuildingControlDetailsViewModelValidator()
        {
            var currentYear = DateTime.Now.Year;

            RuleFor(x => x.ForecastDateMonth)
                .NotEmpty()
                .When(x=> x.ForecastDateYear.HasValue, ApplyConditionTo.CurrentValidator)
                .WithMessage("Please provide a valid month for Forecast Date")
                .InclusiveBetween(1, 12)
                .When(x => x.ForecastDateMonth.HasValue, ApplyConditionTo.CurrentValidator)
                .WithMessage("Please provide a valid month for Forecast Date");

            RuleFor(x => x.ForecastDateYear)
                .NotEmpty()
                .When(x => x.ForecastDateMonth.HasValue, ApplyConditionTo.CurrentValidator)
                .WithMessage("Please provide a valid year for Forecast Date");

            RuleFor(x => x.ActualDateMonth)
                .NotEmpty()
                .When(x => x.ActualDateYear.HasValue, ApplyConditionTo.CurrentValidator)
                .WithMessage("Please provide a valid month for Actual Submission Date")
                .InclusiveBetween(1, 12)
                .When(x => x.ActualDateMonth.HasValue, ApplyConditionTo.CurrentValidator)
                .WithMessage("Please provide a valid month for Actual Submission Date");

            RuleFor(x => x.ActualDateYear)
                .NotEmpty()
                .When(x => x.ActualDateMonth.HasValue, ApplyConditionTo.CurrentValidator)
                .WithMessage("Please provide a valid year for Actual Submission Date")
                .LessThanOrEqualTo(currentYear)
                .When(x => x.ActualDateYear.HasValue, ApplyConditionTo.CurrentValidator)
                .WithMessage($"Actual Submission Date cannot be in the future");

            RuleFor(x => new { x.ActualDateMonth, x.ActualDateYear })
                .Must(x => BeInThePast(x.ActualDateMonth, x.ActualDateYear))
                .When(x => x.ActualDateMonth.HasValue && x.ActualDateYear.HasValue)
                .WithName("ActualDateYear")
                .WithMessage("Actual Submission Date cannot be in the future");

            RuleFor(x => x.ValidationDateMonth)
                .NotEmpty()
                .When(x => x.ValidationDateYear.HasValue, ApplyConditionTo.CurrentValidator)
                .WithMessage("Please provide a valid month for Validation Date")
                .InclusiveBetween(1, 12)
                .When(x => x.ValidationDateMonth.HasValue, ApplyConditionTo.CurrentValidator)
                .WithMessage("Please provide a valid month for Validation Date");

            RuleFor(x => x.ValidationDateYear)
                .NotEmpty()
                .When(x => x.ValidationDateMonth.HasValue, ApplyConditionTo.CurrentValidator)
                .WithMessage("Please provide a valid year for Validation Date")
                .LessThanOrEqualTo(currentYear)
                .When(x => x.ValidationDateYear.HasValue, ApplyConditionTo.CurrentValidator)
                .WithMessage($"Validation Date cannot be in the future");

            RuleFor(x => new { x.ValidationDateMonth, x.ValidationDateYear })
                .Must(x => BeInThePast(x.ValidationDateMonth, x.ValidationDateYear))
                .When(x => x.ValidationDateMonth.HasValue && x.ValidationDateYear.HasValue)
                .WithName("ValidationDateYear")
                .WithMessage("Validation Date cannot be in the future");

            RuleFor(x => x.DecisionDateMonth)
                .NotEmpty()
                .When(x => x.DecisionDateYear.HasValue, ApplyConditionTo.CurrentValidator)
                .WithMessage("Please provide a valid month for Decision Date")
                .InclusiveBetween(1, 12)
                .When(x => x.DecisionDateMonth.HasValue, ApplyConditionTo.CurrentValidator)
                .WithMessage("Please provide a valid month for Decision Date");

            RuleFor(x => x.DecisionDateYear)
                .NotEmpty()
                .When(x => x.DecisionDateMonth.HasValue, ApplyConditionTo.CurrentValidator)
                .WithMessage("Please provide a valid year for Decision Date")
                .LessThanOrEqualTo(currentYear)
                .When(x => x.DecisionDateYear.HasValue, ApplyConditionTo.CurrentValidator)
                .WithMessage($"Decision Date cannot be in the future");

            RuleFor(x => new { x.DecisionDateMonth, x.DecisionDateYear })
                .Must(x => BeInThePast(x.DecisionDateMonth, x.DecisionDateYear))
                .When(x => x.DecisionDateMonth.HasValue && x.DecisionDateYear.HasValue)
                .WithName("DecisionDateYear")
                .WithMessage("Decision Date cannot be in the future");
        }

        private bool BeInThePast(int? month, int? year)
        {
            var currentYear = DateTime.Now.Year;

            if (month is null or < 1 or > 12) return false;
            if (year is null || year < 1 || year > currentYear) return false;

            var date = new DateTime(year.Value, month.Value, 1);

            return date.Date <= DateTime.Today;
        }
    }
}
