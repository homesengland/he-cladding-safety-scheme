using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.CompanyDetails.GetCompanyDetails
{
    public class GetCompanyDetailsHandler : IRequestHandler<GetCompanyDetailsRequest, GetCompanyDetailsResponse>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetCompanyDetailsHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<GetCompanyDetailsResponse> Handle(GetCompanyDetailsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            return await GetCompanyDetails(applicationId);
        }

        private async ValueTask<GetCompanyDetailsResponse> GetCompanyDetails(Guid applicationId)
        {
            var response = await _connection.QuerySingleOrDefaultAsync<GetCompanyDetailsResponse>("GetResponsibleEntityCompanyDetails", new { applicationId });

            return response ?? new GetCompanyDetailsResponse();
        }
    }
}
