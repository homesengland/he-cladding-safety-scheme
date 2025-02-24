using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.ThirdPartyContributions;

public class ThirdPartyContributionResult
{
    public decimal? ContributionAmount { get; set; }
    public string ContributionNotes { get; set; }
}