using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.PreferredContractorLinks.Get;

public class GetPreferredContractorLinksResponse
{
    public EYesNoNonBoolean? PreferredContractorLinks { get; set; }
    public string PreferredContractorLinkAdditionalNotes { get; set; }
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }
    public ENoYes? SoughtQuotes { get; set; }
}
