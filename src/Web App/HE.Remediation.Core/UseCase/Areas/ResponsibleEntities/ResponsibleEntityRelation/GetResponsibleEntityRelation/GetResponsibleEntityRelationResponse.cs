using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityRelation.GetResponsibleEntityRelation
{
    public class GetResponsibleEntityRelationResponse
    {
        public EResponsibleEntityRelation? ResponsibleEntityRelation { get; set; }
        public EApplicationRepresentationType? RepresentationType { get; set; }
    }
}
