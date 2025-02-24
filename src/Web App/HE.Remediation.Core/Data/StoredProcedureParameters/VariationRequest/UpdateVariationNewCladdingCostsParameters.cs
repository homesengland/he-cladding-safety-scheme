namespace HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest
{
    public class UpdateVariationNewCladdingCostsParameters
    {
        public Guid VariationRequestId { get; set; }
        public decimal? NewCladdingAmount { get; set; }
        public string NewCladdingDescription { get; set; }
        public decimal? OtherEligibleWorkToExternalWallAmount { get; set; }
        public string OtherEligibleWorkToExternalWallDescription { get; set; }
        public decimal? InternalMitigationWorksAmount { get; set; }
        public string InternalMitigationWorksDescription { get; set; }
    }
}