using Dapper;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Location.PostCode;
using MediatR;

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

        public async Task<Unit> Handle(SetRepresentationCompanyOrIndividualAddressDetailsRequest request, CancellationToken cancellationToken)
        {
            ParsedAddress parsedAddress = PostCodeUtility.ParseAddressJson(request.SelectedAddressId);
            if (parsedAddress != null)
            {
                var paramsMap = new Dictionary<string, object>()
                {
                    ["NameNumber"] = parsedAddress.NameNumber,
                    ["AddressLine1"] = parsedAddress.AddressLine1,
                    ["AddressLine2"] = string.Empty,
                    ["City"] = parsedAddress.City,
                    ["County"] = string.Empty,
                    ["Postcode"] = parsedAddress.Postcode
                };
                
                var parameters = new DynamicParameters(paramsMap);                
                parameters.Add("@ApplicationId", _applicationDataProvider.GetApplicationId());

                await _connection.ExecuteAsync("SetRepresentationCompanyOrIndividualAddress", parameters);
            }
            return Unit.Value;
        }
    }

    public class SetRepresentationCompanyOrIndividualAddressDetailsRequest : IRequest
    {
        public string SelectedAddressId { get; set; }
    }
}
