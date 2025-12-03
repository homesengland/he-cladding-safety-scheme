namespace HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.KeyDates.Remediation;

public class GetProgressReportRemediationKeyDatesResult
{
    public DateTime? FullCompletionOfWorksDate { get; set; }
    public DateTime? PreviousFullCompletionOfWorksDate { get; set; }

    public DateTime? PracticalCompletionDate { get; set; }
    public DateTime? PreviousPracticalCompletionDate { get; set; }
}