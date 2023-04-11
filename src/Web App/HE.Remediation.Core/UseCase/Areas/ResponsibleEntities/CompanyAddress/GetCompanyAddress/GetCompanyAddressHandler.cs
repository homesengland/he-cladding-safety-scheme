using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.CompanyAddress.GetCompanyAddress
{
    public class GetCompanyAddressHandler : IRequestHandler<GetCompanyAddressRequest, GetCompanyAddressResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IResponsibleEntityRepository _responsibleEntityRepository;

        public GetCompanyAddressHandler(IDbConnectionWrapper dbConnectionWrapper, 
                                        IApplicationDataProvider applicationDataProvider,
                                        IResponsibleEntityRepository responsibleEntityRepository)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
            _responsibleEntityRepository = responsibleEntityRepository;
        }

        public async Task<GetCompanyAddressResponse> Handle(GetCompanyAddressRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            return await GetCompanyAddress(applicationId);
        }

        private async Task<GetCompanyAddressResponse> GetCompanyAddress(Guid applicationId)
        {
            var address = await _responsibleEntityRepository.GetCompanyAddress(applicationId);
            
            return new GetCompanyAddressResponse
            {
                NameNumber = address?.NameNumber,
                AddressLine1 = address?.AddressLine1,
                AddressLine2 = address?.AddressLine2,
                City = address?.City,
                County = address?.County,
                Postcode = address?.Postcode
            };
        }
    }
}
