namespace HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest
{
    public class UpdateVariationUnsafeCladdingCostsParameters
    {
        public Guid VariationRequestId { get; set; }
        public decimal? VariationRemovalOfCladdingAmount { get; set; }
        public string VariationRemovalOfCladdingDescription { get; set; }
    }
}
