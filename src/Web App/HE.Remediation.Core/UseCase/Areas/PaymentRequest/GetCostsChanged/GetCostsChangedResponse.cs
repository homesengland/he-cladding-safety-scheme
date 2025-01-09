namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetCostsChanged;

public class GetCostsChangedResponse
{
    public bool? CostsChanged { get; set; }
    public bool? UnsafeCladdingRemoved { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }

    public bool IsExpired { get; set; }
}
