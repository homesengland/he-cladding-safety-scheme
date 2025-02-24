using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.ProjectTeam;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeam.StartInformation.Set;

public class SetStartInformationResponse
{
    public List<ProjectTeamMembersResult> TeamMembers { get; set; }

    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }
    
    public bool IsSubmitted { get; set; }
}
