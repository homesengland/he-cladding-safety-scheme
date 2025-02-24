
namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNeedsSupport.GetReasonNeedsSupport;

public class GetReasonNeedsSupportResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool? LeadDesignerNeedsSupport { get; set; }

    public bool? OtherMembersNeedsSupport { get; set; }

    public bool? PlanningPermissionNeedsSupport { get; set; }

    public bool? QuotesNeedsSupport { get; set; }

    public string SupportNeededReason { get; set; }
}
