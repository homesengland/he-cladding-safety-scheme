using System.Data;
using System.IO.Pipes;
using Dapper;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Extensions;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories;

public class WorkPackageFireRiskAssessmentRepository : IWorkPackageFireRiskAssessmentRepository
{
    private readonly IDbConnectionWrapper _connection;

    public WorkPackageFireRiskAssessmentRepository(IDbConnectionWrapper connection)
    {
        _connection = connection;
    }

    public async Task InsertWorkPackageFra(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        await _connection.ExecuteAsync(nameof(InsertWorkPackageFra), parameters);
    }

    public async Task<GetWorkPackageFraReportResult> GetWorkPackageFraReport(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        GetWorkPackageFraReportResult result = null;

        await _connection.QueryAsync<GetWorkPackageFraReportResult, FileResult, GetWorkPackageFraReportResult>(
            nameof(GetWorkPackageFraReport),
            (report, file) =>
            {
                result ??= report;

                result.File = file;

                return result;
            },
            parameters);

        return result;
    }

    public async Task UploadWorkPackageFraReport(UploadWorkPackageFraReportParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(UploadWorkPackageFraReport), parameters);
    }

    public async Task DeleteWorkPackageFraReport(DeleteWorkPackageFraReportParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(DeleteWorkPackageFraReport), parameters);
    }

    public async Task<bool> GetWorkPackageFraVisitedCheckYourAnswers(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _connection.QuerySingleOrDefaultAsync<bool>(nameof(GetWorkPackageFraVisitedCheckYourAnswers), parameters);
        return result;
    }

    public async Task SetWorkPackageFireRiskAssessmentType(SetWorkPackageFireRiskAssessmentType parameters)
    {
        await _connection.ExecuteAsync(nameof(SetWorkPackageFireRiskAssessmentType), parameters);
    }

    public async Task<GetWorkPackageFraAssessorAndDateResult> GetWorkPackageFraAssessorAndDate(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _connection.QuerySingleOrDefaultAsync<GetWorkPackageFraAssessorAndDateResult>(nameof(GetWorkPackageFraAssessorAndDate), parameters);
        return result;
    }

    public async Task SetWorkPackageFraAssessorAndDate(SetWorkPackageFraAssessorAndDateParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(SetWorkPackageFraAssessorAndDate), parameters);
    }

    public async Task<GetWorkPackageFraOtherAssessorResult> GetWorkPackageFraOtherAssessor(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _connection.QuerySingleOrDefaultAsync<GetWorkPackageFraOtherAssessorResult>(nameof(GetWorkPackageFraOtherAssessor), parameters);
        return result;
    }

    public async Task SetWorkPackageFraOtherAssessor(SetWorkPackageFraOtherAssessorParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(SetWorkPackageFraOtherAssessor), parameters);
    }

    public async Task<DateTime?> GetWorkPackageFraDate(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _connection.QuerySingleOrDefaultAsync<DateTime?>(nameof(GetWorkPackageFraDate), parameters);
        return result;
    }

    public async Task SetWorkPackageFraDate(SetWorkPackageFraDateParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(SetWorkPackageFraDate), parameters);
    }

    public async Task<GetWorkPacakgeFraFireRiskRatingResult> GetWorkPacakgeFraFireRiskRating(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _connection.QuerySingleOrDefaultAsync<GetWorkPacakgeFraFireRiskRatingResult>(nameof(GetWorkPacakgeFraFireRiskRating), parameters);
        return result;
    }

    public async Task SetWorkPackageFraFireRiskRating(SetWorkPackageFraFireRiskRatingParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(SetWorkPackageFraFireRiskRating), parameters);
    }

    public async Task<IReadOnlyCollection<EInternalFireSafetyDefect>> GetWorkPackageFraInternalDefects(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var results = await _connection.QueryAsync<EInternalFireSafetyDefect>(nameof(GetWorkPackageFraInternalDefects), parameters);
        return results;
    }

    public async Task SetWorkPackageFraInternalDefects(SetWorkPackageFraInternalDefectsParameters parameters)
    {
        var @params = new DynamicParameters();
        @params.Add("@ApplicationId", parameters.ApplicationId);
        @params.Add("@DefectIds", parameters.DefectIds.ToDataTable().AsTableValuedParameter("[dbo].[IntListType]"), DbType.Object);
        @params.Add("@@OtherInternalFireSafetyRisk", parameters.OtherInternalFireSafetyRisk);

        await _connection.ExecuteAsync(nameof(SetWorkPackageFraInternalDefects), @params);
    }

    public async Task<string> GetWorkPackageFraOtherInternalDefect(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _connection.QuerySingleOrDefaultAsync<string>(nameof(GetWorkPackageFraOtherInternalDefect), parameters);
        return result;
    }

    public async Task<GetWorkPackageFraFundingResult> GetWorkPackageFraFunding(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _connection.QuerySingleOrDefaultAsync<GetWorkPackageFraFundingResult>(nameof(GetWorkPackageFraFunding), parameters);
        return result;
    }

    public async Task SetWorkPackageFraFunding(SetWorkPackageFraFundingParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(SetWorkPackageFraFunding), parameters);
    }

    public async Task<GetWorkPackageFraCheckYourAnswersResult> GetWorkPackageFraCheckYourAnswers(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        GetWorkPackageFraCheckYourAnswersResult result = null;

        await _connection.QueryAsync<GetWorkPackageFraCheckYourAnswersResult, GetWorkPackageFraCheckYourAnswersResult.Defect, GetWorkPackageFraCheckYourAnswersResult>(
            nameof(GetWorkPackageFraCheckYourAnswers),
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

    public async Task SetWorkPackageFraVisitedCheckYourAnswers(SetWorkPackageFraVisitedCheckYourAnswersParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(SetWorkPackageFraVisitedCheckYourAnswers), parameters);
    }

    public async Task SetWorkPacakgeFraTaskStatus(SetWorkPacakgeFraTaskStatusParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(SetWorkPacakgeFraTaskStatus), parameters);
    }

    public async Task SubmitWorkPackageFra(Guid applciationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applciationId);

        await _connection.ExecuteAsync(nameof(SubmitWorkPackageFra), parameters);
    }
}