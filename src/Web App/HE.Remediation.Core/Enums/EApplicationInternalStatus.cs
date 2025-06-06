﻿namespace HE.Remediation.Core.Enums
{
    public enum EApplicationInternalStatus
    {
        NotStarted = 1,
        ApplicationStarted = 2,
        InitialApplicationSubmitted = 3,
        FraewInstructed = 4,
        FraewUploaded = 5,
        FraewSubmitted = 6,
        FinalApplicationSubmitted = 7,
        EligibilityInProgress = 8,
        EligibilityInReview = 9,
        EscalatedEligibility = 10,
        ApplicationApproved = 11,
        ApplicationNotEligible = 12,
        ApplicationNotEligiblePostDays = 13,
        GfaAndDotIssued = 14,
        GfaAndDotReturned = 15,
        GfaAndDotSigned = 16,
        GfaInReview = 17,
        EscalatedGfa = 18,
        VendorSetup = 19,
        GfaComplete = 20,
        PreTenderSupportPaid = 21,
        PtfsPaymentEscalated = 22,
        WorksPackageInProgress = 23,
        WorksPackageSubmitted = 24,
        EscalatedWorksPackage = 25,
        WorksPackageApproved = 26,
        WorksPackageRejected = 27,
        OnHoldPrimaryReport = 28,
        PrimaryReportInProgress = 29,
        PrimaryReportDue = 30,
        PrimaryReportOverdue = 31,
        PrimaryReportSubmitted = 32,
        WorksPackageInReview = 33,
        OnHoldWorksPackage = 34,
        ScheduleOfWorksInProgress = 35,
        ScheduleOfWorksSubmitted = 36,
        ScheduleOfWorksInReview = 37,
        OnHoldScheduleOfWorks = 38,
        EscalatedScheduleOfWorks = 39,
        ScheduleOfWorksApproved = 40,
        PaymentRequestInProgress = 41,
        PaymentRequestSubmitted = 42,
        PaymentRequestInReview = 43,
        OnHoldPaymentRequest = 44,
        EscalatedPaymentRequest = 45,
        PaymentRequestApproved = 46,
        VariationInProgress = 47,
        VariationSubmitted = 48,
        VariationInReview = 49,
        OnHoldVariation = 50,
        EscalatedVariation = 51,
        VariationApproved = 52,
        ClosingReportInProgress = 53,
        ClosingReportSubmitted = 54,
        ClosingReportInReview = 55,
        OnHoldClosingReport = 56,
        EscalatedClosingReport = 57,
        ClosingReportApproved = 58,
        PaymentRequestRejected = 59,
        ClosingReportRejected = 60,
        WorksPackageDeedIssued = 61,
        WorksPackageDeedReturned = 62,
        WorksPackageDeedSigned = 63,
        VariationRejected = 64,
        WorksCompleted = 65,
        OnHoldEligibility = 66,
        OnHoldGfa = 67,
        WorksPackageDeedComplete = 68,
        ApplicationClosed = 69,
        ApplicationClosedReferToGla = 70,
        WorksPackageLetterIssued = 71,
        WorksPackageMemorandumIssued = 72
    }
}