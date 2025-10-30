using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.StatusTransition;

using MediatR;


namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.TaskList.GetTaskList;

public class GetTaskListHandler : IRequestHandler<GetTaskListRequest, GetTaskListResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;
    private readonly IStatusTransitionService _statusTransitionService;

    public GetTaskListHandler(IApplicationDataProvider applicationDataProvider,
                              IBuildingDetailsRepository buildingDetailsRepository,
                              IApplicationRepository applicationRepository,
                               IScheduleOfWorksRepository scheduleOfWorksRepository,
                               IStatusTransitionService statusTransitionService)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
        _statusTransitionService = statusTransitionService;
    }

    public async Task<GetTaskListResponse> Handle(GetTaskListRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var hasScheduleOfWorks = await _scheduleOfWorksRepository.HasScheduleOfWorks();

        if (!hasScheduleOfWorks)
        {
            await _scheduleOfWorksRepository.InsertScheduleOfWorks();
        }

        var isSubmitted = await _scheduleOfWorksRepository.IsScheduleOfWorksSubmitted();

        if (!isSubmitted)
        {
            await _statusTransitionService.TransitionToStatus(EApplicationStatus.ScheduleOfWorksInProgress, applicationIds: applicationId);
        }

        var taskStatusesResult = await _scheduleOfWorksRepository.GetScheduleOfWorksTaskStatuses();


        return new GetTaskListResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            LeaseholderEngagementStatusId = taskStatusesResult?.LeaseholderEngagementStatusId ?? ETaskStatus.NotStarted,
            BuildingControlStatusId = taskStatusesResult?.BuildingControlStatusId ?? ETaskStatus.NotStarted,
            WorksContractStatusId = taskStatusesResult?.WorksContractStatusId ?? ETaskStatus.NotStarted,
            ProfileCostsStatusId = taskStatusesResult?.ProfileCostsStatusId ?? ETaskStatus.NotStarted,
            DeclarationStatusId = taskStatusesResult?.DeclarationStatusId ?? ETaskStatus.NotStarted,
        };
    }
}