using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using Mediator;
using System.Transactions;
using HE.Remediation.Core.Data.StoredProcedureParameters;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

internal class ResetResponsibleEntitiesSectionHandler : IRequestHandler<ResetResponsibleEntitiesSectionRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IFileService _fileService;
    private readonly IResponsibleEntityRepository _responsibleEntityRepository;
    private readonly IBankDetailsRepository _bankDetailsRepository;
    private readonly IRightToManageRepository _rightToManageRepository;
    private readonly IFileRepository _fileRepository;

    public ResetResponsibleEntitiesSectionHandler(
        IApplicationDataProvider applicationDataProvider,
        IApplicationRepository applicationRepository,
        IFileService fileService,
        IResponsibleEntityRepository responsibleEntityRepository,
        IBankDetailsRepository bankDetailsRepository, 
        IRightToManageRepository rightToManageRepository, 
        IFileRepository fileRepository)
    {
        _applicationDataProvider =
            applicationDataProvider ?? throw new ArgumentNullException(nameof(applicationDataProvider));
        _applicationRepository =
            applicationRepository ?? throw new ArgumentNullException(nameof(applicationRepository));
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        _responsibleEntityRepository = responsibleEntityRepository ??
                                       throw new ArgumentNullException(nameof(responsibleEntityRepository));
        _bankDetailsRepository = bankDetailsRepository;
        _rightToManageRepository = rightToManageRepository;
        _fileRepository = fileRepository;
    }

    public async ValueTask<Unit> Handle(ResetResponsibleEntitiesSectionRequest request, CancellationToken cancellationToken)
    {
        var applicationId = GetApplicationId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await DeleteEvidenceFiles(applicationId);

        await DeleteRightToManageEvidenceFiles(applicationId);

        await ResetResponsibleEntitiesSection(applicationId);

        await ResetBankDetailsSection(applicationId);
        
        scope.Complete();

        return Unit.Value;
    }

    private Guid GetApplicationId()
    {
        return _applicationDataProvider.GetApplicationId();
    }

    private async Task DeleteEvidenceFiles(Guid applicationId)
    {
        var evidenceFiles = await _applicationRepository.GetResponsibleEntityEvidenceFiles(applicationId);

        var deleteFileTasks = evidenceFiles
            .Select(f => f.Id + f.Extension)
            .Select(_fileService.DeleteFile);

        await Task.WhenAll(deleteFileTasks);
    }

    private async Task DeleteRightToManageEvidenceFiles(Guid applicationId)
    {
        var files = await _rightToManageRepository.GetRightToManageEvidence(applicationId);

        foreach (var file in files)
        {
            await _rightToManageRepository.DeleteRightToManageEvidence(new DeleteRightToManageEvidenceParameters
            {
                ApplicationId = applicationId,
                FileId = file.Id
            });

            await _fileRepository.DeleteFile(file.Id);
            await _fileService.DeleteFile(file.Id + file.Extension);

        }
    }

    private async Task ResetResponsibleEntitiesSection(Guid applicationId)
    {
        await _responsibleEntityRepository.ResetResponsibleEntitiesSection(applicationId);
    }

    private async Task ResetBankDetailsSection(Guid applicationId)
    {
        await _bankDetailsRepository.ResetBankDetails(applicationId);
    }
}

public class ResetResponsibleEntitiesSectionRequest : IRequest
{
}