namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
public class GetTeamMembersParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
}
