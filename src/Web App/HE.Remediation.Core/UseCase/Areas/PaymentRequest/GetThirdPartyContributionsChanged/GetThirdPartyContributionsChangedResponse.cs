namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetThirdPartyContributionsChanged;

public class GetThirdPartyContributionsChangedResponse
{
    public bool? ThirdPartyContributionsChanged { get; set; }
    public bool? CostsChanged { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }
    public bool IsExpired { get; set; }
}
