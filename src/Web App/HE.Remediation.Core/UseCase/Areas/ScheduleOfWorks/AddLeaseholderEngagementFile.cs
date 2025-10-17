using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Settings;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Transactions;
using Microsoft.Extensions.Options;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks;

public class AddLeaseholderEngagementFileHandler : IRequestHandler<AddLeaseholderEngagementFileRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;
    private readonly FileServiceSettings _fileServiceSettings;

    public AddLeaseholderEngagementFileHandler(
        IApplicationDataProvider applicationDataProvider, 
        IFileService fileService, 
        IFileRepository fileRepository, 
        IScheduleOfWorksRepository scheduleOfWorksRepository,
        IOptions<FileServiceSettings> fileServiceSettings)
    {
        _applicationDataProvider = applicationDataProvider;
        _fileService = fileService;
        _fileRepository = fileRepository;
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
        _fileServiceSettings = fileServiceSettings.Value;
    }

    public async Task<Unit> Handle(AddLeaseholderEngagementFileRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var processFileResult = await _fileService.ProcessFile(request.File, _fileServiceSettings.ScheduleOfWorksLeaseholderEngagement);

        if (processFileResult is null)
        {
            throw new InvalidFileException("No file selected");
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _fileRepository.InsertFile(new InsertFileParameters
        {
            Id = processFileResult.FileId,
            Extension = Path.GetExtension(request.File.FileName),
            MimeType = processFileResult.MimeType,
            Name = request.File.FileName,
            Size = request.File.Length
        });

        await _scheduleOfWorksRepository.InsertLeaseholderEngagement(new InsertLeaseholderEngagementParameters
        {
            ApplicationId = applicationId,
            FileId = processFileResult.FileId
        });

        scope.Complete();

        return Unit.Value;
    }
}

public class AddLeaseholderEngagementFileRequest : IRequest
{
    public IFormFile File { get; set; }
}