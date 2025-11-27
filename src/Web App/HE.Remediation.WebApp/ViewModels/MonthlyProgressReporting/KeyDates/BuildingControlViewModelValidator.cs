using FluentValidation;
using static HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates.KeyDatesValidatorHelper;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;

public class BuildingControlViewModelValidator : AbstractValidator<BuildingControlViewModel>
{
    public BuildingControlViewModelValidator()
    {
        #region ExpectedApplicationDate

        RuleFor(x => x.BuildingControlExpectedApplicationDateMonthYearInput.Month)
            .Must(m => IsNullOrNumber(m) && IsRange(m, 1, 12))
            .WithMessage("Expected Application month - invalid");

        RuleFor(x => x.BuildingControlExpectedApplicationDateMonthYearInput.Year)
            .Must(m => IsNullOrNumber(m) && IsRange(m, 2000, 3000))
            .WithMessage("Expected Application year - invalid");

        RuleFor(x => x.BuildingControlExpectedApplicationDateMonthYearInput)
            .Must(m => IsValidCombination(m?.Month, m?.Year))
            .WithMessage("Expected Application - invalid");

        // if no Actual or Decision date, this is required
        RuleFor(x => x.BuildingControlExpectedApplicationDateMonthYearInput)
            .Must(m => m.ToDateTime() != null)
                .When(m => m.BuildingControlActualApplicationDate == null && m.BuildingControlDecisionDate == null)
                .WithMessage("Expected Application is required");

        // Must be in future
        RuleFor(x => x.BuildingControlExpectedApplicationDateMonthYearInput)
            .Must(m => BeInFuture(m.ToDateTime()))
                .When(m => m.PreviousBuildingControlExpectedApplicationDate != m.BuildingControlExpectedApplicationDateMonthYearInput.ToDateTime())
                .WithMessage("Expected Application must be in the future");

        // if previous ...
        When(x => x.PreviousBuildingControlExpectedApplicationDate.HasValue, () =>
        {
            // ... required
            RuleFor(x => x.BuildingControlExpectedApplicationDateMonthYearInput)
                 .Must(x => x.ToDateTime() != null)
                    .WithMessage("Expected Application was supplied previously so must be kept");
        });

        #endregion

        #region ActualApplicationDate

        RuleFor(x => x.BuildingControlActualApplicationDateMonthYearInput.Month)
            .Must(m => IsNullOrNumber(m) && IsRange(m, 1, 12))
            .WithMessage("Actual Application month - invalid");

        RuleFor(x => x.BuildingControlActualApplicationDateMonthYearInput.Year)
            .Must(m => IsNullOrNumber(m) && IsRange(m, 2000, 3000))
            .WithMessage("Actual Application year - invalid");

        RuleFor(x => x.BuildingControlActualApplicationDateMonthYearInput)
            .Must(m => IsValidCombination(m?.Month, m?.Year))
            .WithMessage("Actual Application - invalid");

        When(x => x.BuildingControlActualApplicationDate != null, () => {

            // must be in past
            RuleFor(x => x.BuildingControlActualApplicationDateMonthYearInput)
                .Must(m => BeInPast(m.ToDateTime()))
                   .WithMessage("Actual Application cannot be in the future");

            // must be before expected
            RuleFor(x => x)
                .Must(m => m.BuildingControlActualApplicationDate >= m.BuildingControlExpectedApplicationDate)
                .When(m => m.BuildingControlExpectedApplicationDate.HasValue)
                   .WithMessage("Actual Application cannot be before Expected Application Date")
               .OverridePropertyName(x => x.BuildingControlActualApplicationDateMonthYearInput);
        });

        // if previous ...
        When(x => x.PreviousBuildingControlActualApplicationDate.HasValue, () =>
        {
            // ... required
            RuleFor(x => x.BuildingControlActualApplicationDateMonthYearInput)
                 .Must(x => x.ToDateTime() != null)
                    .WithMessage("Actual Application was supplied previously so must be kept");
        });

        #endregion

        #region ValidationDate

        RuleFor(x => x.BuildingControlValidationDateMonthYearInput.Month)
            .Must(m => IsNullOrNumber(m) && IsRange(m, 1, 12))
            .WithMessage("Validation Date month - invalid");

        RuleFor(x => x.BuildingControlValidationDateMonthYearInput.Year)
            .Must(m => IsNullOrNumber(m) && IsRange(m, 2000, 3000))
            .WithMessage("Validation Date year - invalid");

        RuleFor(x => x.BuildingControlValidationDateMonthYearInput)
            .Must(m => IsValidCombination(m?.Month, m?.Year))
            .WithMessage("Validation Date - invalid");

        RuleFor(x => x)
            .Must(m => BeInPast(m.BuildingControlValidationDate))
                .WithMessage("Validation Date cannot be in the future")
            .OverridePropertyName(x => x.BuildingControlValidationDateMonthYearInput);

        When(x => x.BuildingControlActualApplicationDate.HasValue && x.BuildingControlValidationDate.HasValue, () =>
        {
            RuleFor(x => x)
             .Must(m => m.BuildingControlValidationDate.Value >= m.BuildingControlActualApplicationDate.Value)
                 .WithMessage("Validation Date must be on or after Actual Application date")
                 .OverridePropertyName(x => x.BuildingControlValidationDateMonthYearInput);
        });

        // if previous ...
        When(x => x.PreviousBuildingControlValidationDate.HasValue, () =>
        {
            // ... required
            RuleFor(x => x.BuildingControlValidationDateMonthYearInput)
                 .Must(x => x.ToDateTime() != null)
                    .WithMessage("Validation Date was supplied previously so must be kept");
        });

        #endregion

        #region DecisionDate

        RuleFor(x => x.BuildingControlDecisionDateMonthYearInput.Month)
            .Must(m => IsNullOrNumber(m) && IsRange(m, 1, 12))
            .WithMessage("Decision Date month - invalid");

        RuleFor(x => x.BuildingControlDecisionDateMonthYearInput.Year)
            .Must(m => IsNullOrNumber(m) && IsRange(m, 2000, 3000))
            .WithMessage("Decision Date year - invalid");

        RuleFor(x => x.BuildingControlDecisionDateMonthYearInput)
            .Must(m => IsValidCombination(m?.Month, m?.Year))
            .WithMessage("Decision Date - invalid");

        RuleFor(x => x)
            .Must(m => BeInPast(m.BuildingControlDecisionDate))
                .WithMessage("Decision Date cannot be in the future")
            .OverridePropertyName(x => x.BuildingControlDecisionDateMonthYearInput);

        // if previous ...
        When(x => x.PreviousBuildingControlDecisionDate.HasValue, () =>
        {
            // ... required
            RuleFor(x => x.BuildingControlDecisionDateMonthYearInput)
                 .Must(x => x.ToDateTime() != null)
                    .WithMessage("Decision Date was supplied previously so must be kept");
        });

        #endregion

        RuleFor(x => x.Gateway2Reference)
            .Must(r => string.IsNullOrWhiteSpace(r) || r.Length == 12)
            .WithMessage("Gateway 2 application reference must be 12 characters");

        RuleFor(x => x.BuildingControlDecisionType)
            .NotNull()
            .When(x => x.BuildingControlDecisionDate.HasValue)
            .WithMessage("Provide a Building Control Decision");
    }
}
