namespace HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.ProjectSupport;
public class GetProjectSupportCheckYourAnswersResult
{
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public bool? RequiresSupport { get; set; }
    public bool? LeadDesignerNeedsSupport { get; set; }
    public bool? OtherMembersNeedsSupport { get; set; }
    public bool? QuotesNeedsSupport { get; set; }
    public bool? PlanningPermissionNeedsSupport { get; set; }
    public bool? OtherNeedsSupport { get; set; }
    public string SupportNeededReason { get; set; }
}
