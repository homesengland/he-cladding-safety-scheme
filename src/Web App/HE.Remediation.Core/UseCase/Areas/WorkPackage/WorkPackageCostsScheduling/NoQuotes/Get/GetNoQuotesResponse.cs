namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.NoQuotes.Get;

public class GetNoQuotesResponse
{
    public string Reason { get; set; }
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }
}
