
namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNoDesigner.GetReasonNoDesigner;

public class GetReasonNoDesignerResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public string LeadDesignerNotAppointedReason { get; set; }

    public bool? LeadDesignerNeedsSupport { get; set; }
}
