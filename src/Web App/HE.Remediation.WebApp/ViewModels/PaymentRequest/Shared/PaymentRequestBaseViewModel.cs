namespace HE.Remediation.WebApp.ViewModels.PaymentRequest.Shared;

public class PaymentRequestBaseViewModel
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }

    public bool IsExpired { get; set; }

    public string ReturnUrl { get; set; }
}
