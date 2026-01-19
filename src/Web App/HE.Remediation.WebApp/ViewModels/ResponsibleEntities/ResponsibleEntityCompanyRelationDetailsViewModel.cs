using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class ResponsibleEntityCompanyRelationDetailsViewModel
    {
        public EResponsibleEntityRelation? ResponsibleEntityRelation { get; set; }
        public ESubmitAction SubmitAction { get; set; }
        public string ReturnUrl { get; set; }
    }
}
