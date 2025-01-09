using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Services.FileService;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Evidence.Delete;

public class DeleteEvidenceHandler : IRequestHandler<DeleteEvidenceRequest>
{
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;
    private readonly IVariationRequestRepository _variationRequestRepository;

    public DeleteEvidenceHandler(IFileService fileService,
                                 IFileRepository fileRepository,
                                 IVariationRequestRepository variationRequestRepository)
    {
        _fileService = fileService;
        _fileRepository = fileRepository;
        _variationRequestRepository = variationRequestRepository;
    }

    public async Task<Unit> Handle(DeleteEvidenceRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _variationRequestRepository.DeleteEvidence(request.FileId);
        var result = await _fileRepository.DeleteFile(request.FileId);

        await _fileService.DeleteFile($"{request.FileId}{result.Extension}");
        scope.Complete();

        return Unit.Value;
    }
}
