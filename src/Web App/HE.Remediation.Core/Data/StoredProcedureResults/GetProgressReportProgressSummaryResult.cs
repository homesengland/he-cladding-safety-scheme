namespace HE.Remediation.Core.Data.StoredProcedureResults
{
    public class GetProgressReportProgressSummaryResult
    {
        public string ProgressSummary { get; set; }
        public string GoalSummary { get; set; }
        public bool? RequiresSupport { get; set; }
    }
}
