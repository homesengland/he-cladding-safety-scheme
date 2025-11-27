using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectPlan;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectPlan;

public class DeletePtsUpliftHandler : IRequestHandler<DeletePtsUpliftRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFileRepository _fileRepository;
    private readonly IFileService _fileService;
    private readonly IProgressReportingProjectPlanRepository _projectPlanRepository;

    public DeletePtsUpliftHandler(
        IApplicationDataProvider applicationDataProvider, 
        IFileRepository fileRepository, 
        IFileService fileService, 
        IProgressReportingProjectPlanRepository projectPlanRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fileRepository = fileRepository;
        _fileService = fileService;
        _projectPlanRepository = projectPlanRepository;
    }

    public async Task<Unit> Handle(DeletePtsUpliftRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _projectPlanRepository.DeletePtsUpliftDocument(new DeletePtsUpliftDocumentParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            FileId = request.FileId
        });

        var deleteResult = await _fileRepository.DeleteFile(request.FileId);

        await _fileService.DeleteFile($"{request.FileId}{deleteResult.Extension}");

        scope.Complete();

        return Unit.Value;
    }
}

public class DeletePtsUpliftRequest : IRequest
{
    public Guid FileId { get; set; }
}