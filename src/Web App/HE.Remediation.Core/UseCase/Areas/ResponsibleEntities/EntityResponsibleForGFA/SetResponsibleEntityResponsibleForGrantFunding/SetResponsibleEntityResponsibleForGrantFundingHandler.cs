using System.Transactions;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.EntityResponsibleForGFA.SetResponsibleEntityResponsibleForGrantFunding
{
    public class SetResponsibleEntityResponsibleForGrantFundingHandler : IRequestHandler<SetResponsibleEntityResponsibleForGrantFundingRequest, SetResponsibleEntityResponsibleForGrantFundingResponse>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetResponsibleEntityResponsibleForGrantFundingHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<SetResponsibleEntityResponsibleForGrantFundingResponse> Handle(SetResponsibleEntityResponsibleForGrantFundingRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await SetResponsibleEntityResponsibleForGrantFunding(applicationId, request);


            var signatoriesExist = await _connection.QuerySingleOrDefaultAsync<bool>("ApplicationHasSignatories",
                new
                {
                    ApplicationId = applicationId
                });

            return new SetResponsibleEntityResponsibleForGrantFundingResponse
            {
                ResponsibleForGrantFunding = request.ResponsibleForGrantFunding,
                SignatoriesAlreadyExist = signatoriesExist
            };
        }

        private async Task SetResponsibleEntityResponsibleForGrantFunding(Guid applicationId, SetResponsibleEntityResponsibleForGrantFundingRequest request)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _connection.ExecuteAsync("SetResponsibleEntityResponsibleForGrantFunding", new
            {
                ApplicationId = applicationId,
                request.ResponsibleForGrantFunding
            });

            if (request.ResponsibleForGrantFunding == true)
            {
                await _connection.ExecuteAsync("InsertResponsibleEntitiesGrantFundingDefaultSignatory", new
                {
                    ApplicationId = applicationId
                });
            }

            scope.Complete();
        }
    }
}
