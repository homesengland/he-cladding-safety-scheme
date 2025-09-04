using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.ContractorContingency.Get
{
    public class GetContractorContingencyResponse
    {
        public string ApplicationReferenceNumber { get; set; }

        public string BuildingName { get; set; }

        public bool IsSubmitted { get; set; }

        public ENoYes? UsedVariationContractorContingency { get; set; }

        public string ContractorContingencyAdditionalNotes { get; set; }

        public ENoYes? IneligibleCosts { get; set; }
    }
}
