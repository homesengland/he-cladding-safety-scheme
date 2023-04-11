using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.CompanyAddress.GetCompanyAddress
{
    public class GetCompanyAddressHandler : IRequestHandler<GetCompanyAddressRequest, GetCompanyAddressResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetCompanyAddressHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetCompanyAddressResponse> Handle(GetCompanyAddressRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            return await GetCompanyAddress(applicationId);
        }

        private async Task<GetCompanyAddressResponse> GetCompanyAddress(Guid applicationId)
        {
            var response = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetCompanyAddressResponse>("GetResponsibleEntityCompanyAddress", new { applicationId });

            return response ?? new GetCompanyAddressResponse();
        }
    }
}
