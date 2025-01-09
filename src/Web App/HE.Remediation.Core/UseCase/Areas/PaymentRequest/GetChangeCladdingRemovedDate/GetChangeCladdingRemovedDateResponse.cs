namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetChangeCladdingRemovedDate;

public class GetChangeCladdingRemovedDateResponse
{    
    public int? DateRemovedMonth { get; set; }
    public int? DateRemovedYear { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }
    public bool IsExpired { get; set; }
}
