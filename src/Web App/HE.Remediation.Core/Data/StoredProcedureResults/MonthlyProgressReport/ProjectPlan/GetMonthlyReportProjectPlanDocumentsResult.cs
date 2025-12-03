namespace HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.ProjectPlan;

public class GetMonthlyReportProjectPlanDocumentsResult
{
    public DateTime? PreviousUploadDate { get; set; }
    public bool? HasEnoughFunds { get; set; }

    public IList<FileResult> ProjectPlanDocuments { get; set; } = new List<FileResult>();
}