using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Settings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProgrammePlan;

public class SetUploadProjectPlanHandler : IRequestHandler<SetUploadProjectPlanRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;
    private readonly IWorkPackageRepository _workPackageRepository;
    private readonly FileServiceSettings _fileServiceSettings;

    public SetUploadProjectPlanHandler(
        IApplicationDataProvider applicationDataProvider, 
        IFileService fileService, 
        IFileRepository fileRepository, 
        IWorkPackageRepository workPackageRepository, 
        IOptions<FileServiceSettings> fileServiceSettings)
    {
        _applicationDataProvider = applicationDataProvider;
        _fileService = fileService;
        _fileRepository = fileRepository;
        _workPackageRepository = workPackageRepository;
        _fileServiceSettings = fileServiceSettings.Value;
    }

    public async Task<Unit> Handle(SetUploadProjectPlanRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var file = await _fileService.ProcessFile(request.File, _fileServiceSettings.ProjectPlan);

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        
        await _fileRepository.InsertFile(new InsertFileParameters
        {
            Id = file.FileId,
            Name = request.File.FileName,
            Extension = Path.GetExtension(request.File.FileName),
            MimeType = file.MimeType,
            Size = request.File.Length
        });

        await _workPackageRepository.InsertProgrammePlanDocument(new InsertProgrammePlanDocumentParameters
        {
            ApplicationId = applicationId,
            FileId = file.FileId
        });

        scope.Complete();

        return Unit.Value;
    }
}

public class SetUploadProjectPlanRequest : IRequest
{
    public IFormFile File { get; set; }
}