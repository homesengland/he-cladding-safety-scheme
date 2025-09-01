using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage;

public class TaskListViewModel : WorkPackageBaseViewModel
{
    public ETaskStatus GrantCertifyingOfficerStatusId { get; set; }

    public ETaskStatus CostsScheduleStatusId { get; set; }

    public ETaskStatus InternalDefectsStatusId { get; set; }

    public ETaskStatus ThirdPartyContributionsStatusId { get; set; }

    public ETaskStatus DeclarationStatusId { get; set; }

    public ETaskStatus DutyOfCareDeedStatusId { get; set; }

    public bool DutyOfCareDeedSent { get; set; }

    public ETaskStatus ProjectTeamStatusId { get; set; }

    public ETaskStatus PlanningPermissionStatusId { get; set; }

    public ETaskStatus KeyDatesStatusId { get; set; }

    public ETaskStatus ProgrammePlanStatusId { get; set; }

    public ETaskStatus WorkPackageFireRiskAssessmentStatusId { get; set; }
    public bool HasFra { get; set; }

    public bool CannotSubmit =>
        !IsSubmitted &&
            (GrantCertifyingOfficerStatusId != ETaskStatus.Completed ||
             CostsScheduleStatusId != ETaskStatus.Completed ||
             InternalDefectsStatusId != ETaskStatus.Completed ||
             ThirdPartyContributionsStatusId != ETaskStatus.Completed ||
             DeclarationStatusId != ETaskStatus.Completed ||
             ProjectTeamStatusId != ETaskStatus.Completed ||
             PlanningPermissionStatusId != ETaskStatus.Completed ||
             KeyDatesStatusId != ETaskStatus.Completed ||
             ProgrammePlanStatusId != ETaskStatus.Completed ||
             WorkPackageFireRiskAssessmentStatusId != ETaskStatus.Completed ||
             !DutyOfCareDeedSent);
}
