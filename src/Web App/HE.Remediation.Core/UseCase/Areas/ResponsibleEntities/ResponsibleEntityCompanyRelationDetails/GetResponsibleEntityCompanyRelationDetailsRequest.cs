using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanyRelationDetails
{
    public class GetResponsibleEntityCompanyRelationDetailsRequest : IRequest<GetResponsibleEntityCompanyRelationDetailsResponse>
    {
        private GetResponsibleEntityCompanyRelationDetailsRequest() { }

        public static readonly GetResponsibleEntityCompanyRelationDetailsRequest Request = new();

    }
}
