using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.DeleteFile;

public class DeleteFileHandler : IRequestHandler<DeleteFileRequest>
{
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;
    private readonly IClosingReportRepository _closingRequestRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public DeleteFileHandler(IFileService fileService,
        IFileRepository fileRepository,
        IClosingReportRepository closingRequestRepository,
        IApplicationDataProvider applicationDataProvider)
    {
        _fileService = fileService;
        _fileRepository = fileRepository;
        _closingRequestRepository = closingRequestRepository;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<Unit> Handle(DeleteFileRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var remainingFileCount = await _closingRequestRepository.DeleteFile(request.FileId);
        var result = await _fileRepository.DeleteFile(request.FileId);

        await _fileService.DeleteFile($"{request.FileId}{result.Extension}");

        // if no files left revert task to InProgress
        if(remainingFileCount == 0)
        {
            await _closingRequestRepository.UpsertClosingReportTaskStatus(applicationId, request.Task, ETaskStatus.InProgress, allowRevert: true);
        }

        scope.Complete();

        return Unit.Value;
    }
}