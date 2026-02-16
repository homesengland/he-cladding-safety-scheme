using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanyRelationDetails
{
    public class GetResponsibleEntityCompanyRelationDetailsHandler : IRequestHandler<GetResponsibleEntityCompanyRelationDetailsRequest, GetResponsibleEntityCompanyRelationDetailsResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IResponsibleEntityRepository _responsibleEntityRepository;

        public GetResponsibleEntityCompanyRelationDetailsHandler(IApplicationDataProvider applicationDataProvider, IResponsibleEntityRepository responsibleEntityRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _responsibleEntityRepository = responsibleEntityRepository;
        }

        public async ValueTask<GetResponsibleEntityCompanyRelationDetailsResponse> Handle(GetResponsibleEntityCompanyRelationDetailsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();
            var response = await _responsibleEntityRepository.GetResponsibleEntityCompanyRelationDetails(applicationId);
            return new GetResponsibleEntityCompanyRelationDetailsResponse
            {
                ResponsibleEntityRelation = response.HasValue ? (EResponsibleEntityRelation?)response.Value : null
            };
        }
    }
}
