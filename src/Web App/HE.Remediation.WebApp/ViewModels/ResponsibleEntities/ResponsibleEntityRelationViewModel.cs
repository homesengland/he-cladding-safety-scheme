using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class ResponsibleEntityRelationViewModel
    {
        public EResponsibleEntityRelation? ResponsibleEntityRelation { get; set; }
        public EApplicationRepresentationType? RepresentationType { get; set; }

        public string ReturnUrl { get; set; }
    }
}
