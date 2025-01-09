namespace HE.Remediation.Core.Data.StoredProcedureResults.ClosingReport
{
    public class GetClosingReportReviewPaymentOverviewResult
    {
        public decimal ScheduledAmount { get; set; }
        public decimal RequestedAmount { get; set; }
        public decimal TotalGrantFunding { get; set; }
        public decimal GrantPaidToDate { get; set; }
        public string ReasonForChange { get; set; }
        public bool CostsChanged { get; set; }
    }
}
