namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.Remediation;

public class SetProgressReportRemediationKeyDatesParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public DateTime FullCompletionOfWorksDate { get; set; }
    public DateTime PracticalCompletionDate { get; set; }
}