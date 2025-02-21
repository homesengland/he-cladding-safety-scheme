
namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ProgressReportsViewModel
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public IReadOnlyCollection<ProgressReportSummaryViewModel> ProgressReports { get; set; }
}

public class ProgressReportSummaryViewModel
{
    public Guid? Id { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateDue { get; set; }

    public DateTime? DateSubmitted { get; set; }

    public int Version { get; set; }
}
