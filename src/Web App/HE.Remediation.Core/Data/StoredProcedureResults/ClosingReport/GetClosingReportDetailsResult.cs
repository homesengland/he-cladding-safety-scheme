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

    public bool? FraewRiskToLifeReduced { get; set; }

    public bool? GrantFundingObligations { get; set; }

    public bool? HasThirdPartyContributions { get; set; }

    public bool IsSubmitted { get; set; }
}
