namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.ReasonForNoContributions.Get;

public class GetReasonForNoContributionsResponse
{
    public string ReasonNoThirdPartyContributions { get; set; }
    public bool IsSubmitted { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
}
