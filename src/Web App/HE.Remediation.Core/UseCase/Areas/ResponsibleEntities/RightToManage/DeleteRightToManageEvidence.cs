using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RightToManage;

public class DeleteRightToManageEvidenceHandler : IRequestHandler<DeleteRightToManageEvidenceRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IRightToManageRepository _rightToManageRepository;
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;

    public DeleteRightToManageEvidenceHandler(
        IApplicationDataProvider applicationDataProvider, 
        IRightToManageRepository rightToManageRepository, 
        IFileService fileService, 
        IFileRepository fileRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _rightToManageRepository = rightToManageRepository;
        _fileService = fileService;
        _fileRepository = fileRepository;
    }

    public async Task<Unit> Handle(DeleteRightToManageEvidenceRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var applicationId = _applicationDataProvider.GetApplicationId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _rightToManageRepository.DeleteRightToManageEvidence(new DeleteRightToManageEvidenceParameters
        {
            ApplicationId = applicationId,
            FileId = request.FileId
        });

        var deleteResult = await _fileRepository.DeleteFile(request.FileId);

        await _fileService.DeleteFile($"{request.FileId}{deleteResult.Extension}");

        scope.Complete();

        return Unit.Value;
    }
}

public class DeleteRightToManageEvidenceRequest : IRequest
{
    public Guid FileId { get; set; }
}