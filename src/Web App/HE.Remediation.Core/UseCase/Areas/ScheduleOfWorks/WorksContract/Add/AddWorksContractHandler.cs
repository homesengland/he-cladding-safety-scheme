using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Settings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Add;

public class AddWorksContractHandler : IRequestHandler<AddWorksContractRequest, Unit>
{
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;
    private readonly FileServiceSettings _fileServiceSettings;
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;

    public AddWorksContractHandler(IFileService fileService,
                                   IFileRepository fileRepository,
                                   IOptions<FileServiceSettings> fileServiceSettings,
                                   IScheduleOfWorksRepository scheduleOfWorksRepository)
    {
        _fileService = fileService;
        _fileRepository = fileRepository;
        _fileServiceSettings = fileServiceSettings.Value;
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
    }

    public async Task<Unit> Handle(AddWorksContractRequest request, CancellationToken cancellationToken)
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

        await _scheduleOfWorksRepository.InsertContract(processFileResult.FileId);

        scope.Complete();

        return Unit.Value;
    }

    private async Task<ProcessFileResult> ProcessFile(IFormFile file)
    {
        if (file == null)
        {
            throw new InvalidFileException("No file selected");
        }

        return await _fileService.ProcessFile(file, _fileServiceSettings.ScheduleOfWorksContract);
    }
}
