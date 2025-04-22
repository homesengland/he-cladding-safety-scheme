using System.ComponentModel.DataAnnotations;

namespace HE.Remediation.Core.Enums
{
    public enum EApplicationStatus
    {
        ApplicationNotStarted = 1,
        ApplicationInProgress = 2,
        ApplicationSubmitted = 3,
        ApplicationInReview = 4,
        ApplicationApproved = 5,
        ApplicationNotEligible = 6,
        GfaIssued = 7,
        GfaSubmitted = 8,
        GfaComplete = 9,
        Completed = 10,
        Rejected = 11,
        WorksPackageInProgress = 12,
        WorksPackageSubmitted = 13,
        WorksPackageInReview = 14,
        WorksPackageApproved = 15,
        WorksPackageRejected = 16,
        [Display(Name = "Progress reporting")]
        ProgressReporting = 17,
        [Display(Name = "Schedule of Works In Progress")]
        ScheduleOfWorksInProgress = 18,
        [Display(Name = "Schedule of Works Submitted")]
        ScheduleOfWorksSubmitted = 19,
        [Display(Name = "Schedule of Works In Review")]
        ScheduleOfWorksInReview = 20,
        [Display(Name = "Schedule of Works Approved")]
        ScheduleOfWorksApproved = 21,
        PaymentRequestInProgress = 22,
        PaymentRequestSubmitted = 23,
        PaymentRequestInReview = 24,
        PaymentRequestApproved = 25,
        VariationInProgress = 26,
        VariationSubmitted = 27,
        VariationInReview = 28,
        VariationApproved = 29,
        ClosingReportInProgress = 30,
        ClosingReportSubmitted = 31,
        ClosingReportInReview = 32,
        ClosingReportApproved = 33,
        PaymentRequestRejected = 34,
        ClosingReportRejected = 35,
        WorksPackageDeedIssued = 36,
        WorksPackageDeedSubmitted = 37,
        WorksPackageDeedSigned = 38,
        VariationRejected = 39,
        WorksComplete = 40,
        WorksPackageDeedComplete = 41,
        ApplicationClosed = 42,
        ApplicationClosedReferToGla = 43,
        WorksPackageLetterIssued = 44,
        WorksPackageMemorandumIssued = 45
    }
}
