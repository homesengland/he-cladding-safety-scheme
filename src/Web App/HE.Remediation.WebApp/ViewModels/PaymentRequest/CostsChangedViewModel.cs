using HE.Remediation.WebApp.ViewModels.PaymentRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class CostsChangedViewModel : PaymentRequestBaseViewModel
{
    public bool? CostsChanged { get; set; }    

    public bool? UnsafeCladdingRemoved { get; set; }
}
