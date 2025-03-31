using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProgrammePlan;

public class DeleteProjectPlanHandler : IRequestHandler<DeleteProjectPlanRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public DeleteProjectPlanHandler(
        IApplicationDataProvider applicationDataProvider, 
        IFileService fileService, 
        IFileRepository fileRepository, 
        IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fileService = fileService;
        _fileRepository = fileRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async Task<Unit> Handle(DeleteProjectPlanRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var deleteResult = await _fileRepository.DeleteFile(request.FileId);
        await _fileService.DeleteFile($"{request.FileId}{deleteResult.Extension}");

        await _workPackageRepository.DeleteProgrammePlanDocument(new DeleteProgrammePlanDocumentParameters
        {
            ApplicationId = applicationId,
            FileId = request.FileId
        });

        scope.Complete();

        return Unit.Value;
    }
}

public class DeleteProjectPlanRequest : IRequest
{
    public Guid FileId { get; set; }
}