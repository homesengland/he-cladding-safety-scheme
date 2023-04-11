using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class GetResponsibleEntityCompanyAddressHandler : IRequestHandler<GetResponsibleEntityCompanyAddressRequest, GetResponsibleEntityCompanyAddressResponse>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetResponsibleEntityCompanyAddressHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetResponsibleEntityCompanyAddressResponse> Handle(GetResponsibleEntityCompanyAddressRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await GetResponsibleEntityCompanyAddress(applicationId);

            return response;
        }

        private async Task<GetResponsibleEntityCompanyAddressResponse> GetResponsibleEntityCompanyAddress(Guid applicationId)
        {
            var response = await _connection.QuerySingleOrDefaultAsync<GetResponsibleEntityCompanyAddressResponse>("GetResponsibleEntityCompanyAddress", new { applicationId });

            return response ?? new GetResponsibleEntityCompanyAddressResponse();
        }
    }

    public class GetResponsibleEntityCompanyAddressRequest : IRequest<GetResponsibleEntityCompanyAddressResponse>
    {
        private GetResponsibleEntityCompanyAddressRequest() { }

        public static readonly GetResponsibleEntityCompanyAddressRequest Request = new();
    }

    public class GetResponsibleEntityCompanyAddressResponse
    {
        public string NameNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
    }
}
