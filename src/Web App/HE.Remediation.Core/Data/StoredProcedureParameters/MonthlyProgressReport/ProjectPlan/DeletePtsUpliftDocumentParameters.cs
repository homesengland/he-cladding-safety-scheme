namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectPlan;

public class DeletePtsUpliftDocumentParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public Guid FileId { get; set; }
}