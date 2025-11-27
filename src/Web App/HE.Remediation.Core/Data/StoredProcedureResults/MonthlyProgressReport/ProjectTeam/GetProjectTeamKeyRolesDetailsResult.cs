namespace HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.ProjectTeam;

public class GetProjectTeamKeyRolesDetailsResult
{
    public TeamMemberResult ApplicationLead { get; set; }
    public TeamMemberResult LeaseholderCommunicator { get; set; }
    public TeamMemberResult RegulatoryComplianceMember { get; set; }

    public class TeamMemberResult
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
    }
}