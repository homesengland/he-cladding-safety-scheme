using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.Leaseholders;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.Leaseholders;

namespace HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;

public interface IProgressReportingLeaseholdersRepository
{
    // GET DATA

    Task<GetProgressReportLeaseholderCommunicationResult> GetProgressReportLeaseholderCommunication(GetProgressReportLeaseholderCommunicationParameters parameters);
    Task<IReadOnlyCollection<FileResult>> GetProgressReportLeaseholderCommunicationFiles(GetProgressReportLeaseholderCommunicationParameters parameters);

    // SET DATA

    Task SetProgressReportLeaseholderCommunication(SetProgressReportLeaseholderCommunicationParameters parameters);
    Task SetProgressReportLeaseholderDate(SetProgressReportLeaseholderDateParameters parameters);
    Task SetLeaseholderCommunicationEvidenceFile(SetUploadEvidenceParameters parameters);
    Task SetProgressReportLeaseholderCheckYourAnswers(SetCheckYourAnswersParameters parameters);

    // DELETE DATA

    Task DeleteLeaseholderCommunicationEvidenceFile(DeleteEvidenceFileParameters fileId);

}