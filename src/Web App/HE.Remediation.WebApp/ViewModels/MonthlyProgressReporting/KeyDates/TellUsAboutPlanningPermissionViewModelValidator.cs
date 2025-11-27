using FluentValidation;
using static HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates.KeyDatesValidatorHelper;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;

public class TellUsAboutPlanningPermissionViewModelValidator : AbstractValidator<TellUsAboutPlanningPermissionViewModel>
{
    public TellUsAboutPlanningPermissionViewModelValidator()
    {
        #region Planning Permission Submitted

        RuleFor(x => x.PlanningPermissionDateSubmittedMonth)
            .InclusiveBetween(1, 12)
                .WithMessage("Date Submitted - invalid month");

        RuleFor(x => x.PlanningPermissionDateSubmittedYear)
            .InclusiveBetween(2000, 3000)
                .WithMessage("Date Submitted - invalid year");

        // if not approved, then is required
        When(x => !x.PlanningPermissionDateApproved.HasValue, () =>
        {
            RuleFor(x => x.PlanningPermissionDateSubmittedMonth)
            .NotNull()
                .WithMessage("Date Submitted - month required");

            RuleFor(x => x.PlanningPermissionDateSubmittedYear)
                .NotNull()
                    .WithMessage("Date Submitted - year required");
        });

        When(x => x.PlanningPermissionDateSubmitted.HasValue, () =>
        {
            // must be in past
            RuleFor(x => x.PlanningPermissionDateSubmitted)
                .Must(BeInPast)
                    .WithMessage("Date Submitted cannot be in the future")
                .OverridePropertyName(x => x.PlanningPermissionDateSubmittedYear);
        });

        // if previous ...
        When(x => x.PreviousPlanningPermissionDateSubmitted.HasValue, () =>
        {
            // ... required
            RuleFor(x => x.PlanningPermissionDateSubmitted)
                .NotNull()
                .WithMessage("Date Submitted was supplied previously so must be kept")
                .OverridePropertyName(x => x.PlanningPermissionDateSubmittedYear);
        });

        #endregion

        #region Planning Permission Approved

        RuleFor(x => x.PlanningPermissionDateApprovedMonth)
            .NotNull()
                .When(x => x.PlanningPermissionDateApprovedYear.HasValue, ApplyConditionTo.AllValidators)
                .WithMessage("Date Approved - month required")
            .InclusiveBetween(1, 12)
                .WithMessage("Date Approved - invalid month");

        RuleFor(x => x.PlanningPermissionDateApprovedYear)
            .NotNull()
            .When(x => x.PlanningPermissionDateApprovedMonth.HasValue, ApplyConditionTo.AllValidators)
            .WithMessage("Date Approved - year required")
            .InclusiveBetween(2000, 3000)
            .WithMessage("Date Approved - invalid year");

        When(x => x.PlanningPermissionDateSubmitted.HasValue && x.PlanningPermissionDateApproved.HasValue, () =>
        {
            RuleFor(x => x)
             .Must(x => x.PlanningPermissionDateApproved >= x.PlanningPermissionDateSubmitted)
                 .WithMessage("Date Approved cannot be before the Date Submitted")
             .OverridePropertyName(x => x.PlanningPermissionDateApprovedYear);
        });

        RuleFor(x => x)
            .Must(x => BeInPast(x.PlanningPermissionDateApproved))
                .WithMessage("Date Approved cannot be in the future")
            .OverridePropertyName(x => x.PlanningPermissionDateApprovedYear);

        // if previous ...
        When(x => x.PreviousPlanningPermissionDateApproved.HasValue, () =>
        {
            // ... required
            RuleFor(x => x.PlanningPermissionDateApproved)
                .NotNull()
                .WithMessage("Date Approved was supplied previously so must be kept")
                .OverridePropertyName(x => x.PlanningPermissionDateApprovedYear);
        });

        #endregion
    }
}
