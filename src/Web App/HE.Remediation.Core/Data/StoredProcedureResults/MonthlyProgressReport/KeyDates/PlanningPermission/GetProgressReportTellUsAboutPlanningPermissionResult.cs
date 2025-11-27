namespace HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.KeyDates.PlanningPermission;
public class GetProgressReportTellUsAboutPlanningPermissionResult
{
    public DateTime? PlanningPermissionDateSubmitted { get; set; }
    public DateTime? PlanningPermissionDateApproved { get; set; }
    public DateTime? PreviousPlanningPermissionDateSubmitted { get; set; }
    public DateTime? PreviousPlanningPermissionDateApproved { get; set; }
}
