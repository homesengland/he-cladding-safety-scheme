using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectPlan;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectPlan;

public class DeleteUploadProjectPlanHandler : IRequestHandler<DeleteUploadProjectPlanRequest>
{

    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectPlanRepository _projectPlanRepository;
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;

    public DeleteUploadProjectPlanHandler(
        IApplicationDataProvider applicationDataProvider, 
        IProgressReportingProjectPlanRepository projectPlanRepository, 
        IFileService fileService, 
        IFileRepository fileRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _projectPlanRepository = projectPlanRepository;
        _fileService = fileService;
        _fileRepository = fileRepository;
    }

    public async Task<Unit> Handle(DeleteUploadProjectPlanRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var parameters = new MonthlyProgressReportDeleteUploadProjectPlanParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId,
                FileId = request.FileId
            };
            await _projectPlanRepository.DeleteMonthlyProgressReportUploadProjectPlanFile(parameters);
            var result = await _fileRepository.DeleteFile(request.FileId);

            await _fileService.DeleteFile($"{request.FileId}{result.Extension}");
            scope.Complete();
        }
        return Unit.Value;
    }
}

public class DeleteUploadProjectPlanRequest : IRequest
{
    public Guid FileId { get; set; }
}