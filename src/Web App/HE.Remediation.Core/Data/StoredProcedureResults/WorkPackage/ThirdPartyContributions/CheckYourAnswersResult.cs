using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.ThirdPartyContributions;

public class CheckYourAnswersResult
{
    public IEnumerable<string> ContributionTypes { get; set; }
    public bool PursuingThirdPartyContribution { get; set; }
    public decimal ContributionAmount { get; set; }
    public string ContributionNotes { get; set; }
}