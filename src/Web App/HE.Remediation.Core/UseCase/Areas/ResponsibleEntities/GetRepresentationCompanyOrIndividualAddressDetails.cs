using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class GetRepresentationCompanyOrIndividualAddressDetailsHandler : IRequestHandler<GetRepresentationCompanyOrIndividualAddressDetailsRequest, GetRepresentationCompanyOrIndividualAddressDetailsResponse>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetRepresentationCompanyOrIndividualAddressDetailsHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetRepresentationCompanyOrIndividualAddressDetailsResponse> Handle(GetRepresentationCompanyOrIndividualAddressDetailsRequest request, CancellationToken cancellationToken)
        {
            var result = await _connection.QuerySingleOrDefaultAsync<GetRepresentationCompanyOrIndividualAddressDetailsResponse>("GetRepresentationCompanyOrIndividualAddress",
                new
                {
                    ApplicationId = _applicationDataProvider.GetApplicationId()
                });

            return result ?? new GetRepresentationCompanyOrIndividualAddressDetailsResponse();
        }
    }

    public class GetRepresentationCompanyOrIndividualAddressDetailsRequest : IRequest<GetRepresentationCompanyOrIndividualAddressDetailsResponse>
    {
        private GetRepresentationCompanyOrIndividualAddressDetailsRequest()
        {
        }

        public static readonly GetRepresentationCompanyOrIndividualAddressDetailsRequest Request = new();
    }

    public class GetRepresentationCompanyOrIndividualAddressDetailsResponse
    {
        public EResponsibleEntityType ResponsibleEntityType { get; set; }
        public string NameNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
    }
}
