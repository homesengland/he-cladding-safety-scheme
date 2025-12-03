using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport;
using HE.Remediation.Core.Data.StoredProcedureResults.PaymentRequest;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.Application;

public class StageDiagramViewModel
{
    public string ApplicationNumber { get; set; }

    public string UniqueBuildingName { get; set; }

    public EApplicationStage Stage { get; set; }

    public EApplicationStatus Status { get; set; }
    public EApplicationScheme ApplicationScheme { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? SubmittedDate { get; set; }

    public bool HasSubmittedProgressReports { get; set; }

    public bool HasScheduleOfWorks { get; set; }

    public bool IsScheduleOfWorksApproved { get; set; }

    public bool IsScheduleOfWorksSubmitted { get; set; }
    
    public bool HasSubmittedPaymentRequests { get; set; }
    
    public bool HasWorkPackage { get; set; }

    public bool HasInProgressVariationRequest { get; set; }

    public EVariationRequestApprovalStatus? VariationRequestApprovalStatus { get; set; }

    public bool IsVariationRequestSubmitted { get; set; }

    public bool IsWorkPackageSubmitted { get; set; }

    public bool? isWorkPackageConfirmedToProceed { get; set; }

    public DateTime? StartedOnSiteMilestoneDate { get; set; }
    public bool IsStartedOnSiteSubmitted { get; set; }
    public DateTime? PracticalCompletionMilestoneDate { get; set; }
    public bool IsPracticalCompletionSubmitted { get; set; }

    public List<ProgressReportResult> ProgressReports { get; set; } = new List<ProgressReportResult>();

    public List<PaymentRequestResult> PaymentRequests { get; set; } = new List<PaymentRequestResult>();

    public bool ShowClosingReport { get; set; }

    public EPaymentRequestTaskStatus? ClosingReportStatus { get; set; }

    public bool ClosingReportStarted { get; set; }

    public bool HasProgressReports { get; set; }

    public bool ConsiderVariation { get; set; }

    public bool IsClosingReportSubmitted { get; set; }

    public bool IsApplicationActive { get; set; }
}