using System.Data;
using Dapper;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Extensions;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories;

public class FireRiskAssessmentRepository : IFireRiskAssessmentRepository
{
    private readonly IDbConnectionWrapper _connection;

    public FireRiskAssessmentRepository(IDbConnectionWrapper connection)
    {
        _connection = connection;
    }

    public async Task<bool?> GetHasFra(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _connection.QuerySingleOrDefaultAsync<bool?>(nameof(GetHasFra), parameters);

        return result;
    }

    public async Task SetHasFra(SetHasFraParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(SetHasFra), parameters);
    }

    public async Task CreateFra(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        await _connection.ExecuteAsync(nameof(CreateFra), parameters);
    }

    public async Task SetFraTaskStatus(SetFraTaskStatusParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(SetFraTaskStatus), parameters);
    }

    public async Task<GetAssessorAndFraDateResult> GetAssessorAndFraDate(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _connection.QuerySingleOrDefaultAsync<GetAssessorAndFraDateResult>(nameof(GetAssessorAndFraDate), parameters);
        return result;
    }

    public async Task SetAssessorAndFraDate(SetAssessorAndFraDateParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(SetAssessorAndFraDate), parameters);
    }

    public async Task<GetOtherAssessorResult> GetOtherAssessor(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _connection.QuerySingleOrDefaultAsync<GetOtherAssessorResult>(nameof(GetOtherAssessor), parameters);
        return result;
    }

    public async Task SetOtherAssessor(SetOtherAssessorParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(SetOtherAssessor), parameters);
    }

    public async Task<DateTime?> GetFraDate(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _connection.QuerySingleOrDefaultAsync<DateTime?>(nameof(GetFraDate), parameters);
        return result;
    }

    public async Task SetFraDate(SetFraDateParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(SetFraDate), parameters);
    }

    public async Task<IReadOnlyCollection<EInternalFireSafetyDefect>> GetInternalFireSafetyDefects(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var results = await _connection.QueryAsync<EInternalFireSafetyDefect>(nameof(GetInternalFireSafetyDefects), parameters);
        return results;
    }

    public async Task<string> GetOtherInternalSafetyRisk(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _connection.QuerySingleOrDefaultAsync<string>(nameof(GetOtherInternalSafetyRisk), parameters);
        return result;
    }

    public async Task SetInternalFireSafetyDefects(SetInternalFireSafetyDefectsParameters parameters)
    {
        var @params = new DynamicParameters();
        @params.Add("@ApplicationId", parameters.ApplicationId);
        @params.Add("@InternalFireSafetyDefectIds", parameters.InternalFireSafetyDefectIds.Cast<int>().ToDataTable().AsTableValuedParameter("[dbo].[IntListType]"), DbType.Object);
        @params.Add("@OtherInternalFireSafetyRisk", parameters.OtherInternalFireSafetyRisk);

        await _connection.ExecuteAsync(nameof(SetInternalFireSafetyDefects), @params);
    }

    public async Task<GetFireRiskRatingResult> GetFireRiskRating(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _connection.QuerySingleOrDefaultAsync<GetFireRiskRatingResult>(nameof(GetFireRiskRating), parameters);
        return result;
    }

    public async Task SetFireRiskRating(SetFireRiskRatingParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(SetFireRiskRating), parameters);
    }

    public async Task<GetFraFundingResult> GetFraFunding(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _connection.QuerySingleOrDefaultAsync<GetFraFundingResult>(nameof(GetFraFunding), parameters);
        return result;
    }

    public async Task SetFraFunding(SetFraFundingParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(SetFraFunding), parameters);
    }

    public async Task<GetFraCheckYourAnswersResult> GetFraCheckYourAnswers(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        GetFraCheckYourAnswersResult result = null;
        await _connection.QueryAsync<GetFraCheckYourAnswersResult, GetFraCheckYourAnswersResult.Defect, GetFraCheckYourAnswersResult>(
            nameof(GetFraCheckYourAnswers),
            (answers, defect) =>
            {
                result ??= answers;

                if (defect is not null)
                {
                    result.Defects.Add(defect.Name);
                }

                return result;
            },
            parameters);

        return result;
    }

    public async Task<bool> GetFraVisitedCheckYourAnswers(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _connection.QuerySingleOrDefaultAsync<bool>(nameof(GetFraVisitedCheckYourAnswers), parameters);
        return result;
    }

    public async Task SetFraVisitedCheckYourAnswers(SetFraVisitedCheckYourAnswersParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(SetFraVisitedCheckYourAnswers), parameters);
    }

    public async Task SubmitFra(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        await _connection.ExecuteAsync(nameof(SubmitFra), parameters);
    }

    public async Task ClearFraAnswers(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        await _connection.ExecuteAsync(nameof(ClearFraAnswers), parameters);
    }

    public async Task<GetFireRiskAssessmentForApplicationResult> GetFireRiskAssessmentForApplication(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        GetFireRiskAssessmentForApplicationResult result = null;

        await _connection.QueryAsync<GetFireRiskAssessmentForApplicationResult, FileResult, GetFireRiskAssessmentForApplicationResult>("GetFireRiskAssessmentForApplication",
            (fraType, fraFile) =>
            {
                result ??= fraType;
                result.AddedFra = fraFile;
                return result;

            }, 
            parameters);

        return result;
    }

    public async Task DeleteFraForApplication(DeleteFraForApplicationParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(DeleteFraForApplication), parameters);
    }
}