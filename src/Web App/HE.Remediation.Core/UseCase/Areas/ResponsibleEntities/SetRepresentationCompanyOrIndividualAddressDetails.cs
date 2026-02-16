using Dapper;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Location.PostCode;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class SetRepresentationCompanyOrIndividualAddressDetailsHandler : IRequestHandler<SetRepresentationCompanyOrIndividualAddressDetailsRequest>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetRepresentationCompanyOrIndividualAddressDetailsHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<Unit> Handle(SetRepresentationCompanyOrIndividualAddressDetailsRequest request, CancellationToken cancellationToken)
        {
            ParsedAddress parsedAddress = PostCodeUtility.ParseAddressJson(request.SelectedAddressId);
            if (parsedAddress != null)
            {
                var applicationId = _applicationDataProvider.GetApplicationId();
                await _connection.ExecuteAsync("SetRepresentationCompanyOrIndividualAddress",  new
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
            return Unit.Value;
        }
    }

    public class SetRepresentationCompanyOrIndividualAddressDetailsRequest : IRequest
    {
        public string SelectedAddressId { get; set; }
    }
}