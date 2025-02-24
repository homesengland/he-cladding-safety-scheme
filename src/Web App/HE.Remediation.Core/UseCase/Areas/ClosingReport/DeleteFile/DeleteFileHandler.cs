using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Services.FileService;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.DeleteFile;

public class DeleteFileHandler : IRequestHandler<DeleteFileRequest>
{
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;
    private readonly IClosingReportRepository _closingRequestRepository;

    public DeleteFileHandler(IFileService fileService,
        IFileRepository fileRepository,
        IClosingReportRepository closingRequestRepository)
    {
        _fileService = fileService;
        _fileRepository = fileRepository;
        _closingRequestRepository = closingRequestRepository;
    }

    public async Task<Unit> Handle(DeleteFileRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _closingRequestRepository.DeleteFile(request.FileId);
        var result = await _fileRepository.DeleteFile(request.FileId);

        await _fileService.DeleteFile($"{request.FileId}{result.Extension}");
        scope.Complete();

        return Unit.Value;
    }
}