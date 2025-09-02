using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Services.FileService;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.UploadFileEvidence;

public class DeleteThirdPartyEvidenceFileHandler : IRequestHandler<DeleteThirdPartyEvidenceFileRequest, Unit>
{
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;
    private readonly IEvidenceOfThirdPartyContributionRepository _thirdPartyEvidenceRepository;

    public DeleteThirdPartyEvidenceFileHandler(
        IFileService fileService,
        IFileRepository fileRepository,
        IEvidenceOfThirdPartyContributionRepository thirdPartyEvidenceRepository)
    {
        _fileService = fileService;
        _fileRepository = fileRepository;
        _thirdPartyEvidenceRepository = thirdPartyEvidenceRepository;
    }

    public async Task<Unit> Handle(DeleteThirdPartyEvidenceFileRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        // delete file reference
        await _thirdPartyEvidenceRepository.DeleteThirdPartyEvidenceFile(request.ApplicationId, request.FileId, request.Id);

        // delete physical file
        var result = await _fileRepository.DeleteFile(request.FileId);
        await _fileService.DeleteFile($"{request.FileId}{result.Extension}");

        scope.Complete();

        return Unit.Value;
    }

}