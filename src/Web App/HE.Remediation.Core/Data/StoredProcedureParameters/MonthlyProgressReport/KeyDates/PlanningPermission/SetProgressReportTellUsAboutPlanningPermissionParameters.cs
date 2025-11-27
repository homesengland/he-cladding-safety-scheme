namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.PlanningPermission;
public class SetProgressReportTellUsAboutPlanningPermissionParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public DateTime? PlanningPermissionDateSubmitted { get; set; }
    public DateTime? PlanningPermissionDateApproved { get; set; }
}
