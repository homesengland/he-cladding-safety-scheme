using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.VariationRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class IneligibleCostsViewModel : VariationRequestBaseViewModel
    {
        public ENoYes? HasVariationIneligibleCosts { get; set; }
    }
}