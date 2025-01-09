namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class UpdateProgressReportSupportParameters
{
    public bool LeadDesignerNeedsSupport { get; set; }
    public bool OtherMembersNeedsSupport { get; set; }
    public bool QuotesNeedsSupport { get; set; }
    public bool PlanningPermissionNeedsSupport { get; set; }
    public bool OtherNeedsSupport { get; set; }
    public string SupportNeedsReason { get; set; }
}