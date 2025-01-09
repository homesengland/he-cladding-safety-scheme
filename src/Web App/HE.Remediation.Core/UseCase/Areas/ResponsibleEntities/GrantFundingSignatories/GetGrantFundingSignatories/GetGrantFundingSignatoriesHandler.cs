using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.GrantFundingSignatories.GetGrantFundingSignatories
{
    public class GetGrantFundingSignatoriesHandler : IRequestHandler<GetGrantFundingSignatoriesRequest, IReadOnlyCollection<GetGrantFundingSignatoryResponse>>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetGrantFundingSignatoriesHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<IReadOnlyCollection<GetGrantFundingSignatoryResponse>> Handle(GetGrantFundingSignatoriesRequest request, CancellationToken cancellationToken)
        {
            var response = await _connection.QueryAsync<GetGrantFundingSignatoryResponse>("GetResponsibleEntitiesGrantFundingSignatories",
                new
                {
                    ApplicationId = _applicationDataProvider.GetApplicationId()
                });

            return response;
        }
    }
}