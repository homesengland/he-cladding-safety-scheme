using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityRelation.GetResponsibleEntityRelation
{
    public class GetResponsibleEntityRelationRequest : IRequest<GetResponsibleEntityRelationResponse>
    {
        private GetResponsibleEntityRelationRequest() { }

        public static readonly GetResponsibleEntityRelationRequest Request = new();
    }
}
