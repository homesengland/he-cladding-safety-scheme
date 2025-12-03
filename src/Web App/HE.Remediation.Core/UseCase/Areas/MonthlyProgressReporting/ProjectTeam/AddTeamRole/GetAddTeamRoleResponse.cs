using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.AddTeamRole;

public class GetAddTeamRoleResponse
{
    public List<ETeamRole> AvailableTeamRoles { get; set; }

    public ETeamRole? TeamRole { get; set; }

    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }
}
