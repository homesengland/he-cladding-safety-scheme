using HE.Remediation.Core.Data.StoredProcedureResults.ClosingReport.EvidenceOfThirdPartyContribution;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.EvidenceDetails
{
    public class SetEvidenceDetailResponse
    {
        public Guid ApplicationId { get; set; }
        public string ReturnUrl { get; set; }
        public bool IsSubmitted { get; set; }
        public GetEvidenceDetailsResult EvidenceDetailsResults { get; set; }
    }
}
