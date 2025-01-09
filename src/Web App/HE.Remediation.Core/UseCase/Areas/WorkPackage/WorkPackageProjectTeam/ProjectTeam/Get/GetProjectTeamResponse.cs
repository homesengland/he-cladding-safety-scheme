using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.ProjectTeam;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeam.ProjectTeam.Get;

public class GetProjectTeamResponse
{
    public List<ProjectTeamMembersResult> TeamMembers { get; set; }
    
    public List<ETeamRole> MissingRoles { get; set; }

    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }
    
    public bool IsSubmitted { get; set; }
    
    public bool IsCopy { get; set; }

    public bool? HasChasCertificationValue { get; set; }
}
