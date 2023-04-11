using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityRelation.SetResponsibleEntityRelation
{
    public class SetResponsibleEntityRelationRequest : IRequest
    {
        public EResponsibleEntityRelation ResponsibleEntityRelation { get; set; }
    }
}
