namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Declaration.Get;

public class GetDeclarationResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool? ConfirmedAwareOfApproval { get; set; }

    public bool? ConfirmedCostsReasonable { get; set; }

    public bool? ConfirmedCoversRecommendations { get; set; }

    public bool IsSubmitted { get; set; }
}
