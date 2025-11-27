namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.PlanningPermission;
public class SetProgressReportHaveYouAppliedPlanningPermissionParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public bool HaveAppliedPlanningPermission { get; set; }
}
