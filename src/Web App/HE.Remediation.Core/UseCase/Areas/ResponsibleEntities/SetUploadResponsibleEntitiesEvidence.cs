using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Settings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

public class SetUploadResponsibleEntitiesEvidenceHandler : IRequestHandler<SetUploadResponsibleEntitiesEvidenceRequest>
{
    private readonly IFileRepository _fileRepository;
    private readonly IFileService _fileService;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IDbConnectionWrapper _connection;
    private readonly FileServiceSettings _fileServiceSettings;

    public SetUploadResponsibleEntitiesEvidenceHandler(
        IFileRepository fileRepository, 
        IFileService fileService, 
        IApplicationDataProvider applicationDataProvider, 
        IDbConnectionWrapper connection,
        IOptions<FileServiceSettings> fileServiceSettings)
    {
        _fileRepository = fileRepository;
        _fileService = fileService;
        _applicationDataProvider = applicationDataProvider;
        _connection = connection;
        _fileServiceSettings = fileServiceSettings.Value;
    }

    public async Task<Unit> Handle(SetUploadResponsibleEntitiesEvidenceRequest request, CancellationToken cancellationToken)
    {
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {

            var fileResult = await _fileService.ProcessFile(request.File, _fileServiceSettings.ResponsibleEntitiesEvidence);
        
            await _fileRepository.InsertFile(new InsertFileParameters
            {
                Id = fileResult.FileId,
                Extension = Path.GetExtension(request.File.FileName),
                MimeType = fileResult.MimeType,
                Name = request.File.FileName,
                Size = request.File.Length
            });

            await _connection.ExecuteAsync("InsertResponsibleEntitiesEvidence", new
            {
                ApplicationId = _applicationDataProvider.GetApplicationId(),
                fileResult.FileId,
                request.UploadType
            });


            scope.Complete();
        }

        return Unit.Value;
    }
}

public class SetUploadResponsibleEntitiesEvidenceRequest : IRequest
{
    public IFormFile File { get; set; }
    public EResponsibleEntityUploadType UploadType { get; set; }
}