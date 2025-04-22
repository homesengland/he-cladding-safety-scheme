using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class ResponsibleEntityCompanyTypeViewModel
{
    public EApplicationResponsibleEntityOrganisationType? OrganisationType { get; set; }
    
    public EResponsibleEntityRelation? ResponsibleEntityRelationType { get; set; }

    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
    public EApplicationScheme ApplicationScheme { get; set; }
}