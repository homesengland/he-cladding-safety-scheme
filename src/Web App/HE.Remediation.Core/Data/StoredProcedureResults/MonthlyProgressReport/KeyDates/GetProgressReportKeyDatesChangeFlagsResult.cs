namespace HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.KeyDates;
public class GetProgressReportKeyDatesChangeFlagsResult
{
    public bool WorksPlanningDatesChanged { get; set; }
    public bool BuildingControlDatesChanged { get; set; }
    public bool PlanningPermissionDatesChanged { get; set; }
    public bool RemediationDatesChanged { get; set; }

    public bool HasAnyDateChanges()
    {
        return WorksPlanningDatesChanged
            || BuildingControlDatesChanged
            || PlanningPermissionDatesChanged
            || RemediationDatesChanged;
    }
}
