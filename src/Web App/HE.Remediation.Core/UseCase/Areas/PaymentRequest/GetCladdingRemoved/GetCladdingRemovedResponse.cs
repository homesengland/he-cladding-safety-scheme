namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetCladdingRemoved;

public class GetCladdingRemovedResponse
{
    public bool? UnsafeCladdingRemoved { get; set; }    

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }
    public bool IsExpired { get; set; }
}
