namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.PlanningPermission;
public class SetProgressReportPlanningPermissionKeyDatesParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public int WorksNeedPlanningPermission { get; set; }
}
