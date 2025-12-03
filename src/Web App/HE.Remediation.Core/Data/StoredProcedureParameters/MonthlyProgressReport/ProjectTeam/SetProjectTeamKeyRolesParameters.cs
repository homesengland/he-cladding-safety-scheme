namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;

public class SetProjectTeamKeyRolesParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public Guid? ApplicationleadTeamMemberId { get; set; }
    public Guid? LeaseholderCommunictorTeamMemberId { get; set; }
    public Guid? RegulatoryComplianceTeamMemberId { get; set; }
}