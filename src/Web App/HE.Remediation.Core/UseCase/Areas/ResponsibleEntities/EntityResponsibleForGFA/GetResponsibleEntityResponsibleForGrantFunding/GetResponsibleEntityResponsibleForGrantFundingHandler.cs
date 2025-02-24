using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.EntityResponsibleForGFA.GetResponsibleEntityResponsibleForGrantFunding
{
    public class GetResponsibleEntityResponsibleForGrantFundingHandler : IRequestHandler<GetResponsibleEntityResponsibleForGrantFundingRequest, GetResponsibleEntityResponsibleForGrantFundingResponse>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetResponsibleEntityResponsibleForGrantFundingHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetResponsibleEntityResponsibleForGrantFundingResponse> Handle(GetResponsibleEntityResponsibleForGrantFundingRequest request, CancellationToken cancellationToken)
        {
            var response = await _connection.QuerySingleOrDefaultAsync<GetResponsibleEntityResponsibleForGrantFundingResponse>("GetResponsibleEntityResponsibleForGrantFunding",
                new
                {
                    ApplicationId = _applicationDataProvider.GetApplicationId()
                });

            return response ?? new GetResponsibleEntityResponsibleForGrantFundingResponse();
        }
    }
}
