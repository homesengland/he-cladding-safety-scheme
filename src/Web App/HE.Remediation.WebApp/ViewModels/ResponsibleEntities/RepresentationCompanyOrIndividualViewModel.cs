using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class RepresentationCompanyOrIndividualViewModel
{
    public ESubmitAction SubmitAction { get; set; }
    public EResponsibleEntityType? ReponsibleEntityType { get; set; }

    public string ReturnUrl { get; set; }
}