
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.GrantFundingSignatoryDetails.SetGrantFundingSignatoryDetails
{
    public class SetGrantFundingSignatoryDetailsHandler : IRequestHandler<SetGrantFundingSignatoryDetailsRequest>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetGrantFundingSignatoryDetailsHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<Unit> Handle(SetGrantFundingSignatoryDetailsRequest request, CancellationToken cancellationToken)
        {
            if (request.Id is null || request.Id == Guid.Empty)
            {
                var applicationId = _applicationDataProvider.GetApplicationId();
                await InsertGrantFundingSignatoryDetails(applicationId, request);
            }
            else
            {
                await UpdateGrantFundingSignatoryDetails(request);
            }

            return Unit.Value;
        }

        private async Task InsertGrantFundingSignatoryDetails(Guid applicationId, SetGrantFundingSignatoryDetailsRequest request)
        {
            await _connection.ExecuteAsync("InsertResponsibleEntitiesGrantFundingSignatoryDetails", new { applicationId, request.FirstName, request.LastName, request.EmailAddress, request.Role });
        }

        private async Task UpdateGrantFundingSignatoryDetails(SetGrantFundingSignatoryDetailsRequest request)
        {
            await _connection.ExecuteAsync("UpdateResponsibleEntitiesGrantFundingSignatoryDetails", new { GrantFundingSignatoryId = request.Id, request.FirstName, request.LastName, request.EmailAddress, request.Role });
        }
    }
}
