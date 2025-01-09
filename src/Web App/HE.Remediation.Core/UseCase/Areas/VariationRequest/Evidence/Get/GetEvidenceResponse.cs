using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Evidence.Get;

public class GetEvidenceResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }

    public ENoYes? IneligibleCosts { get; set; }

    public IReadOnlyCollection<FileResult> AddedFiles { get; set; }

    public bool? IsThirdPartyContributionVariation { get; set; }
}
