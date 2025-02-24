using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class AddRoleViewModel
{
    public List<ETeamRole> AvailableTeamRoles { get; set; }

    public ETeamRole? TeamRole { get; set; }

    public bool ShowLeadDesigner { get; set; }

    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }
    public int Version { get; set; }

    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}
