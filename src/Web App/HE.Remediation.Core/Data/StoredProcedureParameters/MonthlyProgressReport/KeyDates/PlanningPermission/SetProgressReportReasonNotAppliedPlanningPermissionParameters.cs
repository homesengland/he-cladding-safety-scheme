namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.PlanningPermission;
public class SetProgressReportReasonNotAppliedPlanningPermissionParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public string ReasonNotAppliedPlanningPermission { get; set; }
    public DateTime? PlannedDateToSubmitApplication { get; set; }
}
