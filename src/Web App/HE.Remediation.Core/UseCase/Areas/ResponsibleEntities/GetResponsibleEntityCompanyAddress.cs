using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class GetResponsibleEntityCompanyAddressHandler : IRequestHandler<GetResponsibleEntityCompanyAddressRequest, GetResponsibleEntityCompanyAddressResponse>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IResponsibleEntityRepository _responsibleEntityRepository;

        public GetResponsibleEntityCompanyAddressHandler(IDbConnectionWrapper connection, 
                                                         IApplicationDataProvider applicationDataProvider,
                                                         IResponsibleEntityRepository responsibleEntityRepository)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
            _responsibleEntityRepository = responsibleEntityRepository;
        }
        
        public async Task<GetResponsibleEntityCompanyAddressResponse> Handle(GetResponsibleEntityCompanyAddressRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await GetResponsibleEntityCompanyAddress(applicationId);
            var ukRegistered = await _responsibleEntityRepository.GetResponsibleEntityUkRegistered(applicationId);
            
            var countries = await _connection.QueryAsync<Country>("GetCountries");
            response.Countries = countries?.ToList();
            response.UkRegistered = ukRegistered;
            return response;
        }

        private async Task<GetResponsibleEntityCompanyAddressResponse> GetResponsibleEntityCompanyAddress(Guid applicationId)
        {
            var address = await _responsibleEntityRepository.GetCompanyAddress(applicationId);

            return new GetResponsibleEntityCompanyAddressResponse
            {
                NameNumber = address?.NameNumber,
                AddressLine1 = address?.AddressLine1,
                AddressLine2 = address?.AddressLine2,
                City = address?.City,
                County = address?.County,
                Postcode = address?.Postcode,
                CountryId = address?.CountryId
            };
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
        public int? CountryId { get; set; }

        public bool? UkRegistered { get; set; }

        public List<Country> Countries { get; set; }
    }

    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
}
