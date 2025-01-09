namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetVariationRequired;

public class GetVariationRequiredResponse
{
    public bool? CostsChanged { get; set; }
    public bool EndDateSlipped { get; set; }
    public bool? ThirdPartyContributionsChanged { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }
    public bool IsExpired { get; set; }
}
