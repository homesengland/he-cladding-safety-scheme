using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults.ClosingReport;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.TaskList.GetTaskList;

public class GetTaskListHandler : IRequestHandler<GetTaskListRequest, GetTaskListResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IClosingReportRepository _closingReportRepository;

    public GetTaskListHandler(IApplicationDataProvider applicationDataProvider,
                              IBuildingDetailsRepository buildingDetailsRepository,
                              IApplicationRepository applicationRepository,
                              IClosingReportRepository closingReportRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _closingReportRepository = closingReportRepository;
    }

    public async Task<GetTaskListResponse> Handle(GetTaskListRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var tasks = await _closingReportRepository.GetClosingReportTaskStatus(applicationId);

        return new GetTaskListResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            TasksWithStatuses = [.. tasks.Select(t => new GetTaskListResponse.TaskWithStatus(t.ClosingReportTask, t.TaskStatus))],
            IsSubmitted = false
        };
    }
}