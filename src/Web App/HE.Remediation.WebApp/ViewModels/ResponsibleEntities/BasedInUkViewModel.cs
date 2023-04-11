using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class BasedInUkViewModel
{
    public bool? BasedInUk { get; set; }
    public ESubmitAction SubmitAction { get; set; }

    public string ReturnUrl { get; set; }
}