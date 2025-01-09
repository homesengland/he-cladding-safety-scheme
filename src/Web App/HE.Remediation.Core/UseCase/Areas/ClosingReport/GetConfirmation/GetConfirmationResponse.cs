namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetConfirmation;

public class GetConfirmationResponse
{
	public bool? FinalCostReport { get; set; }
	public bool? ExitFraew { get; set; }
	public bool? CompletionCertificate { get; set; }
	public bool? InformedPracticalCompletion { get; set; } 

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }
}