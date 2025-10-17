using HE.Remediation.Core.Data.StoredProcedureResults.ClosingReport.EvidenceOfThirdPartyContribution;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.EvidenceDetails
{
    public class GetEvidenceDetailsResponse
    {
        public Guid ApplicationId { get; set; }
        public string ReturnUrl { get; set; }
        public bool IsSubmitted { get; set; }
        public bool IsEditable { get; set; }
        public List<GetEvidenceDetailsResult> EvidenceDetailsResults { get; set; } = new List<GetEvidenceDetailsResult>();
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
    }
}
