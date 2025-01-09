using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class AppointedLeadDesignerViewModel
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool? LeadDesignerAppointed { get; set; }

    public bool? LeaseholdersInformed { get; set; }

    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}
