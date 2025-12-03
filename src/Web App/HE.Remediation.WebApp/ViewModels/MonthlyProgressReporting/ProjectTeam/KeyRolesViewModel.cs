namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam;

public class KeyRolesViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public Guid? ApplicationLeadTeamMemberId { get; set; }
    public Guid? LeaseholderCommunicatorTeamMemberId { get; set; }
    public Guid? RegulatoryComplianceTeamMemberId { get; set; }

    public IList<KeyRolesTeamMemberModel> AllTeamMembers { get; set; } = new List<KeyRolesTeamMemberModel>();

    public class KeyRolesTeamMemberModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string RoleName { get; set; }
    }
}