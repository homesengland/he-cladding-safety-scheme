
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AddRole.GetAddRole;

public class GetAddRoleResponse
{
    public List<ETeamRole> AvailableTeamRoles { get; set; }

    public ETeamRole? TeamRole { get; set; }

    public bool ShowLeadDesigner { get; set; }

    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }
    public int Version { get; set; }
}
