namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
public class DeleteProjectTeamTeamMemberParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public Guid TeamMemberId { get; set; }
}
