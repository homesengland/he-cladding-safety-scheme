using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class BuildingControlDecisionViewModelValidator : AbstractValidator<BuildingControlDecisionViewModel>
{
    public BuildingControlDecisionViewModelValidator()
    {
        RuleFor(x => x.DecisionDateMonth)
            .NotNull()
            .When(x => x.DecisionDateYear.HasValue, ApplyConditionTo.CurrentValidator)
            .WithMessage("Enter a month")
            .InclusiveBetween(1, 12)
            .WithMessage("Enter a valid month");

        RuleFor(x => x.DecisionDateYear)
            .NotNull()
            .When(x => x.DecisionDateMonth.HasValue, ApplyConditionTo.CurrentValidator)
            .WithMessage("Enter a year")
            .InclusiveBetween(2000, 3000)
            .WithMessage("Enter a valid year");

        RuleFor(x => x.DecisionDate)
            .Must(BeInThePast)
            .When(x => x.DecisionDate.HasValue, ApplyConditionTo.CurrentValidator)
            .WithMessage("Decision date must be in the past")
            .OverridePropertyName(x => x.DecisionDateYear);

        RuleFor(x => x.DecisionInformation)
            .MaximumLength(1000)
            .WithMessage("The maximum character limit of 1000 has been exceeded");
    }

    private bool BeInThePast(DateTime? date)
    {
        var today = DateTime.Today;
        return !date.HasValue || ((date.Value.Month <= today.Month && date.Value.Year == today.Year) || date.Value.Year < today.Year);
    }
}