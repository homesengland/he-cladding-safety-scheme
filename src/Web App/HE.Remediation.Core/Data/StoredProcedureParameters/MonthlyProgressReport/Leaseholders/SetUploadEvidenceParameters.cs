namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.Leaseholders
{
    public class SetUploadEvidenceParameters
    {
        public Guid ApplicationId { get; set; }
        public Guid FileId { get; set; }
        public Guid ProgressReportId { get; set; }
    }
}
