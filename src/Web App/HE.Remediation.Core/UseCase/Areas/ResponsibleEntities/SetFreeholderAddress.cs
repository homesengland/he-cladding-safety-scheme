using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class SetFreeholderAddressHandler : IRequestHandler<SetFreeholderAddressRequest>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetFreeholderAddressHandler
        (
                IDbConnectionWrapper connection,
                IApplicationDataProvider applicationDataProvider
        )
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<Unit> Handle(SetFreeholderAddressRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await _connection.QuerySingleOrDefaultAsync<GetBuildingAddressResponse>("GetFreeholderAddress", new { applicationId });

            if (response is not null)
            {
                await UpdateFreeholderAddress(applicationId, request);
                return Unit.Value;
            }

            await InsertFreeholderAddress(applicationId, request);
            return Unit.Value;
        }

        private async Task UpdateFreeholderAddress(Guid applicationId, SetFreeholderAddressRequest request)
        {
            await _connection.ExecuteAsync("UpdateFreeholderAddress",
                new
                {
                    applicationId,
                    request.NameNumber,
                    request.AddressLine1,
                    request.AddressLine2,
                    request.City,
                    request.County,
                    request.Postcode
                });
        }

        private async Task InsertFreeholderAddress(Guid applicationId, SetFreeholderAddressRequest request)
        {
            var addressId = Guid.NewGuid();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _connection.ExecuteAsync("InsertFreeholderAddress",
                    new
                    {
                        addressId,
                        request.NameNumber,
                        request.AddressLine1,
                        request.AddressLine2,
                        request.City,
                        request.County,
                        request.Postcode
                    });

                await _connection.ExecuteAsync("UpdateFreeholderAddressId", new { applicationId, addressId });

                scope.Complete();
            }
        }
    }

    public class SetFreeholderAddressRequest : IRequest
    {
        public string NameNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
    }
}
