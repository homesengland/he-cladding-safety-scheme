using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.VariationRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class ContractorContingencyViewModel : VariationRequestBaseViewModel
    {
        public ENoYes? UsedVariationContractorContingency { get; set; }

        public string ContractorContingencyAdditionalNotes { get; set; }

        public ENoYes? IneligibleCosts { get; set; }

    }
}