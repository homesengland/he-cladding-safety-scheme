using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.ProjectTeam;

public class ProjectTeamMembersResult
{
    public Guid Id { get; set; }
    
    public string EmailAddress { get; set; }
    
    public string Name { get; set; }
    
    public string CompanyName { get; set; }
    
    public string OtherRole { get; set; }
    
    public ETeamRole Role { get; set; }
    public bool? HasChasCertification { get; set; }
    public bool? InvolvedInOriginalInstallation { get; set; }
}