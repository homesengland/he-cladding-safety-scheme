using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.SetBuildingAddress
{
    public class SetBuildingAddressHandler : IRequestHandler<SetBuildingAddressRequest, Unit>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;
        public SetBuildingAddressHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<Unit> Handle(SetBuildingAddressRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetBuildingAddressResponse>("GetBuildingAddress", new { applicationId });

            if (response is not null)
            {
                await UpdateBuildingAddress(request, applicationId);
                return Unit.Value;
            }

            await InsertBuildingAddress(request, applicationId);
            return Unit.Value;
        }

        private async Task InsertBuildingAddress(SetBuildingAddressRequest request, Guid applicationId)
        {
            var addressId = Guid.NewGuid();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _dbConnectionWrapper.ExecuteAsync("InsertBuildingAddress",
                    new
                    {
                        addressId,
                        request.NameNumber,
                        request.AddressLine1,
                        request.AddressLine2,
                        request.City,
                        request.LocalAuthority,
                        request.Postcode,
                        request.County
                    }
                );

                await _dbConnectionWrapper.ExecuteAsync("UpdateBuildingDetailsAddressId", new { applicationId, addressId });

                scope.Complete();
            }
        }

        private async Task UpdateBuildingAddress(SetBuildingAddressRequest request, Guid applicationId)
        {
            await _dbConnectionWrapper.ExecuteAsync("UpdateBuildingAddress",
                new
                {
                    applicationId,
                    request.NameNumber,
                    request.AddressLine1,
                    request.AddressLine2,
                    request.City,
                    request.LocalAuthority,
                    request.Postcode,
                    request.County
                }
            );
        }


    }
}