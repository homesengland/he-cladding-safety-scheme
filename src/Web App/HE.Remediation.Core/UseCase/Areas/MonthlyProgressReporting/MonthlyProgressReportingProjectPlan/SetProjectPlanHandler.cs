using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectPlan;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectPlan;

public class SetProjectPlanHandler : IRequestHandler<SetProjectPlanRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFileRepository _fileRepository;
    private readonly IFileService _fileService;
    private readonly IProgressReportingProjectPlanRepository _projectPlanRepository;
    private readonly IMonthlyProgressReportingRepository _monthlyProgressReportingRepository;

    public SetProjectPlanHandler(
        IApplicationDataProvider applicationDataProvider, 
        IFileRepository fileRepository, 
        IFileService fileService, 
        IProgressReportingProjectPlanRepository projectPlanRepository, 
        IMonthlyProgressReportingRepository monthlyProgressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fileRepository = fileRepository;
        _fileService = fileService;
        _projectPlanRepository = projectPlanRepository;
        _monthlyProgressReportingRepository = monthlyProgressReportingRepository;
    }

    public async ValueTask<Unit> Handle(SetProjectPlanRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var parameters = new SetProjectPlanParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            RemainingAmount = request.RemainingAmount,
            EnoughFunds = request.EnoughFunds,
            IntentToProceedType = (int?)request.IntentToProceedType,
            InternalAdditionalWork = request.InternalAdditionalWork
        };

        await _projectPlanRepository.SetProjectPlanDetails(parameters);

        if (request.EnoughFunds == true)
        {
            await DeletePtsUplift(applicationId, progressReportId);
        }

        await _projectPlanRepository.SetProjectPlanTaskStatus(new SetProjectPlanTaskStatusParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            TaskStatusId = (int)ETaskStatus.InProgress
        });

        await _monthlyProgressReportingRepository.SetMonthlyReportStatus(new SetMonthlyReportStatusParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            TaskStatusId = (int)ETaskStatus.InProgress
        });

        scope.Complete();
        return Unit.Value;
    }

    private async Task DeletePtsUplift(Guid applicationId, Guid progressReportId)
    {
        var files = await _projectPlanRepository.GetPtsUpliftDocument(new GetPtsUpliftDocumentParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });

        foreach (var file in files)
        {
            await _projectPlanRepository.DeletePtsUpliftDocument(new DeletePtsUpliftDocumentParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId,
                FileId = file.Id
            });

            var deleteResult = await _fileRepository.DeleteFile(file.Id);

            await _fileService.DeleteFile($"{file.Id}{deleteResult.Extension}");
        }
    }
}

public class SetProjectPlanRequest : IRequest
{
    public EIntentToProceedType? IntentToProceedType { get; set; }
    public decimal? RemainingAmount { get; set; }
    public bool? EnoughFunds { get; set; }
    public bool? InternalAdditionalWork { get; set; }
}