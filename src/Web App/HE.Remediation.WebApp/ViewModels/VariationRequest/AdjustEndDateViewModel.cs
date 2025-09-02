using HE.Remediation.WebApp.ViewModels.VariationRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class AdjustEndDateViewModel : VariationRequestBaseViewModel
{
    public int? NewEndMonth { get; set; }
    public int? NewEndYear { get; set; }
    public int? PreviousEndMonth { get; set; }
    public int? PreviousEndYear { get; set; }
    public bool LastMonthlyPaymentCompleted { get; set; }
}