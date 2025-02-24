namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetProgressReportSupportNeedsResult
{
    public bool? LeadDesignerNeedsSupport { get; set; }
    public string LeadDesignerNotAppointedReason { get; set; }
    public bool? OtherMembersNeedsSupport { get; set; }
    public string OtherMembersNotAppointedReason { get; set; }
    public bool? QuotesNeedsSupport { get; set; }
    public string QuotesNotSoughtReason { get; set; }
    public bool? PlanningPermissionNeedsSupport { get; set; }
    public string ReasonPlanningPermissionNotApplied { get; set; }
    public bool? OtherNeedsSupport { get; set; }
    public string SupportNeededReason { get; set; }
}