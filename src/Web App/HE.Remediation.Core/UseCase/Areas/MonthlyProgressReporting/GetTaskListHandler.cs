using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting;
public class GetTaskListHandler : IRequestHandler<GetTaskListRequest, GetTaskListResponse>
{
    private readonly IApplicationDetailsProvider _applicationDetailsProvider;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IMonthlyProgressReportingRepository _monthlyProgressReportingRepository;

    public GetTaskListHandler(
        IApplicationDetailsProvider applicationDetailsProvider, 
        IApplicationDataProvider applicationDataProvider, 
        IMonthlyProgressReportingRepository monthlyProgressReportingRepository)
    {
        _applicationDetailsProvider = applicationDetailsProvider;
        _applicationDataProvider = applicationDataProvider;
        _monthlyProgressReportingRepository = monthlyProgressReportingRepository;
    }

    public async ValueTask<GetTaskListResponse> Handle(GetTaskListRequest request, CancellationToken cancellationToken)
    {
        var details = await _applicationDetailsProvider.GetApplicationDetails();

        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var progressReport = await _monthlyProgressReportingRepository.GetMonthlyProgressReport(
            new GetMonthlyProgressReportParameters
            {
                ApplicationId = details.ApplicationId,
                ProgressReportId = progressReportId
            });

        return new GetTaskListResponse
        {
            BuildingName = details.BuildingName,
            ApplicationReferenceNumber = details.ApplicationReferenceNumber,
            DateCreated = progressReport.DateCreated,
            DateDue = progressReport.DateDue,
            DateSubmitted = progressReport.DateSubmitted,
            Version = progressReport.Version,
            TaskStatusId = progressReport.TaskStatusId,
            KeyDatesTaskStatusId = progressReport.KeyDatesTaskStatusId,
            ProjectTeamTaskStatusId = progressReport.ProjectTeamTaskStatusId,
            ProjectPlanTaskStatusId = progressReport.ProjectPlanTaskStatusId,
            LeaseholdersTaskStatusId = progressReport.LeaseholdersTaskStatusId,
            SupportTaskStatusId = progressReport.SupportTaskStatusId
        };
    }
}

public class GetTaskListRequest() : IRequest<GetTaskListResponse>
{
}

public class GetTaskListResponse
{
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateDue { get; set; }
    public DateTime? DateSubmitted { get; set; }
    public int Version { get; set; }
    public ETaskStatus TaskStatusId { get; set; }

    public ETaskStatus KeyDatesTaskStatusId { get; set; }
    public ETaskStatus ProjectTeamTaskStatusId { get; set; }
    public ETaskStatus ProjectPlanTaskStatusId { get; set; }
    public ETaskStatus LeaseholdersTaskStatusId { get; set; }
    public ETaskStatus SupportTaskStatusId { get; set; }
}