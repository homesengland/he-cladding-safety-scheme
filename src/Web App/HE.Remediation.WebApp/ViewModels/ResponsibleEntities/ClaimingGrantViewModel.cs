using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class ClaimingGrantViewModel
{
    public bool? IsClaimingGrant { get; set; }
    public bool? HasOwners { get; set; }

    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}