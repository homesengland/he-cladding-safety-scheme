namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetChangeProjectDates;

public class GetChangeProjectDatesResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }
    public bool IsExpired { get; set; }
    
    public int? ProjectDateEndMonth { get; set; }
    public int? ProjectDateEndYear { get; set; }  
    
    public DateTime? ExpectedStartDate { get; set; }
}
