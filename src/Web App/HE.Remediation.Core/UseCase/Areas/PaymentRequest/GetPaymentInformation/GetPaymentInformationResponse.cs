
namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetPaymentInformation;

public class GetPaymentInformationResponse
{
    public bool IsSubmitted { get; set; } 
    
    public bool IsExpired { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
}
