namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
public class GetTeamMemberParameters
{
    public Guid? TeamMemberId { get; set; }
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
}
