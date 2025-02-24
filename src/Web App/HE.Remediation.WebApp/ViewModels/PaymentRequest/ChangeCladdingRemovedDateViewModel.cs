using HE.Remediation.WebApp.ViewModels.PaymentRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class ChangeCladdingRemovedDateViewModel : PaymentRequestBaseViewModel
{
    public int? DateRemovedMonth { get; set; }
    public int? DateRemovedYear { get; set; }    
}
