using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.ClosingReport;

public class GetClosingReportDetailsResult
{
	public Guid? PaymentRequestId { get; set; }
	public Guid? SubcontractorSurveyId { get; set; }

	public bool? FinalCostReport { get; set; }

	public bool? ExitFraew { get; set; }

	public bool? CompletionCertificate { get; set; }

	public bool? InformedPracticalCompletion { get; set; }

	public DateTime? ProjectCompletionDate { get; set; }

	public ERiskType? LifeSafetyRiskAssessment { get; set; }

	public bool IsSubmitted { get; set; }
}
