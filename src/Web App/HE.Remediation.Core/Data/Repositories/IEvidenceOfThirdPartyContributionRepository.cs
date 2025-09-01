using HE.Remediation.Core.Data.StoredProcedureResults.ClosingReport.EvidenceOfThirdPartyContribution;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.DeleteEvidence;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.EvidenceDetails;

namespace HE.Remediation.Core.Data.Repositories
{
    public interface IEvidenceOfThirdPartyContributionRepository
    {
        Task<List<GetEvidenceDetailsResult>> GetEvidenceDetails(Guid applicationId);
        Task<SetEvidenceDetailResponse> UpsertClosingReportEvidenceDetail(SetEvidenceDetailRequest request);
        Task<bool> DeleteEvidenceDetails(DeleteEvidenceRequest request);
        Task InsertThirdPartyEvidenceFile(Guid applicationId, Guid fileId, Guid evidenceId);
        Task DeleteThirdPartyEvidenceFile(Guid applicationId, Guid fileId, Guid evidenceId);
        Task ImportClosingReportEvidenceDetail(Guid applicationId);
        Task UpdateClosingReportThirdPartyEvidenceAsSubmitted(Guid applicationId, Guid evidenceId);
    }
}
