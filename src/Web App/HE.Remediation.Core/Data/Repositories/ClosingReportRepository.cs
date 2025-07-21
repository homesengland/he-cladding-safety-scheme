using HE.Remediation.Core.Data.StoredProcedureParameters.ClosingReport;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Data.StoredProcedureResults.ClosingReport;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories;

public class ClosingReportRepository : IClosingReportRepository
{
    private readonly IDbConnectionWrapper _connection;

    public ClosingReportRepository(IDbConnectionWrapper connection)
    {
        _connection = connection;
    }

    public async Task<GetClosingReportConfirmationDetailsResult> GetClosingReportConfirmationDetails(Guid applicationId)
    {
        return await _connection.QuerySingleOrDefaultAsync<GetClosingReportConfirmationDetailsResult>(
            nameof(GetClosingReportConfirmationDetails), new
            {
                ApplicationId = applicationId
            });
    }
    public async Task<bool?> GetClosingReportNeedVariations(Guid applicationId)
    {
        return await _connection.QuerySingleOrDefaultAsync<bool?>(
            nameof(GetClosingReportNeedVariations), new
            {
                ApplicationId = applicationId
            });
    }
    public async Task<bool> IsClosingReportSubmitted(Guid applicationId)
    {
        return await _connection.QuerySingleOrDefaultAsync<bool>(nameof(IsClosingReportSubmitted), new
        {
            ApplicationId = applicationId
        });
    }
    
    public async Task<GetClosingReportDetailsResult> GetClosingReportDetails(Guid applicationId)
    {
        return await _connection.QuerySingleOrDefaultAsync<GetClosingReportDetailsResult>(
            nameof(GetClosingReportDetails), new
            {
                ApplicationId = applicationId
            });
    }

    public async Task UpdateClosingReportProjectDate(Guid applicationId, DateTime? projectCompletionDate)
    {
        await _connection.ExecuteAsync(nameof(UpdateClosingReportProjectDate), new
        {
            ApplicationId = applicationId,
            ProjectCompletionDate = projectCompletionDate
        });
    }

    public async Task UpdateClosingReportLifeSafetyRiskAssessment(Guid applicationId, ERiskType? lifeSafetyRiskAssessment)
    {
        await _connection.ExecuteAsync(nameof(UpdateClosingReportLifeSafetyRiskAssessment), new
        {
            ApplicationId = applicationId,
            LifeSafetyRiskAssessment = lifeSafetyRiskAssessment
        });
    }

    public async Task UpdateClosingReportDeclarations(Guid applicationId, bool? fraewRiskToLifeReduced,
                                                       bool? grantFundingObligations)
    {
        await _connection.ExecuteAsync(nameof(UpdateClosingReportDeclarations), new
        {
            ApplicationId = applicationId,
            FraewRiskToLifeReduced = fraewRiskToLifeReduced,
            GrantFundingObligations = grantFundingObligations
        });
    }

    public async Task UpdateClosingReportToSubmitted(Guid applicationId)
    {
        await _connection.ExecuteAsync(nameof(UpdateClosingReportToSubmitted), new
        {
            ApplicationId = applicationId,
            IsSubmitted = true
        });
    }

    public async Task UpdateClosingReportConfirmation(Guid applicationId, ConfirmationParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(UpdateClosingReportConfirmation), new
        {
            ApplicationId = applicationId,
            parameters?.FinalCostReport,
            parameters?.ExitFraew,
            parameters?.CompletionCertificate,
            parameters?.InformedPracticalCompletion
        });
    }

    public async Task UpdateClosingReportNeedVariations(Guid applicationId, bool? needVariations)
    {
        await _connection.ExecuteAsync(nameof(UpdateClosingReportNeedVariations), new
        {
            ApplicationId = applicationId,
            NeedVariations = needVariations
        });
    }

    public async Task InsertFile(Guid applicationId, Guid fileId, EClosingReportFileType uploadType)
    {
        await _connection.ExecuteAsync("InsertClosingReportFile", new
        {
            ApplicationId = applicationId,
            FileId = fileId,
            UploadType = uploadType
        });
    }

    public async Task DeleteFile(Guid fileId)
    {
        await _connection.ExecuteAsync("DeleteClosingReportFile", new
        {
            FileId = fileId
        });
    }

    public async Task<IReadOnlyCollection<FileResult>> GetFiles(Guid applicationId, EClosingReportFileType uploadType)
    {
        return await _connection.QueryAsync<FileResult>("GetClosingReportFiles", new
        {
            ApplicationId = applicationId,
            UploadType = uploadType
        });
    }

    public async Task<Guid?> GetSubcontractorSurveyId(Guid applicationId)
    {
        return await _connection.QuerySingleOrDefaultAsync<Guid?>("GetClosingReportSubcontractorSurveyId", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task UpdateSubcontractorSurveyId(Guid applicationId, Guid subContractorSurveyId)
    {
        await _connection.ExecuteAsync("UpdateClosingReportSubcontractorSurveyId", new
        {
            ApplicationId = applicationId,
            SubContractorSurveyId = subContractorSurveyId
        });
    }

    public async Task<bool> GetApplicationReadyForClosingReport(Guid applicationId)
    {
        return await _connection.QuerySingleOrDefaultAsync<bool>(nameof(GetApplicationReadyForClosingReport), new { applicationId });
    }

    public async Task CreateClosingReport(Guid applicationId)
    {
        await _connection.ExecuteAsync(nameof(CreateClosingReport), new { applicationId });
    }

    public async Task<IReadOnlyCollection<GetClosingReportCostProfileResult>> GetClosingReportCostProfile(Guid applicationId)
    {
        return await _connection.QueryAsync<GetClosingReportCostProfileResult>(nameof(GetClosingReportCostProfile), new { applicationId });
    }

    public async Task UpdateFinalPaymentAmount(Guid costId, decimal? amount)
    {
        await _connection.ExecuteAsync(nameof(UpdateFinalPaymentAmount), new { costId, amount });
    }

    public async Task<decimal> GetClosingReportRequestedAmount(Guid applicationId)
    {
        return await _connection.QuerySingleOrDefaultAsync<decimal>(nameof(GetClosingReportRequestedAmount), new {applicationId});
    }

    public async Task UpdateClosingReportCostChanged(Guid applicationId, bool haveCostsChanged)
    {
        await _connection.ExecuteAsync(nameof(UpdateClosingReportCostChanged), new { applicationId, haveCostsChanged });
    }

    public async Task<GetClosingReportReviewPaymentOverviewResult> GetClosingReportReviewPaymentOverview(Guid applicationId)
    {
        return await _connection.QuerySingleOrDefaultAsync<GetClosingReportReviewPaymentOverviewResult>(nameof(GetClosingReportReviewPaymentOverview), new { applicationId });
    }

    public async Task UpdateClosingReportReasonForChange(Guid applicationId, string reasonForChange)
    {
        await _connection.ExecuteAsync(nameof(UpdateClosingReportReasonForChange), new { applicationId, reasonForChange });
    }

    public async Task<decimal> GetClosingReportScheduledAmount(Guid applicationId)
    {
        return await _connection.QuerySingleOrDefaultAsync<decimal>(nameof(GetClosingReportScheduledAmount), new { applicationId });
    }

    public Task<bool> GetSubContractorsRequired(Guid applicationId)
    {
        return _connection.QuerySingleOrDefaultAsync<bool>(nameof(GetSubContractorsRequired), new { applicationId });
    }

    public async Task<int> GetClosingReportProjectDuration(Guid applicationId)
    {
        return await _connection.QuerySingleOrDefaultAsync<int>(nameof(GetClosingReportProjectDuration), new { applicationId });
    }

    public async Task<Guid?> GetPaymentRequestIDForClosingReport(Guid applicationId)
    {
        return await _connection.QuerySingleOrDefaultAsync<Guid?>(nameof(GetPaymentRequestIDForClosingReport), new
        {
            ApplicationId = applicationId
        });
    }

    public async Task<decimal> GetClosingReportAllowedFinalPaymentAmount(Guid applicationId)
    {
        return await _connection.QuerySingleOrDefaultAsync<decimal>(nameof(GetClosingReportAllowedFinalPaymentAmount), new
        {
            ApplicationId = applicationId
        });
    }
}