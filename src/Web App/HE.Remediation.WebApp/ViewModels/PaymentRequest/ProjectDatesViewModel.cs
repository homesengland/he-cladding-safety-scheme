using HE.Remediation.WebApp.ViewModels.PaymentRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class ProjectDatesViewModel : PaymentRequestBaseViewModel
{    
    public bool? ProjectDatesChanged { get; set; }

    public DateTime? ExpectedStartDate { get; set; }

    public DateTime? ExpectedEndDate { get; set; }    

    public bool? UnsafeCladdingAlreadyRemoved { get; set; }

    public bool IsFirstPaymentRequest { get; set; }
}
