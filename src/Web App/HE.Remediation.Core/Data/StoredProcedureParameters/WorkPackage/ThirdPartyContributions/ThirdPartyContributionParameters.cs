using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.ThirdPartyContributions;

public class ThirdPartyContributionParameters
{
    public Guid ApplicationId { get; set; }
    public IEnumerable<int> ContributionPursuingTypes { get; set; }
    public decimal ContributionAmount { get; set; }
    public string ContributionNotes { get; set; }
}
