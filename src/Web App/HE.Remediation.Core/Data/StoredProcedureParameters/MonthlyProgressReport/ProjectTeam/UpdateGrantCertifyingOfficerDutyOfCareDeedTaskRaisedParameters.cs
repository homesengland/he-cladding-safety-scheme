namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;

public class UpdateGrantCertifyingOfficerDutyOfCareDeedTaskRaisedParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public bool DutyOfCareDeedTaskRaised { get; set; }
}