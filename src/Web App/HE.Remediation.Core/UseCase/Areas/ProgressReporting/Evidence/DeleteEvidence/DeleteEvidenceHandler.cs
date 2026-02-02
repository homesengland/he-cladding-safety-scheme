using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.Evidence.DeleteEvidence;

public class DeleteEvidenceHandler : IRequestHandler<DeleteEvidenceRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingRepository _progressReportingRepository;
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;

    public DeleteEvidenceHandler(
        IApplicationDataProvider applicationDataProvider, 
        IProgressReportingRepository progressReportingRepository, 
        IFileService fileService, 
        IFileRepository fileRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingRepository = progressReportingRepository;
        _fileService = fileService;
        _fileRepository = fileRepository;
    }

    public async ValueTask<Unit> Handle(DeleteEvidenceRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _progressReportingRepository.RemoveProgressReportLeaseholderInformationDocument(
            new RemoveProgressReportLeaseholderInformationDocumentParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId,
                FileId = request.FileId
            });

        var result = await _fileRepository.DeleteFile(request.FileId);

        await _fileService.DeleteFile($"{request.FileId}{result.Extension}");

        scope.Complete();

        return Unit.Value;
    }
}
