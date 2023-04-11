using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class LeaseholderOrPrivateOwnerViewModel
{
    public bool? HasOwners { get; set; }
    public int? SharedOwnerCount { get; set; }

    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}