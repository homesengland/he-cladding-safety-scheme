
using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProgressReports.GetProgressReports;

public class GetProgressReportsResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public IReadOnlyCollection<ProgressReportResult> ProgressReports { get; set; }
}
