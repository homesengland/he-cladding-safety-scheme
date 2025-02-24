using Azure.Core;

namespace HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest
{
    public class UpdateVariationIneligibleCostsParameters
    {
        public Guid VariationRequestId { get; set; }
        public bool? HasVariationIneligibleCosts { get; set; }
        public decimal? VariationIneligibleAmount { get; set; }
        public string VariationIneligibleDescription { get; set; }
    }
}
