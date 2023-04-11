using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class GetFreeholderAddressHandler : IRequestHandler<GetFreeholderAddressRequest, GetFreeholderAddressResponse>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetFreeholderAddressHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetFreeholderAddressResponse> Handle(GetFreeholderAddressRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await GetFreeholderAddress(applicationId);

            return response;
        }

        private async Task<GetFreeholderAddressResponse> GetFreeholderAddress(Guid applicationId)
        {
            var response = await _connection.QuerySingleOrDefaultAsync<GetFreeholderAddressResponse>("GetFreeholderAddress", new { applicationId });

            return response ?? new GetFreeholderAddressResponse();
        }
    }

    public class GetFreeholderAddressRequest : IRequest<GetFreeholderAddressResponse>
    {
        private GetFreeholderAddressRequest() { }

        public static readonly GetFreeholderAddressRequest Request = new();
    }

    public class GetFreeholderAddressResponse
    {
        public string NameNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
    }
}
