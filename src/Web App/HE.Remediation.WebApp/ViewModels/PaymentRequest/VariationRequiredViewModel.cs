using HE.Remediation.WebApp.ViewModels.PaymentRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class VariationRequiredViewModel : PaymentRequestBaseViewModel
{
    public bool? CostsChanged { get; set; }
    public bool EndDateSlipped { get; set; }
    public bool? ThirdPartyContributionsChanged { get; set; }
}
