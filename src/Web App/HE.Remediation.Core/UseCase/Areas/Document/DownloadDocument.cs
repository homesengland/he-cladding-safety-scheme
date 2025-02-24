using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Document;

public class DownloadDocumentHandler : IRequestHandler<DownloadDocumentRequest, DownloadDocumentResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IDocumentRepository _documentRepository;
    private readonly IFileService _fileService;

    public DownloadDocumentHandler(IApplicationDataProvider applicationDataProvider, IDocumentRepository documentRepository, IFileService fileService)
    {
        _applicationDataProvider = applicationDataProvider;
        _documentRepository = documentRepository;
        _fileService = fileService;
    }

    public async Task<DownloadDocumentResponse> Handle(DownloadDocumentRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var applicationId = _applicationDataProvider.GetApplicationId();
        var file = await _documentRepository.GetApplicationFile(new GetApplicationFileParameters
        {
            ApplicationId = applicationId,
            FileId = request.FileId
        });

        if (file is null)
        {
            throw new EntityNotFoundException("File not found");
        }

        var fileResult = await _fileService.GetFile(file.Id, file.Extension);

        return new DownloadDocumentResponse
        {
            File = fileResult.FileBytes,
            FileName = file.Name,
            ContentType = fileResult.ContentType
        };
    }
}

public class DownloadDocumentRequest : IRequest<DownloadDocumentResponse>
{
    public Guid FileId { get; set; }
}

public class DownloadDocumentResponse
{
    public byte[] File { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
}