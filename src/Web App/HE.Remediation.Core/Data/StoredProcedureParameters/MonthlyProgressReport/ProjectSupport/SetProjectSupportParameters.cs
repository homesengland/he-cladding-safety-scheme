namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectSupport
{
    public class SetProjectSupportParameters
    {
        public Guid ApplicationId { get; set; }
        public Guid ProgressReportId { get; set; }
        public bool? RequiresSupport { get; set; }
        public int? TaskStatusId { get; set; }
    }
}
