using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.FreeholderCompanyOrIndividual.SetFreeholderCompanyOrIndividual
{
    public class SetFreeholderCompanyOrIndividualHandler : IRequestHandler<SetFreeholderCompanyOrIndividualRequest>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetFreeholderCompanyOrIndividualHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<Unit> Handle(SetFreeholderCompanyOrIndividualRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var result = await _connection.QuerySingleOrDefaultAsync<int?>("GetFreeholderCompanyOrIndividual", new { applicationId });

            if (result is null)
            {
                var freeholderId = Guid.NewGuid();
                await InsertFreeholderCompanyOrIndividual(applicationId, freeholderId, (int)request.ReponsibleEntityType);
                return Unit.Value;
            }

            await UpdateFreeholderCompanyOrIndividual(applicationId, (int)request.ReponsibleEntityType);
            return Unit.Value;
        }
        private async Task InsertFreeholderCompanyOrIndividual(Guid applicationId, Guid freeholderId, int responsibleEntityTypeId)
        {
            await _connection.ExecuteAsync("InsertFreeholderCompanyOrIndividual", new { applicationId, freeholderId, responsibleEntityTypeId });
        }

        private async Task UpdateFreeholderCompanyOrIndividual(Guid applicationId, int responsibleEntityTypeId)
        {
            await _connection.ExecuteAsync("UpdateFreeholderCompanyOrIndividual", new { applicationId, responsibleEntityTypeId });
        }
    }
}
