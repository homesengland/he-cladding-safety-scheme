using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.Leaseholders;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.Leaseholders;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;

public class ProgressReportingLeaseholdersRepository : IProgressReportingLeaseholdersRepository
{
    private readonly IDbConnectionWrapper _dbConnectionWrapper;

    public ProgressReportingLeaseholdersRepository(IDbConnectionWrapper dbConnectionWrapper)
    {
        _dbConnectionWrapper = dbConnectionWrapper;
    }

    // GET DATA

    public async Task<GetProgressReportLeaseholderCommunicationResult> GetProgressReportLeaseholderCommunication(GetProgressReportLeaseholderCommunicationParameters parameters)
    {
        return await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetProgressReportLeaseholderCommunicationResult>(nameof(GetProgressReportLeaseholderCommunication), parameters);
    }

    public async Task<IReadOnlyCollection<FileResult>> GetProgressReportLeaseholderCommunicationFiles(GetProgressReportLeaseholderCommunicationParameters parameters)
    {
        return await _dbConnectionWrapper.QueryAsync<FileResult>(nameof(GetProgressReportLeaseholderCommunicationFiles), parameters);
    }

    // SET DATA

    public async Task SetProgressReportLeaseholderCommunication(SetProgressReportLeaseholderCommunicationParameters parameters)
    {
        await _dbConnectionWrapper.ExecuteAsync(nameof(SetProgressReportLeaseholderCommunication), parameters);
    }

    public async Task SetProgressReportLeaseholderDate(SetProgressReportLeaseholderDateParameters parameters)
    {
        await _dbConnectionWrapper.ExecuteAsync(nameof(SetProgressReportLeaseholderDate), parameters);
    }

    public async Task SetLeaseholderCommunicationEvidenceFile(SetUploadEvidenceParameters parameters)
    {
        await _dbConnectionWrapper.ExecuteAsync(nameof(SetLeaseholderCommunicationEvidenceFile), parameters);
    }

    public async Task SetProgressReportLeaseholderCheckYourAnswers(SetCheckYourAnswersParameters parameters)
    {
        await _dbConnectionWrapper.ExecuteAsync(nameof(SetProgressReportLeaseholderCheckYourAnswers), parameters);
    }

    // DELETE DATA

    public async Task DeleteLeaseholderCommunicationEvidenceFile(DeleteEvidenceFileParameters parameters)
    {
        await _dbConnectionWrapper.ExecuteAsync(nameof(DeleteLeaseholderCommunicationEvidenceFile), parameters);
    }
}