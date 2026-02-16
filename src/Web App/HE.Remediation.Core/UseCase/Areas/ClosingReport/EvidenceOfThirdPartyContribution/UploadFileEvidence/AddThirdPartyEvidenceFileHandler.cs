using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Settings;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.AddFile;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.UploadFileEvidence;

public class AddThirdPartyEvidenceFileHandler : IRequestHandler<AddThirdPartyEvidenceFileRequest, Unit>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;
    private readonly FileServiceSettings _fileServiceSettings;
    private readonly IEvidenceOfThirdPartyContributionRepository _thirdPartyEvidenceRepository;


    public AddThirdPartyEvidenceFileHandler(IApplicationDataProvider applicationDataProvider,
        IFileService fileService,
        IFileRepository fileRepository,
        IOptions<FileServiceSettings> fileServiceSettings,
        IEvidenceOfThirdPartyContributionRepository thirdPartyEvidenceRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fileService = fileService;
        _fileRepository = fileRepository;
        _fileServiceSettings = fileServiceSettings.Value;
        _thirdPartyEvidenceRepository = thirdPartyEvidenceRepository;
    }

    public async ValueTask<Unit> Handle(AddThirdPartyEvidenceFileRequest request, CancellationToken cancellationToken)
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

        await _thirdPartyEvidenceRepository.InsertThirdPartyEvidenceFile(request.ApplicationId, processFileResult.FileId, request.Id);

        scope.Complete();

        return Unit.Value;
    }

    private async ValueTask<ProcessFileResult> ProcessFile(IFormFile file)
    {
        if (file == null)
        {
            throw new InvalidFileException("No file selected");
        }

        return await _fileService.ProcessFile(file, _fileServiceSettings.ClosingReport);
    }
}