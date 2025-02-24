namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.Remove.Get;

public class GetRemoveTeamMemberResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public Guid TeamMemberId { get; set; }

    public string TeamMemberName { get; set; }
}
