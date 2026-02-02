using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Services.StatusTransition;

public class StatusTransitionService : IStatusTransitionService
{
    private readonly IApplicationRepository _applicationRepository;

    public StatusTransitionService(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }
    #region status mapping
    //private static IDictionary<EApplicationStatus, IReadOnlyCollection<EApplicationStatus>> StatusTransitionMap = new Dictionary<EApplicationStatus, IReadOnlyCollection<EApplicationStatus>>
    //{
    //    { EApplicationStatus.ApplicationNotStarted, new [] { EApplicationStatus.ApplicationInProgress } },
    //    { EApplicationStatus.ApplicationInProgress, new [] { EApplicationStatus.ApplicationSubmitted, EApplicationStatus.ApplicationNotEligible } },
    //    { EApplicationStatus.ApplicationSubmitted, new [] { EApplicationStatus.ApplicationInReview } },
    //    { EApplicationStatus.ApplicationInReview, new [] { EApplicationStatus.ApplicationApproved, EApplicationStatus.ApplicationClosedReferToGla } },
    //    { EApplicationStatus.ApplicationApproved, new [] { EApplicationStatus.GfaIssued, EApplicationStatus.ProgressReporting } },
    //    { EApplicationStatus.GfaIssued, new [] { EApplicationStatus.GfaSubmitted } },
    //    { EApplicationStatus.GfaSubmitted, new [] { EApplicationStatus.GfaComplete } },
    //    { EApplicationStatus.GfaComplete, new [] { EApplicationStatus.WorksPackageInProgress } },
    //    { EApplicationStatus.ProgressReporting, new [] { EApplicationStatus.WorksPackageInProgress } },
    //    { EApplicationStatus.WorksPackageInProgress, new [] { EApplicationStatus.WorksPackageSubmitted } },
    //    { EApplicationStatus.WorksPackageSubmitted, new [] { EApplicationStatus.WorksPackageInReview } },
    //    { EApplicationStatus.WorksPackageInReview, new [] { EApplicationStatus.WorksPackageApproved, EApplicationStatus.WorksPackageRejected } },
    //    { EApplicationStatus.WorksPackageApproved, new [] { EApplicationStatus.WorksPackageDeedIssued } },
    //    { EApplicationStatus.WorksPackageDeedIssued, new [] { EApplicationStatus.WorksPackageDeedSigned } },
    //    { EApplicationStatus.WorksPackageDeedSigned, new [] { EApplicationStatus.WorksPackageDeedSubmitted } },
    //    { EApplicationStatus.WorksPackageDeedSubmitted, new [] { EApplicationStatus.WorksPackageDeedComplete } },
    //    { EApplicationStatus.WorksPackageDeedComplete, new [] { EApplicationStatus.ScheduleOfWorksInProgress } },
    //    { EApplicationStatus.ScheduleOfWorksInProgress, new [] { EApplicationStatus.ScheduleOfWorksSubmitted } },
    //    { EApplicationStatus.ScheduleOfWorksSubmitted, new [] { EApplicationStatus.ScheduleOfWorksInReview } },
    //    { EApplicationStatus.ScheduleOfWorksInReview, new [] { EApplicationStatus.ScheduleOfWorksApproved } },
    //    { EApplicationStatus.ScheduleOfWorksApproved, new [] { EApplicationStatus.PaymentRequestInProgress, EApplicationStatus.VariationInProgress } },
    //    { EApplicationStatus.PaymentRequestInProgress, new [] { EApplicationStatus.PaymentRequestSubmitted, EApplicationStatus.VariationInProgress } },
    //    { EApplicationStatus.PaymentRequestSubmitted, new [] { EApplicationStatus.PaymentRequestInReview, EApplicationStatus.VariationInProgress } },
    //    { EApplicationStatus.PaymentRequestInReview, new [] { EApplicationStatus.PaymentRequestApproved, EApplicationStatus.PaymentRequestRejected, EApplicationStatus.VariationInProgress } },
    //    { EApplicationStatus.PaymentRequestApproved, new [] { EApplicationStatus.PaymentRequestInProgress, EApplicationStatus.VariationInProgress, EApplicationStatus.ClosingReportInProgress } },
    //    { EApplicationStatus.PaymentRequestRejected, new [] { EApplicationStatus.PaymentRequestInProgress, EApplicationStatus.VariationInProgress } },
    //    { EApplicationStatus.VariationInProgress, new [] { EApplicationStatus.VariationSubmitted } },
    //    { EApplicationStatus.VariationSubmitted, new [] { EApplicationStatus.VariationInReview } },
    //    { EApplicationStatus.VariationInReview, new [] { EApplicationStatus.VariationApproved, EApplicationStatus.VariationRejected } },
    //    { EApplicationStatus.VariationApproved, new [] { EApplicationStatus.ScheduleOfWorksApproved, EApplicationStatus.PaymentRequestInProgress, EApplicationStatus.PaymentRequestSubmitted, EApplicationStatus.PaymentRequestInReview, EApplicationStatus.PaymentRequestApproved, EApplicationStatus.PaymentRequestRejected } },
    //    { EApplicationStatus.VariationRejected, new [] { EApplicationStatus.ScheduleOfWorksApproved, EApplicationStatus.PaymentRequestInProgress, EApplicationStatus.PaymentRequestSubmitted, EApplicationStatus.PaymentRequestInReview, EApplicationStatus.PaymentRequestApproved, EApplicationStatus.PaymentRequestRejected } },
    //    { EApplicationStatus.ClosingReportInProgress, new [] { EApplicationStatus.ClosingReportSubmitted } },
    //    { EApplicationStatus.ClosingReportSubmitted , new [] { EApplicationStatus.ClosingReportInReview } },
    //    { EApplicationStatus.ClosingReportInReview, new [] { EApplicationStatus.ClosingReportApproved, EApplicationStatus.ClosingReportRejected } },
    //    { EApplicationStatus.ClosingReportApproved, new [] { EApplicationStatus.ApplicationClosed } },
    //    { EApplicationStatus.ClosingReportRejected, new [] { EApplicationStatus.ApplicationClosed } },
    //    { EApplicationStatus.ApplicationClosed, Array.Empty<EApplicationStatus>() },
    //    { EApplicationStatus.ApplicationClosedReferToGla, Array.Empty<EApplicationStatus>() },
    //    { EApplicationStatus.ApplicationNotEligible, Array.Empty<EApplicationStatus>() }
    //};

    //private static IDictionary<EApplicationInternalStatus, IReadOnlyCollection<EApplicationInternalStatus>> InternalStatusTransitionMap = new Dictionary<EApplicationInternalStatus, IReadOnlyCollection<EApplicationInternalStatus>>
    //{
    //    { EApplicationInternalStatus.NotStarted, new [] { EApplicationInternalStatus.ApplicationStarted } },
    //    { EApplicationInternalStatus.ApplicationStarted, new [] { EApplicationInternalStatus.InitialApplicationSubmitted } },
    //    { EApplicationInternalStatus.InitialApplicationSubmitted, new [] { EApplicationInternalStatus.FraewInstructed } },
    //    { EApplicationInternalStatus.FraewInstructed, new [] { EApplicationInternalStatus.FraewUploaded } },
    //    { EApplicationInternalStatus.FraewUploaded, new [] { EApplicationInternalStatus.FraewSubmitted } },
    //    { EApplicationInternalStatus.FraewSubmitted, new [] { EApplicationInternalStatus.FinalApplicationSubmitted } },
    //    { EApplicationInternalStatus.FinalApplicationSubmitted, new [] { EApplicationInternalStatus.EligibilityInProgress, EApplicationInternalStatus.EligibilityInReview } },
    //    { EApplicationInternalStatus.EligibilityInProgress, new [] { EApplicationInternalStatus.EligibilityInReview, EApplicationInternalStatus.OnHoldEligibility, EApplicationInternalStatus.EscalatedEligibility, EApplicationInternalStatus.ApplicationNotEligible, EApplicationInternalStatus.ApplicationNotEligiblePostDays, EApplicationInternalStatus.ApplicationClosedReferToGla, EApplicationInternalStatus.ApplicationApproved } },
    //    { EApplicationInternalStatus.EligibilityInReview, new [] { EApplicationInternalStatus.EligibilityInProgress, EApplicationInternalStatus.EscalatedEligibility, EApplicationInternalStatus.ApplicationApproved, EApplicationInternalStatus.ApplicationNotEligible, EApplicationInternalStatus.ApplicationNotEligiblePostDays, EApplicationInternalStatus.ApplicationClosedReferToGla, EApplicationInternalStatus.OnHoldEligibility } },
    //    { EApplicationInternalStatus.EscalatedEligibility, new [] { EApplicationInternalStatus.EligibilityInProgress, EApplicationInternalStatus.EligibilityInReview, EApplicationInternalStatus.ApplicationApproved, EApplicationInternalStatus.ApplicationNotEligible, EApplicationInternalStatus.ApplicationNotEligiblePostDays, EApplicationInternalStatus.OnHoldEligibility } },
    //    { EApplicationInternalStatus.OnHoldEligibility, new [] { EApplicationInternalStatus.EligibilityInProgress, EApplicationInternalStatus.EligibilityInReview, EApplicationInternalStatus.EscalatedEligibility} },
    //    { EApplicationInternalStatus.ApplicationApproved, new [] { EApplicationInternalStatus.GfaAndDotIssued } },
    //    { EApplicationInternalStatus.GfaAndDotIssued, new [] { EApplicationInternalStatus.GfaAndDotReturned, EApplicationInternalStatus.OnHoldGfa } },
    //    { EApplicationInternalStatus.GfaAndDotReturned, new [] { EApplicationInternalStatus.GfaAndDotSigned, EApplicationInternalStatus.OnHoldGfa } },
    //    { EApplicationInternalStatus.GfaAndDotSigned, new [] { EApplicationInternalStatus.GfaInReview, EApplicationInternalStatus.OnHoldGfa, EApplicationInternalStatus.GfaComplete } },
    //    { EApplicationInternalStatus.GfaInReview, new [] { EApplicationInternalStatus.GfaComplete, EApplicationInternalStatus.EscalatedGfa, EApplicationInternalStatus.OnHoldGfa } },
    //    { EApplicationInternalStatus.GfaComplete, new [] { EApplicationInternalStatus.PrimaryReportInProgress, EApplicationInternalStatus.WorksPackageInProgress } },
    //    { EApplicationInternalStatus.EscalatedGfa, new [] { EApplicationInternalStatus.GfaInReview, EApplicationInternalStatus.GfaComplete, EApplicationInternalStatus.OnHoldGfa } },
    //    { EApplicationInternalStatus.OnHoldGfa, new [] { EApplicationInternalStatus.GfaAndDotIssued, EApplicationInternalStatus.GfaAndDotReturned, EApplicationInternalStatus.GfaAndDotSigned, EApplicationInternalStatus.GfaInReview, EApplicationInternalStatus.EscalatedGfa } },
    //    { EApplicationInternalStatus.VendorSetup, new [] { EApplicationInternalStatus.PtfsPaymentEscalated, EApplicationInternalStatus.PreTenderSupportPaid } },
    //    { EApplicationInternalStatus.PtfsPaymentEscalated, new [] { EApplicationInternalStatus.VendorSetup, EApplicationInternalStatus.PreTenderSupportPaid } },
    //    { EApplicationInternalStatus.PreTenderSupportPaid, new [] { EApplicationInternalStatus.PrimaryReportInProgress, EApplicationInternalStatus.WorksPackageInProgress } },
    //    { EApplicationInternalStatus.PrimaryReportInProgress, new [] { EApplicationInternalStatus.PrimaryReportSubmitted, EApplicationInternalStatus.PrimaryReportDue, EApplicationInternalStatus.OnHoldPrimaryReport } },
    //    { EApplicationInternalStatus.PrimaryReportDue, new [] { EApplicationInternalStatus.PrimaryReportOverdue, EApplicationInternalStatus.OnHoldPrimaryReport } },
    //    { EApplicationInternalStatus.PrimaryReportOverdue, new [] { EApplicationInternalStatus.PrimaryReportSubmitted, EApplicationInternalStatus.OnHoldPrimaryReport } },
    //    { EApplicationInternalStatus.OnHoldPrimaryReport, new [] { EApplicationInternalStatus.PrimaryReportInProgress, EApplicationInternalStatus.PrimaryReportSubmitted, EApplicationInternalStatus.PrimaryReportDue, EApplicationInternalStatus.PrimaryReportOverdue } },
    //    { EApplicationInternalStatus.PrimaryReportSubmitted, new [] { EApplicationInternalStatus.WorksPackageInProgress, EApplicationInternalStatus.OnHoldPrimaryReport } },
    //    { EApplicationInternalStatus.WorksPackageInProgress, new [] { EApplicationInternalStatus.WorksPackageSubmitted } },
    //    { EApplicationInternalStatus.WorksPackageSubmitted, new [] { EApplicationInternalStatus.WorksPackageInReview, EApplicationInternalStatus.OnHoldWorksPackage, EApplicationInternalStatus.EscalatedWorksPackage } },
    //    { EApplicationInternalStatus.WorksPackageInReview, new [] { EApplicationInternalStatus.WorksPackageApproved, EApplicationInternalStatus.WorksPackageRejected, EApplicationInternalStatus.OnHoldWorksPackage } },
    //    { EApplicationInternalStatus.WorksPackageApproved, new [] { EApplicationInternalStatus.WorksPackageDeedIssued } },
    //    { EApplicationInternalStatus.WorksPackageRejected, new [] { EApplicationInternalStatus.WorksPackageDeedIssued } },
    //    { EApplicationInternalStatus.EscalatedWorksPackage, new [] { EApplicationInternalStatus.WorksPackageInReview, EApplicationInternalStatus.WorksPackageApproved, EApplicationInternalStatus.WorksPackageRejected, EApplicationInternalStatus.OnHoldWorksPackage } },
    //    { EApplicationInternalStatus.OnHoldWorksPackage, new [] { EApplicationInternalStatus.WorksPackageSubmitted, EApplicationInternalStatus.WorksPackageInReview, EApplicationInternalStatus.EscalatedWorksPackage } },
    //    { EApplicationInternalStatus.WorksPackageDeedIssued, new [] { EApplicationInternalStatus.WorksPackageDeedSigned, EApplicationInternalStatus.WorksPackageDeedReturned } },
    //    { EApplicationInternalStatus.WorksPackageDeedReturned, new [] { EApplicationInternalStatus.WorksPackageDeedComplete } },
    //    { EApplicationInternalStatus.WorksPackageDeedSigned, new [] { EApplicationInternalStatus.WorksPackageDeedReturned, EApplicationInternalStatus.WorksPackageDeedComplete } },
    //    { EApplicationInternalStatus.WorksPackageDeedComplete, new [] { EApplicationInternalStatus.ScheduleOfWorksInProgress } },
    //    { EApplicationInternalStatus.ScheduleOfWorksInProgress, new [] { EApplicationInternalStatus.ScheduleOfWorksSubmitted } },
    //    { EApplicationInternalStatus.ScheduleOfWorksSubmitted, new [] { EApplicationInternalStatus.ScheduleOfWorksInReview, EApplicationInternalStatus.OnHoldScheduleOfWorks, EApplicationInternalStatus.EscalatedScheduleOfWorks } },
    //    { EApplicationInternalStatus.ScheduleOfWorksInReview, new [] { EApplicationInternalStatus.ScheduleOfWorksApproved, EApplicationInternalStatus.OnHoldScheduleOfWorks, EApplicationInternalStatus.EscalatedScheduleOfWorks } },
    //    { EApplicationInternalStatus.OnHoldScheduleOfWorks, new [] { EApplicationInternalStatus.ScheduleOfWorksInReview, EApplicationInternalStatus.ScheduleOfWorksSubmitted, EApplicationInternalStatus.EscalatedScheduleOfWorks } },
    //    { EApplicationInternalStatus.EscalatedScheduleOfWorks, new [] { EApplicationInternalStatus.ScheduleOfWorksInReview, EApplicationInternalStatus.ScheduleOfWorksSubmitted, EApplicationInternalStatus.OnHoldScheduleOfWorks } },
    //    { EApplicationInternalStatus.ScheduleOfWorksApproved, new [] { EApplicationInternalStatus.PaymentRequestInProgress, EApplicationInternalStatus.VariationInProgress } },
    //    { EApplicationInternalStatus.PaymentRequestInProgress, new [] { EApplicationInternalStatus.PaymentRequestSubmitted, EApplicationInternalStatus.VariationInProgress } },
    //    { EApplicationInternalStatus.PaymentRequestSubmitted, new [] { EApplicationInternalStatus.PaymentRequestInReview, EApplicationInternalStatus.VariationInProgress, EApplicationInternalStatus.EscalatedPaymentRequest, EApplicationInternalStatus.OnHoldPaymentRequest } },
    //    { EApplicationInternalStatus.PaymentRequestInReview, new [] { EApplicationInternalStatus.PaymentRequestApproved, EApplicationInternalStatus.PaymentRequestRejected, EApplicationInternalStatus.VariationInProgress, EApplicationInternalStatus.OnHoldPaymentRequest, EApplicationInternalStatus.EscalatedPaymentRequest, EApplicationInternalStatus.VariationInProgress } },
    //    { EApplicationInternalStatus.OnHoldPaymentRequest, new [] { EApplicationInternalStatus.PaymentRequestSubmitted, EApplicationInternalStatus.PaymentRequestInReview, EApplicationInternalStatus.EscalatedPaymentRequest } },
    //    { EApplicationInternalStatus.EscalatedPaymentRequest, new [] { EApplicationInternalStatus.PaymentRequestSubmitted, EApplicationInternalStatus.PaymentRequestInReview, EApplicationInternalStatus.OnHoldPaymentRequest, EApplicationInternalStatus.VariationInProgress } },
    //    { EApplicationInternalStatus.PaymentRequestApproved, new [] { EApplicationInternalStatus.PaymentRequestInProgress, EApplicationInternalStatus.VariationInProgress, EApplicationInternalStatus.ClosingReportInProgress } },
    //    { EApplicationInternalStatus.VariationInProgress, new [] { EApplicationInternalStatus.VariationSubmitted } },
    //    { EApplicationInternalStatus.VariationSubmitted, new [] { EApplicationInternalStatus.VariationInReview, EApplicationInternalStatus.EscalatedVariation, EApplicationInternalStatus.OnHoldVariation } },
    //    { EApplicationInternalStatus.VariationInReview, new [] { EApplicationInternalStatus.VariationApproved, EApplicationInternalStatus.VariationRejected, EApplicationInternalStatus.EscalatedVariation, EApplicationInternalStatus.OnHoldVariation } },
    //    { EApplicationInternalStatus.EscalatedVariation, new [] { EApplicationInternalStatus.VariationSubmitted, EApplicationInternalStatus.VariationInReview, EApplicationInternalStatus.OnHoldVariation } },
    //    { EApplicationInternalStatus.OnHoldVariation, new [] { EApplicationInternalStatus.VariationSubmitted, EApplicationInternalStatus.VariationInReview, EApplicationInternalStatus.EscalatedVariation } },
    //    { EApplicationInternalStatus.VariationApproved, new [] { EApplicationInternalStatus.PaymentRequestInProgress, EApplicationInternalStatus.PaymentRequestInReview, EApplicationInternalStatus.PaymentRequestSubmitted, EApplicationInternalStatus.PaymentRequestApproved, EApplicationInternalStatus.PaymentRequestRejected, EApplicationInternalStatus.EscalatedPaymentRequest } },
    //    { EApplicationInternalStatus.VariationRejected, new [] { EApplicationInternalStatus.PaymentRequestInProgress, EApplicationInternalStatus.PaymentRequestInReview, EApplicationInternalStatus.PaymentRequestSubmitted, EApplicationInternalStatus.PaymentRequestApproved, EApplicationInternalStatus.PaymentRequestRejected, EApplicationInternalStatus.EscalatedPaymentRequest } },
    //    { EApplicationInternalStatus.ClosingReportInProgress, new [] { EApplicationInternalStatus.ClosingReportSubmitted } },
    //    { EApplicationInternalStatus.ClosingReportSubmitted, new [] { EApplicationInternalStatus.ClosingReportInReview, EApplicationInternalStatus.EscalatedClosingReport, EApplicationInternalStatus.OnHoldClosingReport } },
    //    { EApplicationInternalStatus.ClosingReportInReview, new [] { EApplicationInternalStatus.ClosingReportApproved, EApplicationInternalStatus.ClosingReportRejected, EApplicationInternalStatus.EscalatedClosingReport, EApplicationInternalStatus.OnHoldClosingReport } },
    //    { EApplicationInternalStatus.EscalatedClosingReport, new [] { EApplicationInternalStatus.ClosingReportSubmitted, EApplicationInternalStatus.ClosingReportInReview, EApplicationInternalStatus.OnHoldClosingReport } },
    //    { EApplicationInternalStatus.OnHoldClosingReport, new [] { EApplicationInternalStatus.ClosingReportSubmitted, EApplicationInternalStatus.ClosingReportInReview, EApplicationInternalStatus.EscalatedClosingReport } },
    //    { EApplicationInternalStatus.ClosingReportApproved, new [] { EApplicationInternalStatus.WorksCompleted } },
    //    { EApplicationInternalStatus.ClosingReportRejected, new [] { EApplicationInternalStatus.ClosingReportInProgress, EApplicationInternalStatus.ClosingReportInReview } },
    //    { EApplicationInternalStatus.WorksCompleted, new [] { EApplicationInternalStatus.ApplicationClosed } },
    //    { EApplicationInternalStatus.ApplicationClosed, Array.Empty<EApplicationInternalStatus>() },
    //    { EApplicationInternalStatus.ApplicationClosedReferToGla, Array.Empty<EApplicationInternalStatus>() },
    //    { EApplicationInternalStatus.ApplicationNotEligible, Array.Empty<EApplicationInternalStatus>() },
    //    { EApplicationInternalStatus.ApplicationNotEligiblePostDays, Array.Empty<EApplicationInternalStatus>() }
    //};
    #endregion

    public async Task TransitionToStatus(EApplicationStage stage, EApplicationStatus status, string reason = null, params Guid[] applicationIds)
    {
        reason = !string.IsNullOrWhiteSpace(reason) ? reason : null;
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        foreach (var applicationId in applicationIds)
        {
            await _applicationRepository.UpdateApplicationStage(applicationId, stage);
            await _applicationRepository.UpdateStatus(applicationId, status, reason);
        }

        scope.Complete();
    }

    public async Task TransitionToStatus(EApplicationStatus status, string reason = null, params Guid[] applicationIds)
    {
        reason = !string.IsNullOrWhiteSpace(reason) ? reason : null;
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        foreach (var applicationId in applicationIds)
        {
            await _applicationRepository.UpdateStatus(applicationId, status, reason);
        }

        scope.Complete();
    }

    public async Task TransitionToInternalStatus(EApplicationInternalStatus internalStatus, string reason = null, params Guid[] applicationIds)
    {
        reason = !string.IsNullOrWhiteSpace(reason) ? reason : null;
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        foreach (var applicationId in applicationIds)
        {
            await _applicationRepository.UpdateInternalStatus(applicationId, internalStatus, reason);
        }

        scope.Complete();
    }
}