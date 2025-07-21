using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ReasonNeedsSupportViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public bool? LeadDesignerNeedsSupport { get; set; }

    public bool? OtherMembersNeedsSupport { get; set; }

    public bool? PlanningPermissionNeedsSupport { get; set; }
    
    public bool? QuotesNeedsSupport { get; set; }

    public string SupportNeededReason { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }

    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}
