using FluentValidation;
using static HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates.KeyDatesValidatorHelper;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;

public class WorksPlanningViewModelValidator : AbstractValidator<WorksPlanningViewModel>
{
    public WorksPlanningViewModelValidator()
    {
        #region Expected Tender Date

        RuleFor(x => x.ExpectedTenderMonth)
            .InclusiveBetween(1, 12)
            .WithMessage("Expected Tender Date - invalid month");

        RuleFor(x => x.ExpectedTenderYear)
            .InclusiveBetween(2000, 3000)
            .WithMessage("Expected Tender Date - invalid year");

        // required, if no ActualTenderDate
        When(x => !x.ActualTenderDate.HasValue, () =>
        {
            RuleFor(x => x.ExpectedTenderMonth)
             .NotNull()
                 .WithMessage("Expected Tender Date - month required");

            RuleFor(x => x.ExpectedTenderYear)
                .NotNull()
                    .WithMessage("Expected Tender Date - year required");

            RuleFor(x => x.ExpectedTenderDate)
               .Must(BeInFuture)
               .When(x => x.PreviousExpectedTenderDate != x.ExpectedTenderDate)
                   .WithMessage(x =>
                   {
                       return "Expected Tender Date must be in the future";
                   })
               .OverridePropertyName(x => x.ExpectedTenderYear);
        });

        // if previous ...
        When(x => x.PreviousExpectedTenderDate.HasValue, () =>
        {
            // ... required
            RuleFor(x => x.ExpectedTenderDate)
                 .Must(x => x.HasValue)
                    .WithMessage("Actual Tender Date was supplied previously so must be kept")
            .OverridePropertyName(x => x.ExpectedTenderYear);
        });

        #endregion

        #region Actual Tender Date

        RuleFor(x => x.ActualTenderMonth)
            .NotNull()
                .When(x => x.ActualTenderYear.HasValue, ApplyConditionTo.AllValidators)
            .WithMessage("Actual Tender Date month - invalid")
                .InclusiveBetween(1, 12)
                .WithMessage("Actual Tender Date month - invalid");

        RuleFor(x => x.ActualTenderYear)
            .NotNull()
                .When(x => x.ActualTenderMonth.HasValue, ApplyConditionTo.AllValidators)
                .WithMessage("Actual Tender Date year - invalid")
            .InclusiveBetween(2000, 3000)
                .WithMessage("Actual Tender Date year - invalid");

        When(x => x.ActualTenderDate.HasValue, () => {

            // must be in past
            RuleFor(x => x.ActualTenderDate)
                .Must(m => BeInPast(m))
                   .WithMessage("Actual Tender Date cannot be in the future")
            .OverridePropertyName(x => x.ExpectedTenderYear);

            When(x => x.ExpectedTenderDate.HasValue, () =>
            {
                // must be after expected
                RuleFor(x => x)
                    .Must(m => m.ActualTenderDate >= m.ExpectedTenderDate)
                        .WithMessage("Actual Tender Date cannot be before Expected Tender Date")
                .OverridePropertyName(x => x.ActualTenderYear);
            });
        });

        // if previous ...
        When(x => x.PreviousActualTenderDate.HasValue, () =>
        {
            // ... required
            RuleFor(x => x.ActualTenderDate)
                 .Must(x => x.HasValue)
                    .WithMessage("Actual Tender Date was supplied previously so must be kept")
            .OverridePropertyName(x => x.ExpectedTenderYear);
        });

        #endregion

        #region Expected Lead Contractor Appointment Date

        When(x => !x.ActualLeadContractorAppointmentDate.HasValue, () =>
        {
            RuleFor(x => x.ExpectedLeadContractorAppointmentMonth)
             .NotNull()
                 .WithMessage("Expected Lead Contractor Appointment month - required")
             .InclusiveBetween(1, 12)
                 .WithMessage("Expected Lead Contractor Appointment month - invalid");

            RuleFor(x => x.ExpectedLeadContractorAppointmentYear)
                .NotNull()
                    .WithMessage("Expected Lead Contractor Appointment year - required")
                .InclusiveBetween(2000, 3000)
                    .WithMessage("Expected Lead Contractor Appointment year - invalid");
        });

        RuleFor(x => x.ExpectedLeadContractorAppointmentDate)
            .Must(BeInFuture)
            .When(x => x.PreviousExpectedLeadContractorAppointmentDate != x.ExpectedLeadContractorAppointmentDate)
                .WithMessage(x =>
                {
                    return "Expected Lead Contractor Appointment must be in the future";
                })
            .OverridePropertyName(x => x.ExpectedLeadContractorAppointmentYear);

        // if previous ...
        When(x => x.PreviousExpectedLeadContractorAppointmentDate.HasValue, () =>
        {
            // ... required
            RuleFor(x => x.ExpectedLeadContractorAppointmentDate)
                 .Must(x => x.HasValue)
                    .WithMessage("Expected Lead Contractor Appointment was supplied previously so must be kept")
            .OverridePropertyName(x => x.ExpectedLeadContractorAppointmentYear);
        });

        #endregion

        #region Actual Lead Contractor Appointment Date

        RuleFor(x => x.ActualLeadContractorAppointmentMonth)
            .NotNull()
                .When(x => x.ActualLeadContractorAppointmentYear.HasValue, ApplyConditionTo.AllValidators)
                .WithMessage("Actual Lead Contractor Appointment month - required")
            .InclusiveBetween(1, 12)
                .WithMessage("Actual Lead Contractor Appointment month - invalid");

        RuleFor(x => x.ActualLeadContractorAppointmentYear)
            .NotNull()
                .When(x => x.ActualLeadContractorAppointmentMonth.HasValue, ApplyConditionTo.AllValidators)
                 .WithMessage("Actual Lead Contractor Appointment year - required")
            .InclusiveBetween(2000, 3000)
                .WithMessage("Actual Lead Contractor Appointment year - invalid");

        When(x => x.ActualLeadContractorAppointmentDate != null, () =>
        {
            // must be in past
            RuleFor(x => x.ActualLeadContractorAppointmentDate)
                .Must(m => BeInPast(m))
                   .WithMessage("Actual Lead Contractor Appointment cannot be in the future")
            .OverridePropertyName(x => x.ActualLeadContractorAppointmentYear);

            When(x => x.ExpectedLeadContractorAppointmentDate.HasValue, () =>
            {
                // must be after expected
                RuleFor(x => x)
                    .Must(m => m.ActualLeadContractorAppointmentDate >= m.ExpectedLeadContractorAppointmentDate)
                        .WithMessage("Actual Lead Contractor Appointment cannot be before Expected Lead Contractor Appointment")
                .OverridePropertyName(x => x.ActualLeadContractorAppointmentYear);
            });
        });

        // if previous ...
        When(x => x.PreviousActualLeadContractorAppointmentDate.HasValue, () =>
        {
            // ... required
            RuleFor(x => x.ActualLeadContractorAppointmentDate)
                 .Must(x => x.HasValue)
                    .WithMessage("Actual Lead Contractor Appointment was supplied previously so must be kept")
            .OverridePropertyName(x => x.ActualLeadContractorAppointmentYear);
        });

        #endregion

        #region Expected Works Package Submission Date

        RuleFor(x => x.ExpectedWorksPackageSubmissionMonth)
            .NotNull()
            .WithMessage("Expected Works Package Submission month - required")
            .InclusiveBetween(1, 12)
            .WithMessage("Expected Works Package Submission month - invalid");

        RuleFor(x => x.ExpectedWorksPackageSubmissionYear)
            .NotNull()
            .WithMessage("Expected Works Package Submission year - required")
            .InclusiveBetween(2000, 3000)
            .WithMessage("Expected Works Package Submission year - invalid");

        RuleFor(x => x.ExpectedWorksPackageSubmissionDate)
            .Must(BeInFuture)
            .When(x => x.PreviousExpectedWorksPackageSubmissionDate != x.ExpectedWorksPackageSubmissionDate)
            .WithMessage(x =>
            {
                return "Expected Works Package Submission must be in the future";
            })
            .OverridePropertyName(x => x.ExpectedWorksPackageSubmissionYear);

        #endregion

    }
}