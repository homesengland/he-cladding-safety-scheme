using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.TaskList.GetTaskList;

public class GetTaskListHandler : IRequestHandler<GetTaskListRequest, GetTaskListResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetTaskListHandler(IApplicationDataProvider applicationDataProvider,
                              IBuildingDetailsRepository buildingDetailsRepository,
                              IApplicationRepository applicationRepository,
                              IProgressReportingRepository progressReportingRepository,
                              IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async Task<GetTaskListResponse> Handle(GetTaskListRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var workPackageTaskListSummary = await _workPackageRepository.GetWorkPackageTaskListSummary();

        var planningPermissionStatus = await SetStatusToInProgressIfHasSubmittedProgressReporRequirePlanningPermissio(workPackageTaskListSummary.WorkPackagePlanningPermissionStatusId);
        var projectTeamStatus = await SetStatusToInProgressIfHasSubmittedProgressReportTeamMembers(workPackageTaskListSummary.WorkPackageProjectTeamStatusId);

        return new GetTaskListResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            GrantCertifyingOfficerStatusId = workPackageTaskListSummary.WorkPackageGrantCertifyingOfficerStatusId,
            CostsScheduleStatusId = workPackageTaskListSummary.WorkPackageCostsScheduleStatusId,
            ThirdPartyContributionsStatusId = workPackageTaskListSummary.WorkPackageThirdPartyContributionsStatusId,
            DeclarationStatusId = workPackageTaskListSummary.WorkPackageDeclarationStatusId,
            DutyOfCareDeedStatusId = workPackageTaskListSummary.WorkPackageDutyOfCareDeedStatusId,
            DutyOfCareDeedSent = workPackageTaskListSummary.WorkPackageDutyOfCareDeedSent,
            ProjectTeamStatusId = projectTeamStatus,
            PlanningPermissionStatusId = planningPermissionStatus,
            KeyDatesStatusId = workPackageTaskListSummary.WorkPackageKeyDatesStatusId,
            SignatoriesStatusId = workPackageTaskListSummary.WorkPackageSignatoriesStatusId,
            ProgrammePlanStatusId = workPackageTaskListSummary.WorkPackageProgrammePlanStatusId,
            IsSubmitted = workPackageTaskListSummary.IsSubmitted
        };
    }

    private async Task<ETaskStatus> SetStatusToInProgressIfHasSubmittedProgressReporRequirePlanningPermissio(ETaskStatus taskStatus)
    {
        if (taskStatus == ETaskStatus.NotStarted)
        {
            var requirePlanningPermission = await _progressReportingRepository.GetLastSubmittedProgressReportRequirePlanningPermission();

            if (requirePlanningPermission is not null)
            {
                return ETaskStatus.InProgress;
            }
        }

        return taskStatus;
    }

    private async Task<ETaskStatus> SetStatusToInProgressIfHasSubmittedProgressReportTeamMembers(ETaskStatus taskStatus)
    {
        if (taskStatus == ETaskStatus.NotStarted)
        {
            var otherMembersAppointed = await _progressReportingRepository.GetLastSubmittedProgressReportOtherMembersAppointed();
            var teamMembers = await _progressReportingRepository.GetLastSubmittedProgressReportTeamMembers();

            if (otherMembersAppointed == true && teamMembers != null && teamMembers.Any())
            {
                return ETaskStatus.InProgress;
            }
        }

        return taskStatus;
    }
}
