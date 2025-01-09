using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using MediatR;
using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

public class DeleteResponsibleEntitiesEvidenceHandler : IRequestHandler<DeleteResponsibleEntitiesEvidenceRequest>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;

    public DeleteResponsibleEntitiesEvidenceHandler(IDbConnectionWrapper connection, IFileService fileService, IFileRepository fileRepository)
    {
        _connection = connection;
        _fileService = fileService;
        _fileRepository = fileRepository;
    }

    public async Task<Unit> Handle(DeleteResponsibleEntitiesEvidenceRequest request, CancellationToken cancellationToken)
    {
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _connection.ExecuteAsync("DeleteResponsibleEntitiesEvidence", new
            {
                request.FileId
            });
            var result = await _fileRepository.DeleteFile(request.FileId);

            await _fileService.DeleteFile($"{request.FileId}{result.Extension}");
            scope.Complete();
        }
        return Unit.Value;
    }
}

public class DeleteResponsibleEntitiesEvidenceRequest : IRequest
{
    public Guid FileId { get; set; }
    public string ReturnUrl { get; set; }
    public EResponsibleEntityUploadType UploadType { get; set; }
}