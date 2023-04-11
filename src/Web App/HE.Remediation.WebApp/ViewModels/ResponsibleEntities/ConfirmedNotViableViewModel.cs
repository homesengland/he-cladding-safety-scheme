using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class ConfirmedNotViableViewModel
{
    public EApplicationResponsibleEntityOrganisationType? OrganisationType { get; set; }
    public bool? IsConfirmedNotViable { get; set; }

    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}