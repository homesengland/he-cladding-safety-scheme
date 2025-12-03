namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectPlan;

public class InsertPtsUpliftDocumentParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public Guid FileId { get; set; }
}