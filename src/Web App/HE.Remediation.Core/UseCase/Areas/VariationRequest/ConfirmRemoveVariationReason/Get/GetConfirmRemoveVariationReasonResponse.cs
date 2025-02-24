namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.ConfirmRemoveVariationReason.Get;

public class GetConfirmRemoveVariationReasonResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }
}
