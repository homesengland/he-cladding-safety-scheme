using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.Leaseholders;
using HE.Remediation.Core.Services.FileService;
using Microsoft.Extensions.Logging;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.Leaseholders;

public abstract class DeleteUploadEvidenceService
{
    private readonly IFileRepository _fileRepository;
    private readonly IFileService _fileService;
    private readonly IProgressReportingLeaseholdersRepository _progressReportingLeaseholdersRepository;
    private readonly ILogger _logger;

    public DeleteUploadEvidenceService(
        IFileRepository fileRepository,
        IFileService fileService,
        IProgressReportingLeaseholdersRepository progressReportingLeaseholdersRepository, 
        ILogger logger)
    {
        _fileRepository = fileRepository;
        _fileService = fileService;
        _progressReportingLeaseholdersRepository = progressReportingLeaseholdersRepository;
        _logger = logger;
    }

    protected async Task DeleteFile(Guid applicationId, Guid progressReportId, Guid fileId)
    {
        try
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var parameters = new DeleteEvidenceFileParameters() { FileId = fileId, ApplicationId = applicationId, ProgressReportId = progressReportId };
            await _progressReportingLeaseholdersRepository.DeleteLeaseholderCommunicationEvidenceFile(parameters);
            var result = await _fileRepository.DeleteFile(fileId);
            await _fileService.DeleteFile($"{fileId}{result.Extension}");
            scope.Complete();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting file with id {FileId}", fileId);
        }
    }
}
