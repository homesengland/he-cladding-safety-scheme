using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class BuildingControlSubmissionViewModelValidator : AbstractValidator<BuildingControlSubmissionViewModel>
{
    public BuildingControlSubmissionViewModelValidator()
    {
        RuleFor(x => x.SubmissionDateMonth)
            .NotNull()
            .When(x => x.SubmissionDateYear.HasValue || !string.IsNullOrWhiteSpace(x.BuildingControlApplicationReference), ApplyConditionTo.CurrentValidator)
            .WithMessage("Enter a month")
            .InclusiveBetween(1, 12)
            .WithMessage("Enter a valid month");

        RuleFor(x => x.SubmissionDateYear)
            .NotNull()
            .When(x => x.SubmissionDateMonth.HasValue || !string.IsNullOrWhiteSpace(x.BuildingControlApplicationReference), ApplyConditionTo.CurrentValidator)
            .WithMessage("Enter a year")
            .InclusiveBetween(2000, 3000)
            .WithMessage("Enter a valid year");

        RuleFor(x => x.SubmissionDate)
            .Must(BeInThePast)
            .When(x => x.SubmissionDate.HasValue, ApplyConditionTo.CurrentValidator)
            .WithMessage("Actual submission date must be in the past")
            .OverridePropertyName(x => x.SubmissionDateYear);

        RuleFor(x => x.SubmissionInformation)
            .MaximumLength(1000)
            .WithMessage("The maximum character limit of 1000 has been exceeded");

        When(x => x.BuildingControlRequired == true && x.SubmissionDate.HasValue, () =>
        {
            RuleFor(x => x.BuildingControlApplicationReference)
                .NotEmpty()
                .WithMessage("Enter gateway 2 application reference")
                .Must(x => x?.Length == 12)
                .WithMessage("Gateway 2 application reference must be 12 characters");
        });
    }

    private bool BeInThePast(DateTime? date)
    {
        var today = DateTime.Today;
        return !date.HasValue || ((date.Value.Month <= today.Month && date.Value.Year == today.Year) || date.Value.Year < today.Year);
    }
}