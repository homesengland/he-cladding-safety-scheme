using HE.Remediation.WebApp.ViewModels.VariationRequest.Shared;
using System.ComponentModel.DataAnnotations;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class ConfirmRemoveVariationReasonViewModel : VariationRequestBaseViewModel
{
    public bool? IsCostVariation { get; set; }

    public bool? IsScopeVariation { get; set; }

    public bool? IsTimescaleVariation { get; set; }

    public bool? IsThirdPartyContributionVariation { get; set; }

    public bool? Proceed { get; set; }
}
