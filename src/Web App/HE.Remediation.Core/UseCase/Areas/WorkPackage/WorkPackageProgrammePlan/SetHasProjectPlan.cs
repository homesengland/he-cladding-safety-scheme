using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProgrammePlan;

public class SetHasProjectPlanHandler : IRequestHandler<SetHasProjectPlanRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IWorkPackageRepository _workPackageRepository;
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;

    public SetHasProjectPlanHandler(
        IApplicationDataProvider applicationDataProvider, 
        IWorkPackageRepository workPackageRepository, 
        IFileService fileService, 
        IFileRepository fileRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _workPackageRepository = workPackageRepository;
        _fileService = fileService;
        _fileRepository = fileRepository;
    }

    public async ValueTask<Unit> Handle(SetHasProjectPlanRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var originalValue = await _workPackageRepository.GetHasProgrammePlan(applicationId);

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _workPackageRepository.SetHasProgrammePlan(new SetHasProgrammePlanParameters
        {
            ApplicationId = applicationId,
            HasProgrammePlan = request.HasProjectPlan!.Value
        });

        await CleanUpDocumentsIfNeeded(originalValue, request.HasProjectPlan!.Value, applicationId);

        scope.Complete();

        return Unit.Value;
    }

    private async Task CleanUpDocumentsIfNeeded(bool? originalValue, bool newValue, Guid applicationId)
    {
        if (originalValue == true && !newValue)
        {
            var files = await _workPackageRepository.GetProgrammePlanDocuments(applicationId);
            foreach (var file in files)
            {
                await DeleteFile(file.Id, applicationId);
            }
        }
    }

    private async Task DeleteFile(Guid fileId, Guid applicationId)
    {
        var deleteResult = await _fileRepository.DeleteFile(fileId);
        await _fileService.DeleteFile($"{fileId}{deleteResult.Extension}");
        await _workPackageRepository.DeleteProgrammePlanDocument(new DeleteProgrammePlanDocumentParameters
        {
            ApplicationId = applicationId,
            FileId = fileId
        });
    }
}

public class SetHasProjectPlanRequest : IRequest
{
    public bool? HasProjectPlan { get; set; }
}