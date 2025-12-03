namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectPlan
{
    public class MonthlyProgressReportDeleteUploadProjectPlanParameters
    {
        public Guid ApplicationId { get; set; }
        public Guid FileId { get; set; }
        public Guid ProgressReportId { get; set; }
    }
}
