namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting;

public class ProgressReportsViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public IReadOnlyCollection<ProgressReportSummaryViewModel> ProgressReports { get; set; }
}