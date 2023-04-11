using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class ResponsibleEntityCompanySubTypeViewModel
{
    public EApplicationResponsibleEntityOrganisationSubType? OrganisationSubType { get; set; }
    public string OrganisationSubTypeDescription { get; set; }

    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}
