using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress;
using HE.Remediation.Core.UseCase.Areas.Location.PostCode;
using Mediator;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class SetFreeholderAddressHandler : IRequestHandler<SetFreeholderAddressRequest>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IResponsibleEntityRepository _responsibleEntityRepository;

        public SetFreeholderAddressHandler
        (
                IDbConnectionWrapper connection,
                IApplicationDataProvider applicationDataProvider,
                IResponsibleEntityRepository responsibleEntityRepository
        )
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
            _responsibleEntityRepository = responsibleEntityRepository;
        }

        public async ValueTask<Unit> Handle(SetFreeholderAddressRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();
            ParsedAddress parsedAddress = PostCodeUtility.ParseAddressJson(request.SelectedAddressId);
            if (parsedAddress != null)
            {
                var address = await _responsibleEntityRepository.GetFreeholderAddress(applicationId);        
                if (address is not null)
                {
                    await UpdateFreeholderAddress(applicationId, parsedAddress);
                    return Unit.Value;
                }

                await InsertFreeholderAddress(applicationId, parsedAddress);
            }

            return Unit.Value;
        }

        private async Task UpdateFreeholderAddress(Guid applicationId, ParsedAddress parsedAddress)
        {
            await _connection.ExecuteAsync("UpdateFreeholderAddress",
                new
                {
                    ApplicationId = applicationId,
                    parsedAddress.NameNumber,
                    parsedAddress.AddressLine1,
                    parsedAddress.AddressLine2,
                    parsedAddress.City,
                    parsedAddress.LocalAuthority,
                    parsedAddress.County,
                    parsedAddress.Postcode,
                    parsedAddress.SubBuildingName,
                    parsedAddress.BuildingName,
                    parsedAddress.BuildingNumber,
                    parsedAddress.Street,
                    parsedAddress.Town,
                    parsedAddress.AdminArea,
                    parsedAddress.UPRN,
                    parsedAddress.AddressLines,
                    parsedAddress.XCoordinate,
                    parsedAddress.YCoordinate,
                    parsedAddress.Toid,
                    parsedAddress.BuildingType
                });
        }

        private async Task InsertFreeholderAddress(Guid applicationId, ParsedAddress parsedAddress)
        {
            var addressId = Guid.NewGuid();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _connection.ExecuteAsync("InsertFreeholderAddress",
                    new
                    {
                        AddressId = addressId,
                        parsedAddress.NameNumber,
                        parsedAddress.AddressLine1,
                        parsedAddress.AddressLine2,
                        parsedAddress.City,
                        parsedAddress.LocalAuthority,
                        parsedAddress.County,
                        parsedAddress.Postcode,
                        parsedAddress.SubBuildingName,
                        parsedAddress.BuildingName,
                        parsedAddress.BuildingNumber,
                        parsedAddress.Street,
                        parsedAddress.Town,
                        parsedAddress.AdminArea,
                        parsedAddress.UPRN,
                        parsedAddress.AddressLines,
                        parsedAddress.XCoordinate,
                        parsedAddress.YCoordinate,
                        parsedAddress.Toid,
                        parsedAddress.BuildingType
                    });

                await _connection.ExecuteAsync("UpdateFreeholderAddressId", new { applicationId, addressId });

                scope.Complete();
            }
        }
    }

    public class SetFreeholderAddressRequest : IRequest
    {
        public string SelectedAddressId { get; set; }
    }
}
