using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.TaskList.GetTaskList;

public class GetTaskListResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public ETaskStatus GrantCertifyingOfficerStatusId { get; set; }

    public ETaskStatus CostsScheduleStatusId { get; set; }

    public ETaskStatus ThirdPartyContributionsStatusId { get; set; }

    public ETaskStatus DeclarationStatusId { get; set; }

    public ETaskStatus DutyOfCareDeedStatusId { get; set; }

    public bool DutyOfCareDeedSent { get; set; }

    public ETaskStatus ProjectTeamStatusId { get; set; }

    public ETaskStatus PlanningPermissionStatusId { get; set; }

    public ETaskStatus KeyDatesStatusId { get; set; }
    
    public ETaskStatus SignatoriesStatusId { get; set; }

    public ETaskStatus ProgrammePlanStatusId { get; set; }

    public bool IsSubmitted { get; set; }
}
