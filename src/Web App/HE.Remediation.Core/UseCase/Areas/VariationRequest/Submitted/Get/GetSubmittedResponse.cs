namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Submitted.Get;

public class GetSubmittedResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public int? VariationNumber { get; set; }
}
