namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.WorksPlanning;
public class SetReasonForNonCompetitiveTenderParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public string ReasonForNonCompetitiveTender { get; set; }
}
