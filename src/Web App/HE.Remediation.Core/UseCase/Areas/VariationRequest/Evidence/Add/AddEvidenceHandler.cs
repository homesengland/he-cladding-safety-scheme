using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Settings;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Evidence.Add;

public class AddEvidenceHandler : IRequestHandler<AddEvidenceRequest, Unit>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;
    private readonly FileServiceSettings _fileServiceSettings;
    private readonly IVariationRequestRepository _variationRequestRepository;

    public AddEvidenceHandler(IApplicationDataProvider applicationDataProvider,
                                   IFileService fileService,
                                   IFileRepository fileRepository,
                                   IOptions<FileServiceSettings> fileServiceSettings,
                                   IVariationRequestRepository variationRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fileService = fileService;
        _fileRepository = fileRepository;
        _fileServiceSettings = fileServiceSettings.Value;
        _variationRequestRepository = variationRequestRepository;
    }

    public async ValueTask<Unit> Handle(AddEvidenceRequest request, CancellationToken cancellationToken)
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

        await _variationRequestRepository.InsertEvidence(processFileResult.FileId);

        var applicationId = _applicationDataProvider.GetApplicationId();

        scope.Complete();

        return Unit.Value;
    }

    private async ValueTask<ProcessFileResult> ProcessFile(IFormFile file)
    {
        if (file == null)
        {
            throw new InvalidFileException("No file selected");
        }

        return await _fileService.ProcessFile(file, _fileServiceSettings.VariationRequestEvidence);
    }
}
