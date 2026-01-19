using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanyRelationDetails
{
    public class SetResponsibleEntityCompanyRelationDetailsRequest : IRequest
    {
        public EResponsibleEntityRelation ResponsibleEntityRelation { get; set; }
    }
}
