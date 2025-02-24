using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class GetFreeholderAddressHandler : IRequestHandler<GetFreeholderAddressRequest, GetFreeholderAddressResponse>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IResponsibleEntityRepository _responsibleEntityRepository;

        public GetFreeholderAddressHandler(IDbConnectionWrapper connection, 
                                           IApplicationDataProvider applicationDataProvider,
                                           IResponsibleEntityRepository responsibleEntityRepository)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
            _responsibleEntityRepository = responsibleEntityRepository;
        }

        public async Task<GetFreeholderAddressResponse> Handle(GetFreeholderAddressRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await GetFreeholderAddress(applicationId);

            return response;
        }

        private async Task<GetFreeholderAddressResponse> GetFreeholderAddress(Guid applicationId)
        {
            var address = await _responsibleEntityRepository.GetFreeholderAddress(applicationId);        
            if (address is null)
            {
                return new GetFreeholderAddressResponse();
            }

            var response = new GetFreeholderAddressResponse
            {
                NameNumber = address.NameNumber,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                County = address.County,
                Postcode = address.Postcode
            };

            return response;
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
