
using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProjectTeam.GetProjectTeam;

public class GetProjectTeamResponse
{
    public List<GetTeamMembersResult> TeamMembers { get; set; }

    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }
}
