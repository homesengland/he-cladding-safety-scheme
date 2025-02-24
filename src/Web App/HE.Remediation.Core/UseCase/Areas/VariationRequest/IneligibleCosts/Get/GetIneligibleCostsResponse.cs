using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.IneligibleCosts.Get
{
    public class GetIneligibleCostsResponse
    {
        public string ApplicationReferenceNumber { get; set; }

        public string BuildingName { get; set; }

        public bool IsSubmitted { get; set; }

        public ENoYes? HasVariationIneligibleCosts { get; set; }
    }
}
