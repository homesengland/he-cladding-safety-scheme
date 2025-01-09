using HE.Remediation.WebApp.ViewModels.PaymentRequest.Shared;
using HE.Remediation.WebApp.ViewModels.Shared;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class SubmitPaymentRequestViewModel : PaymentRequestBaseViewModel
{
    public CostsViewModel Costs { get; set; }
}
