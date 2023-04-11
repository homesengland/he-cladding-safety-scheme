using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityRelation.SetResponsibleEntityRelation
{
    public class SetResponsibleEntityRelationHandler : IRequestHandler<SetResponsibleEntityRelationRequest>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetResponsibleEntityRelationHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<Unit> Handle(SetResponsibleEntityRelationRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var responsibleEntityRelationId = (int)request.ResponsibleEntityRelation;

            await SetResponsibleEntityRelationRequest(applicationId, responsibleEntityRelationId);

            return Unit.Value;
        }

        private async Task SetResponsibleEntityRelationRequest(Guid applicationId, int responsibleEntityRelationId)
        {
            await _connection.ExecuteAsync("UpdateResponsibleEntityRelation", new { applicationId, responsibleEntityRelationId });
        }
    }
}
