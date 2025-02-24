using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class BuildingControlForecastViewModelValidator : AbstractValidator<BuildingControlForecastViewModel>
{
    public BuildingControlForecastViewModelValidator()
    {
        RuleFor(x => x.ForecastDateMonth)
            .NotNull()
            .When(x => x.ForecastDateYear.HasValue, ApplyConditionTo.CurrentValidator)
            .WithMessage("Enter a month")
            .InclusiveBetween(1, 12)
            .WithMessage("Enter a valid month");

        RuleFor(x => x.ForecastDateYear)
            .NotNull()
            .When(x => x.ForecastDateMonth.HasValue, ApplyConditionTo.CurrentValidator)
            .WithMessage("Enter a year")
            .InclusiveBetween(2000, 3000)
            .WithMessage("Enter a valid year");

        RuleFor(x => x.ForecastDate)
            .Must(BeInTheFuture)
            .When(x => x.ForecastDate.HasValue, ApplyConditionTo.CurrentValidator)
            .WithMessage("Forecast date must be in the future")
            .OverridePropertyName(x => x.ForecastDateYear);

        RuleFor(x => x.ForecastInformation)
            .MaximumLength(1000)
            .WithMessage("The maximum character limit of 1000 has been exceeded");
    }

    private bool BeInTheFuture(DateTime? date)
    {
        var today = DateTime.Today;
        return !date.HasValue || ((date.Value.Month >= today.Month && date.Value.Year == today.Year) || date.Value.Year > today.Year);
    }
}