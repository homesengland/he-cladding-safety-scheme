using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Settings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.AddFile;

public class AddFileHandler : IRequestHandler<AddFileRequest, Unit>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;
    private readonly FileServiceSettings _fileServiceSettings;
    private readonly IClosingReportRepository _closingRequestRepository;


    public AddFileHandler(IApplicationDataProvider applicationDataProvider,
        IFileService fileService,
        IFileRepository fileRepository,
        IOptions<FileServiceSettings> fileServiceSettings,
        IClosingReportRepository closingRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fileService = fileService;
        _fileRepository = fileRepository;
        _fileServiceSettings = fileServiceSettings.Value;
        _closingRequestRepository = closingRequestRepository;
    }

    public async Task<Unit> Handle(AddFileRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var processFileResult = await ProcessFile(request.File);

        await _fileRepository.InsertFile(new InsertFileParameters
        {
            Id = processFileResult.FileId,
            Extension = Path.GetExtension(request.File.FileName),
            MimeType = processFileResult.MimeType,
            Name = request.File.FileName,
            Size = request.File.Length
        });

        var applicationId = _applicationDataProvider.GetApplicationId();
        await _closingRequestRepository.InsertFile(applicationId, processFileResult.FileId, request.UploadType);

        scope.Complete();

        return Unit.Value;
    }

    private async Task<ProcessFileResult> ProcessFile(IFormFile file)
    {
        if (file == null)
        {
            throw new InvalidFileException("No file selected");
        }

        return await _fileService.ProcessFile(file, _fileServiceSettings.ClosingReport);
    }
}