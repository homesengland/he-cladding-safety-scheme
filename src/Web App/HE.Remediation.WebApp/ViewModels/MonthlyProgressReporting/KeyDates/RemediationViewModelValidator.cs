using FluentValidation;
using static HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates.KeyDatesValidatorHelper;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;

public class RemediationViewModelValidator : AbstractValidator<RemediationViewModel>
{
    public RemediationViewModelValidator()
    {
        #region Expected Remediation Start

        RuleFor(x => x.FullCompletionOfWorksMonthYearInput.Month)
            .Must(m => IsNullOrNumber(m) && IsRange(m, 1, 12))
            .WithMessage("Remediation Start month - invalid");

        RuleFor(x => x.FullCompletionOfWorksMonthYearInput.Year)
            .Must(m => IsNullOrNumber(m) && IsRange(m, 2000, 3000))
            .WithMessage("Remediation Start year - invalid");

        RuleFor(x => x.FullCompletionOfWorksMonthYearInput)
            .Must(m => IsValidCombination(m?.Month, m?.Year))
            .WithMessage($"Remediation Start - invalid");

        // required
        RuleFor(x => x.FullCompletionOfWorksMonthYearInput)
            .Must(m => m.ToDateTime() != null)
                .WithMessage("Remediation Start is required");

        RuleFor(x => x.FullCompletionOfWorksMonthYearInput)
            .Must(m => BeInFuture(m?.ToDateTime()))
            .When(m => m.PreviousFullCompletionOfWorksDate != m.FullCompletionOfWorksMonthYearInput.ToDateTime())
            .WithMessage($"Remediation Start - Date must be in the future");

        // if previous ...
        When(x => x.PreviousFullCompletionOfWorksDate.HasValue, () =>
        {
            // ... required
            RuleFor(x => x.FullCompletionOfWorksMonthYearInput)
                 .Must(x => x.ToDateTime() != null)
                    .WithMessage("Remediation Start was supplied previously so must be kept");
        });

        #endregion

        #region Expected Practical Completion 

        RuleFor(x => x.PracticalCompletionMonthYearInput.Month)
           .Must(m => IsNullOrNumber(m) && IsRange(m, 1, 12))
           .WithMessage("Practical Completion month - invalid");

        RuleFor(x => x.PracticalCompletionMonthYearInput.Year)
            .Must(m => IsNullOrNumber(m) && IsRange(m, 2000, 3000))
            .WithMessage("Practical Completion year - invalid");

        RuleFor(x => x.PracticalCompletionMonthYearInput)
            .Must(m => IsValidCombination(m?.Month, m?.Year))
            .WithMessage($"Practical Completion - invalid");

        // required
        RuleFor(x => x.PracticalCompletionMonthYearInput)
            .Must(m => m.ToDateTime() != null)
                .WithMessage("Practical Completion is required");

        RuleFor(x => x.PracticalCompletionMonthYearInput)
            .Must(m => BeInFuture(m?.ToDateTime()))
            .When(m => m.PreviousPracticalCompletionDate != m.PracticalCompletionMonthYearInput.ToDateTime())
            .WithMessage($"Practical Completion - Date must be in the future");

        When(x => x.FullCompletionOfWorksDate.HasValue && x.PracticalCompletionDate.HasValue, () =>
        {
            RuleFor(x => x)
                .Must(m => m.PracticalCompletionDate >= m.FullCompletionOfWorksDate)
                .WithMessage("Practical Completion cannot be before Remediation Start")
            .OverridePropertyName(x => x.PracticalCompletionMonthYearInput);
        });

        // if previous ...
        When(x => x.PreviousPracticalCompletionDate.HasValue, () =>
        {
            // ... required
            RuleFor(x => x.PracticalCompletionMonthYearInput)
                 .Must(x => x.ToDateTime() != null)
                    .WithMessage("Practical Completion was supplied previously so must be kept");
        });

        #endregion
    }

   
}
