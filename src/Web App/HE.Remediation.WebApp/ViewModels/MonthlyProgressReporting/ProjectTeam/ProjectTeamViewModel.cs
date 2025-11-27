namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam;

public class ProjectTeamViewModel
{
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public Guid? ApplicationLeadTeamMemberId { get; set; }
    public string ApplicationLeadTeamMember { get; set; }
    public Guid? LeaseholderCommunicatorTeamMemberId { get; set; }
    public string LeaseholderCommunicatorTeamMember { get; set; }
    public Guid? RegulatoryComplianceTeamMemberId { get; set; }
    public string RegulatoryComplianceTeamMember { get; set; }
    public Guid? GrantCertifyingOfficerTeamMemberId { get; set; }
    public string GrantCertifyingOfficerTeamMember { get; set; }
    public bool? HasAssignedGco { get; set; }

    public IList<ProjectTeamMemberViewModel> TeamMembers { get; set; } = new List<ProjectTeamMemberViewModel>();
    public bool HasAvailableRoles { get; set; }

    public class ProjectTeamMemberViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Role { get; set; }
        public int RoleId { get; set; }
    }
}
