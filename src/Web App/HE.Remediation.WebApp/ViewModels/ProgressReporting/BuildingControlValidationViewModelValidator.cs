using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class BuildingControlValidationViewModelValidator : AbstractValidator<BuildingControlValidationViewModel>
{
    public BuildingControlValidationViewModelValidator()
    {
        RuleFor(x => x.ValidationDateMonth)
            .NotNull()
            .When(x => x.ValidationDateYear.HasValue, ApplyConditionTo.CurrentValidator)
            .WithMessage("Enter a month")
            .InclusiveBetween(1, 12)
            .WithMessage("Enter a valid month");

        RuleFor(x => x.ValidationDateYear)
            .NotNull()
            .When(x => x.ValidationDateMonth.HasValue, ApplyConditionTo.CurrentValidator)
            .WithMessage("Enter a year")
            .InclusiveBetween(2000, 3000)
            .WithMessage("Enter a valid year");

        RuleFor(x => x.ValidationDate)
            .Must(BeInThePast)
            .When(x => x.ValidationDate.HasValue, ApplyConditionTo.CurrentValidator)
            .WithMessage("Validation date must be in the past")
            .OverridePropertyName(x => x.ValidationDateYear);

        RuleFor(x => x.ValidationInformation)
            .MaximumLength(1000)
            .WithMessage("The maximum character limit of 1000 has been exceeded");
    }

    private bool BeInThePast(DateTime? date)
    {
        var today = DateTime.Today;
        return !date.HasValue || ((date.Value.Month <= today.Month && date.Value.Year == today.Year) || date.Value.Year < today.Year);
    }
}