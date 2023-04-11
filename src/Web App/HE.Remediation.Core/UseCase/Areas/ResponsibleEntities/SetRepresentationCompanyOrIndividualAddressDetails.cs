using Dapper;
using HE.Remediation.Core.Interface;
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
            var parameters = new DynamicParameters(request);
            parameters.Add("@ApplicationId", _applicationDataProvider.GetApplicationId());

            await _connection.ExecuteAsync("SetRepresentationCompanyOrIndividualAddress", parameters);
            return Unit.Value;
        }
    }

    public class SetRepresentationCompanyOrIndividualAddressDetailsRequest : IRequest
    {
        public string NameNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
    }
}
