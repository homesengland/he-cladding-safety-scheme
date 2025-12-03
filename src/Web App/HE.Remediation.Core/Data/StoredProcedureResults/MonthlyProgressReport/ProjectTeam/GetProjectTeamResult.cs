using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.ProjectTeam;

public class GetProjectTeamResult
{
    public Guid? ApplicationLeadTeamMemberId { get; set; }
    public string ApplicationLeadTeamMember { get; set; }
    public Guid? LeaseholderCommunicatorTeamMemberId { get; set; }
    public string LeaseholderCommunicatorTeamMember { get; set; }
    public Guid? RegulatoryComplianceTeamMemberId { get; set; }
    public string RegulatoryComplicanceTeamMember { get; set; }
    public Guid? GrantCertifyingOfficerTeamMemberId { get; set; }
    public string GrantCertifyingOfficerTeamMember { get; set; }

    public IList<ProjectTeamMemberResult> TeamMembers { get; set; } = new List<ProjectTeamMemberResult>();

    public class ProjectTeamMemberResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Role { get; set; }
        public int RoleId { get; set; }
    }
}