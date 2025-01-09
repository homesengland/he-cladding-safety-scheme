namespace HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest
{
    public class UpdateHasVariationIneligibleCostsParameters
    {
        public Guid VariationRequestId { get; set; }
        public bool? HasVariationIneligibleCosts { get; set; }
    }
}
