using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.ContractorContingency.Set
{
    public class SetContractorContingencyRequest : IRequest
    {
        public ENoYes? UsedVariationContractorContingency { get; set; }

        public string ContractorContingencyAdditionalNotes { get; set; }
    }
}
