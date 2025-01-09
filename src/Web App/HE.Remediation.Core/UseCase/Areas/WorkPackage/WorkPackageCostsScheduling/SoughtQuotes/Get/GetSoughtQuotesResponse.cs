using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.SoughtQuotes.Get;

public class GetSoughtQuotesResponse
{
    public ENoYes? SoughtQuotes { get; set; }
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }

}
