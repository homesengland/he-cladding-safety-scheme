namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectSupport;
public class SetProjectSupportTypeParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public bool LeadDesignerNeedsSupport { get; set; }
    public bool OtherMembersNeedsSupport { get; set; }
    public bool QuotesNeedsSupport { get; set; }
    public bool PlanningPermissionNeedsSupport { get; set; }
    public bool OtherNeedsSupport { get; set; }
    public string SupportNeedsReason { get; set; }
}
