namespace HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.ProjectSupport;
public class GetProgressReportSupportTypeResult
{
    public bool? LeadDesignerNeedsSupport { get; set; }
    public bool? OtherMembersNeedsSupport { get; set; }
    public bool? PlanningPermissionNeedsSupport { get; set; }
    public bool? QuotesNeedsSupport { get; set; }
    public bool? OtherNeedsSupport { get; set; }
    public string SupportNeededReason { get; set; }
}
