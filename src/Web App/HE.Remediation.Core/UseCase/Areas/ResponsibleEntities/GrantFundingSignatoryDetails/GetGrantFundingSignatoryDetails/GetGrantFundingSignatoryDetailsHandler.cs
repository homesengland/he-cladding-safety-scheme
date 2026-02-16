using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.GrantFundingSignatoryDetails.GetGrantFundingSignatoryDetails
{
    public class GetGrantFundingSignatoryDetailsHandler : IRequestHandler<GetGrantFundingSignatoryDetailsRequest, GetGrantFundingSignatoryDetailsResponse>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetGrantFundingSignatoryDetailsHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<GetGrantFundingSignatoryDetailsResponse> Handle(GetGrantFundingSignatoryDetailsRequest request, CancellationToken cancellationToken)
        {
            var response = await _connection.QuerySingleOrDefaultAsync<GetGrantFundingSignatoryDetailsResponse>("GetResponsibleEntitiesGrantFundingSignatoryDetails",
                new
                {
                    request.GrantFundingSignatoryId
                });

            return response ?? new GetGrantFundingSignatoryDetailsResponse();
        }
    }
}
