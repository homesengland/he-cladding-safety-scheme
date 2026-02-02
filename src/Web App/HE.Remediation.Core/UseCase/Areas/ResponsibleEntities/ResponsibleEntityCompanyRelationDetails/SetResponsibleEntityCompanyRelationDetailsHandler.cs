using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.ResponsibleEntities;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanyRelationDetails
{
    public class SetResponsibleEntityCompanyRelationDetailsHandler : IRequestHandler<SetResponsibleEntityCompanyRelationDetailsRequest>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IResponsibleEntityRepository _responsibleEntityRepository;

        public SetResponsibleEntityCompanyRelationDetailsHandler(IApplicationDataProvider applicationDataProvider,
            IResponsibleEntityRepository responsibleEntityRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _responsibleEntityRepository = responsibleEntityRepository;
        }

        public async ValueTask<Unit> Handle(SetResponsibleEntityCompanyRelationDetailsRequest request, CancellationToken cancellationToken)
        {
            var applicantId = _applicationDataProvider.GetApplicationId();
            var parameters = new SetResponsibleEntityCompanyRelationDetailsParameters
            {
                ApplicationId = applicantId,
                ResponsibleEntityRelationId = (int)request.ResponsibleEntityRelation
            };
            await _responsibleEntityRepository.SetResponsibleEntityCompanyRelationDetails(parameters);
            return Unit.Value;
        }
    }
}
