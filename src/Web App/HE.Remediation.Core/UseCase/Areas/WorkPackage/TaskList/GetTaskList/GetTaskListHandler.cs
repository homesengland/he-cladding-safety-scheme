using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.TaskList.GetTaskList;

public class GetTaskListHandler : IRequestHandler<GetTaskListRequest, GetTaskListResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;
    private readonly IWorkPackageRepository _workPackageRepository;
    private readonly IWorkPackageFireRiskAssessmentRepository _fireRiskAssessmentRepository;
    private readonly IFireRiskAssessmentRepository _fraRepository;

    public GetTaskListHandler(IApplicationDataProvider applicationDataProvider,
                              IBuildingDetailsRepository buildingDetailsRepository,
                              IApplicationRepository applicationRepository,
                              IProgressReportingRepository progressReportingRepository,
                              IWorkPackageRepository workPackageRepository,
                              IWorkPackageFireRiskAssessmentRepository fireRiskAssessmentRepository,
                              IFireRiskAssessmentRepository fraRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
        _workPackageRepository = workPackageRepository;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
        _fraRepository = fraRepository;
    }

    public async ValueTask<GetTaskListResponse> Handle(GetTaskListRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var applicationScheme = _applicationDataProvider.GetApplicationScheme();

        var hasFra = await _fraRepository.GetHasFra(applicationId);

        await _fireRiskAssessmentRepository.InsertWorkPackageFra(applicationId);

        var workPackageTaskListSummary = await _workPackageRepository.GetWorkPackageTaskListSummary();

        var isDutyOfCareCompletedInProgressReport = await IsDutyOfCareCompleteInProgressReport();
        var isGCOCompleteInProgressReport = await IsGCOCompleteInProgressReport();

        var planningPermissionStatus = await SetStatusToInProgressIfHasSubmittedProgressReporRequirePlanningPermissio(workPackageTaskListSummary.WorkPackagePlanningPermissionStatusId);
        var projectTeamStatus = await SetStatusToInProgressIfHasSubmittedProgressReportTeamMembers(workPackageTaskListSummary.WorkPackageProjectTeamStatusId, isGCOCompleteInProgressReport);

        return new GetTaskListResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            ApplicationScheme = applicationScheme,
            GrantCertifyingOfficerStatusId = isGCOCompleteInProgressReport ? ETaskStatus.Completed
                                             : workPackageTaskListSummary.WorkPackageGrantCertifyingOfficerStatusId,
            InternalDefectsStatusId = workPackageTaskListSummary.WorkPackageInternalDefectsStatusId,
            CostsScheduleStatusId = workPackageTaskListSummary.WorkPackageCostsScheduleStatusId,
            CladdingSystemStatusId = workPackageTaskListSummary.WorkPackageCladdingSystemStatusId,
            ThirdPartyContributionsStatusId = workPackageTaskListSummary.WorkPackageThirdPartyContributionsStatusId,
            DeclarationStatusId = workPackageTaskListSummary.WorkPackageDeclarationStatusId,
            DutyOfCareDeedStatusId = isDutyOfCareCompletedInProgressReport ? ETaskStatus.Completed
                                     : workPackageTaskListSummary.WorkPackageDutyOfCareDeedStatusId,
            DutyOfCareDeedSent = workPackageTaskListSummary.WorkPackageDutyOfCareDeedSent,
            ProjectTeamStatusId = projectTeamStatus,
            PlanningPermissionStatusId = planningPermissionStatus,
            KeyDatesStatusId = workPackageTaskListSummary.WorkPackageKeyDatesStatusId,
            ProgrammePlanStatusId = workPackageTaskListSummary.WorkPackageProgrammePlanStatusId,
            WorkPackageFireRiskAssessmentStatusId = workPackageTaskListSummary.WorkPackageFireRiskAssessmentStatusId,
            IsSubmitted = workPackageTaskListSummary.IsSubmitted,
            HasFra = hasFra == true
        };
    }

    private async ValueTask<ETaskStatus> SetStatusToInProgressIfHasSubmittedProgressReporRequirePlanningPermissio(ETaskStatus taskStatus)
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

    private async Task<bool> IsGCOCompleteInProgressReport()
    {
        var isGrantCertifyingOfficerComplete = await _progressReportingRepository.IsGrantCertifyingOfficerComplete();
        if (isGrantCertifyingOfficerComplete)
        {
            await _workPackageRepository.UpdateGrantCertifyingOfficerStatus(ETaskStatus.Completed);
        }
        return isGrantCertifyingOfficerComplete;
    }

    private async Task<bool> IsDutyOfCareCompleteInProgressReport()
    {
        var isDutyOfCareComplete = await _progressReportingRepository.IsDutyOfCareComplete();
        if (isDutyOfCareComplete)
        {
            await _workPackageRepository.UpdateDutyOfCareDeedStatus(ETaskStatus.Completed);
        }
        return isDutyOfCareComplete;
    }

    private async ValueTask<ETaskStatus> SetStatusToInProgressIfHasSubmittedProgressReportTeamMembers(ETaskStatus taskStatus, bool isGCOCompleteInProgressReport)
    {
        if (taskStatus != ETaskStatus.Completed)
        {
            var teamMembers = await _progressReportingRepository.GetLastSubmittedProgressReportTeamMembers();

            if (isGCOCompleteInProgressReport || (teamMembers != null && teamMembers.Any()))
            {
                return ETaskStatus.InProgress;
            }
        }

        return taskStatus;
    }
}
