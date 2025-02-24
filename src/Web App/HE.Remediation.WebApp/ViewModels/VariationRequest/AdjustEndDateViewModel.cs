using HE.Remediation.WebApp.ViewModels.VariationRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class AdjustEndDateViewModel : VariationRequestBaseViewModel
{
    public int? NewEndMonth { get; set; }
    public int? NewEndYear { get; set; }
}