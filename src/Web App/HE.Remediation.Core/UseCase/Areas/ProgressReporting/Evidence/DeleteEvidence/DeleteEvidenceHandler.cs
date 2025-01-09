using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Services.FileService;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.Evidence.DeleteEvidence;

public class DeleteEvidenceHandler : IRequestHandler<DeleteEvidenceRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;

    public DeleteEvidenceHandler(
        IProgressReportingRepository progressReportingRepository,
        IFileService fileService,
        IFileRepository fileRepository)
    {
        _progressReportingRepository = progressReportingRepository;
        _fileService = fileService;
        _fileRepository = fileRepository;
    }

    public async Task<Unit> Handle(DeleteEvidenceRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _progressReportingRepository.UpdateProgressReportLeaseholdersInformedFileId(null);

        var result = await _fileRepository.DeleteFile(request.FileId);

        await _fileService.DeleteFile($"{request.FileId}{result.Extension}");

        scope.Complete();

        return Unit.Value;
    }
}
