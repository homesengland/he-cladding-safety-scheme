using HE.Remediation.WebApp.ViewModels.VariationRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class DeclarationViewModel : VariationRequestBaseViewModel
{
    public bool? ConfirmedAwareOfApproval { get; set; }

    public bool? ConfirmedCostsReasonable { get; set; }

    public bool? ConfirmedCoversRecommendations { get; set; }
}
