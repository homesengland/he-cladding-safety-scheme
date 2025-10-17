using HE.Remediation.Core.Data.StoredProcedureParameters.SubContractorRatings;
using HE.Remediation.Core.Data.StoredProcedureResults.SubContractorRatings;

namespace HE.Remediation.Core.Data.Repositories;

public interface ISubContractorSurveyRepository
{
    Task<Guid> CreateSurvey(Guid applicationId);
    Task<IReadOnlyCollection<GetSubContractorRatingsOverviewResult>> GetSurveyOverview(Guid subcontractorSurveyId);
    Task<GetSubContractorRatingResult> GetRating(Guid subcontractorRatingId);
    Task UpdateRating(Guid subcontractorRatingId, UpdateSubcontractorRatingParameters parameters);
    Task<IReadOnlyCollection<GetSubcontractorRatingsSummaryResult>> GetSummary(Guid subcontractorSurveyId);
    Task<int?> GetSubcontractorSurveyCount(Guid applicationId);
    Task AddSubcontractorSurveyLeadContractor(Guid applicationId, Guid subcontractorSurveyId);
}