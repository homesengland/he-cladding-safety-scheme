using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Location.PostCode;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class SetResponsibleEntityCompanyAddressHandler : IRequestHandler<SetResponsibleEntityCompanyAddressRequest>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IResponsibleEntityRepository _responsibleEntityRepository;

        public SetResponsibleEntityCompanyAddressHandler
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

        public async Task<Unit> Handle(SetResponsibleEntityCompanyAddressRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            ParsedAddress parsedAddress = PostCodeUtility.ParseAddressJson(request.SelectedAddressId);
            if (parsedAddress != null)
            {
                var address = await _responsibleEntityRepository.GetCompanyAddress(applicationId);
                if (address is not null)
                {
                    await UpdateResponsibleEntityCompanyAddress(applicationId, parsedAddress);
                    return Unit.Value;
                }

                await InsertResponsibleEntityCompanyAddress(applicationId, parsedAddress);
            }
            
            return Unit.Value;
        }

        private async Task UpdateResponsibleEntityCompanyAddress(Guid applicationId, 
                                                                 ParsedAddress parsedAddress)
        {
            await _connection.ExecuteAsync("UpdateResponsibleEntityCompanyAddress",
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
                    parsedAddress.BuildingType,
                    CountryId = (int?)null
                });
        }

        private async Task InsertResponsibleEntityCompanyAddress(Guid applicationId,
                                                                 ParsedAddress parsedAddress)
        {
            var addressId = Guid.NewGuid();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _connection.ExecuteAsync("InsertResponsibleEntityCompanyAddress",
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
                        parsedAddress.BuildingType,
                        CountryId = (int?)null
                    });

                await _connection.ExecuteAsync("UpdateResponsibleEntityCompanyAddressId", new { applicationId, addressId });

                scope.Complete();
            }
        }
    }

    public class SetResponsibleEntityCompanyAddressRequest : IRequest
    {
        public string SelectedAddressId { get; set; }
    }
}