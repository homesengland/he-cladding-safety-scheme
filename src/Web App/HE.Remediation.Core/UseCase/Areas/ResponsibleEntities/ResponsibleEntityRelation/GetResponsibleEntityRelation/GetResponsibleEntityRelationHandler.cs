using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityRelation.GetResponsibleEntityRelation
{
    public class GetResponsibleEntityRelationHandler : IRequestHandler<GetResponsibleEntityRelationRequest, GetResponsibleEntityRelationResponse>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetResponsibleEntityRelationHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetResponsibleEntityRelationResponse> Handle(GetResponsibleEntityRelationRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await GetResponsibleEntityRelation(applicationId);

            return response;
        }

        private async Task<GetResponsibleEntityRelationResponse> GetResponsibleEntityRelation(Guid applicationId)
        {
            var result = await _connection.QuerySingleOrDefaultAsync<GetResponsibleEntityRelationResponse>("GetResponsibleEntityRelation", new { applicationId });
            return result ?? new GetResponsibleEntityRelationResponse();
        }
    }
}
