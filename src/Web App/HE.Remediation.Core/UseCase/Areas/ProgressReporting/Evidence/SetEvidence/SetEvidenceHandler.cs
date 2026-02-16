using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Settings;
using Mediator;
using Microsoft.Extensions.Options;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.Evidence.SetEvidence;

public class SetEvidenceHandler : IRequestHandler<SetEvidenceRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;
    private readonly IFileRepository _fileRepository;
    private readonly IFileService _fileService;
    private readonly FileServiceSettings _fileServiceSettings;

    public SetEvidenceHandler(
        IProgressReportingRepository progressReportingRepository,
        IFileRepository fileRepository,
        IFileService fileService,
        IOptions<FileServiceSettings> fileServiceSettings)
    {
        _progressReportingRepository = progressReportingRepository;
        _fileRepository = fileRepository;
        _fileService = fileService;
        _fileServiceSettings = fileServiceSettings.Value;
    }

    public async ValueTask<Unit> Handle(SetEvidenceRequest request, CancellationToken cancellationToken)
    {
        await ValidateFile(request);

        if (request.File is not null)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var fileResult = await _fileService.ProcessFile(request.File, _fileServiceSettings.ProgressReportEvidence);

            await _fileRepository.InsertFile(new InsertFileParameters
            {
                Id = fileResult.FileId,
                Extension = Path.GetExtension(request.File.FileName),
                MimeType = fileResult.MimeType,
                Name = request.File.FileName,
                Size = request.File.Length
            });

            await _progressReportingRepository.UpdateProgressReportLeaseholdersInformedFileId(fileResult.FileId);

            scope.Complete();
        }

        return Unit.Value;
    }

    private async Task ValidateFile(SetEvidenceRequest request)
    {
        var existingFiles = await _progressReportingRepository.GetProgressReportLeaseholdersInformedFiles();

        if (existingFiles.Count == 0 && request.File is null)
        {
            throw new InvalidFileException("File is required", nameof(request.File));
        }
    }
}
