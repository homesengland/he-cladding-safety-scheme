using HE.Remediation.WebApp.ViewModels.PaymentRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class ThirdPartyContributionsChangedViewModel : PaymentRequestBaseViewModel
{
    public bool? ThirdPartyContributionsChanged { get; set; }
    public bool? CostsChanged { get; set; }
}
