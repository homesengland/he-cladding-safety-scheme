namespace HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest
{
    public class UpdateVariationContractorContingencyParameters
    {
        public Guid VariationRequestId { get; set; }

        public bool? UsedContractorContingency { get; set; }

        public string UsedContractorContingencyDescription { get; set; }
    }
}
