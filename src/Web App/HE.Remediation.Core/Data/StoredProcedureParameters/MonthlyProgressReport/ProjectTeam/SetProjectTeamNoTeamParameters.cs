using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
public class SetProjectTeamNoTeamParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public string ReasonNoTeam { get; set; }
    public int SubmitAction { get; set; }
}
