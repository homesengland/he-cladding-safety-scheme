using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Settings;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RightToManage;

public class AddRightToManageEvidenceHandler : IRequestHandler<AddRightToManageEvidenceRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IRightToManageRepository _rightToManageRepository;
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;
    private readonly FileServiceSettings _settings;

    public AddRightToManageEvidenceHandler(
        IApplicationDataProvider applicationDataProvider, 
        IRightToManageRepository rightToManageRepository, 
        IFileService fileService, 
        IFileRepository fileRepository,
        IOptions<FileServiceSettings> settings)
    {
        _applicationDataProvider = applicationDataProvider;
        _rightToManageRepository = rightToManageRepository;
        _fileService = fileService;
        _fileRepository = fileRepository;
        _settings = settings.Value;
    }

    public async ValueTask<Unit> Handle(AddRightToManageEvidenceRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var fileResult = await _fileService.ProcessFile(request.File, _settings.RtmEvidence);

        await _fileRepository.InsertFile(new InsertFileParameters
        {
            Id = fileResult.FileId,
            Name = request.File.FileName,
            Extension = Path.GetExtension(request.File.FileName),
            Size = request.File.Length,
            MimeType = fileResult.MimeType
        });

        await _rightToManageRepository.AddRightToManageEvidence(new AddRightToManageEvidenceParameters
        {
            ApplicationId = applicationId,
            FileId = fileResult.FileId
        });

        scope.Complete();
        return Unit.Value;
    }
}

public class AddRightToManageEvidenceRequest : IRequest
{
    public IFormFile File { get; set; }
}