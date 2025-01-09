using HE.Remediation.WebApp.ViewModels.PaymentRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class DeclarationViewModel : PaymentRequestBaseViewModel
{
    public bool? AwareProcess { get; set; }
    public bool? AwareNoPrecedentForFuture { get; set; }
    public bool? PredictionsAccurate { get; set; }    
}
