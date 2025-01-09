namespace HE.Remediation.Core.Data.StoredProcedureResults
{
    public class ProgressReportSecondaryCheckMyAnswersResult
    {
        public DateTime? LastUpdate { get; set; }
        public string ThisMonthProgressSummary { get; set; }
        public string NextMonthProgressSummary { get; set; }
        public bool? HelpNeeded { get; set; }
        public bool? LeadDesignerNeedsSupport { get; set; }
        public bool? OtherMembersNeedsSupport { get; set; }
        public bool? PlanningPermissionNeedsSupport { get; set; }
        public bool? QuotesNeedsSupport { get; set; }
        public bool? OtherNeedsSupport { get; set; }
        public string SupportNeededReason { get; set; }
    }
}