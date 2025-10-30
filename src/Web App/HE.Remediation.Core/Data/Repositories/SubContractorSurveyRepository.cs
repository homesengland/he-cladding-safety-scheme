using HE.Remediation.Core.Data.StoredProcedureParameters.SubContractorRatings;
using HE.Remediation.Core.Data.StoredProcedureResults.SubContractorRatings;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories;

public class SubContractorSurveyRepository : ISubContractorSurveyRepository
{
    private readonly IDbConnectionWrapper _connection;    

    public SubContractorSurveyRepository(IDbConnectionWrapper connection)
    {
        _connection = connection;
    }

    public async Task<Guid> CreateSurvey(Guid applicationId)
    {
        return await _connection.QuerySingleOrDefaultAsync<Guid>("CreateSubcontractorSurvey", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task<IReadOnlyCollection<GetSubContractorRatingsOverviewResult>> GetSurveyOverview(Guid subcontractorSurveyId)
    {
        return await _connection.QueryAsync<GetSubContractorRatingsOverviewResult>("GetSubcontractorSurveyOverview", new
        {
            SubcontractorSurveyId = subcontractorSurveyId
        });
    }

    public async Task<GetSubContractorRatingResult> GetRating(Guid subcontractorRatingId)
    {
        return await _connection.QuerySingleOrDefaultAsync<GetSubContractorRatingResult>("GetSubcontractorRating", new
        {
            SubcontractorRatingId = subcontractorRatingId
        });
    }

    public async Task UpdateRating(Guid subcontractorRatingId, UpdateSubcontractorRatingParameters parameters)
    {
        await _connection.ExecuteAsync("UpdateSubcontractorRating", new
        {
            SubcontractorRatingId = subcontractorRatingId,
            parameters?.QualityOfWork,
            parameters?.ValueForMoney,
            parameters?.Reliability,
            parameters?.ConsiderationOfResidents,
            parameters?.OverallSatisfaction,
            parameters?.Status
        });
    }

    public async Task<IReadOnlyCollection<GetSubcontractorRatingsSummaryResult>> GetSummary(Guid subcontractorSurveyId)
    {
        return await _connection.QueryAsync<GetSubcontractorRatingsSummaryResult>("GetSubcontractorSurveySummary", new
        {
            SubcontractorSurveyId = subcontractorSurveyId
        });
    }

    public async Task<int?> GetSubcontractorSurveyCount(Guid applicationId)
    {
        return await _connection.QuerySingleOrDefaultAsync<int?>(nameof(GetSubcontractorSurveyCount), new
        {
            ApplicationId = applicationId
        });
    }

    public async Task AddSubcontractorSurveyLeadContractor(Guid applicationId, Guid subcontractorSurveyId)
    {
        await _connection.ExecuteAsync(nameof(AddSubcontractorSurveyLeadContractor), new
        {
            Applicationid = applicationId,
            SubcontractorSurveyId = subcontractorSurveyId
        });
    }
}