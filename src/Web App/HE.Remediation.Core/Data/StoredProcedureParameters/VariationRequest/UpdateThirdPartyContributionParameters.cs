using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest;

public class UpdateThirdPartyContributionParameters
{
    public Guid ApplicationId { get; set; }
    public Guid? VariationRequestId { get; set; }
    public IEnumerable<int> ContributionPursuingTypes { get; set; }
    public decimal ContributionAmount { get; set; }
    public string ContributionNotes { get; set; }
}
