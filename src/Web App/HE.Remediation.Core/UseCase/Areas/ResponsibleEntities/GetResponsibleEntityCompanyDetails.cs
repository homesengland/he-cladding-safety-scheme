using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class GetResponsibleEntityCompanyDetailsHandler : IRequestHandler<GetResponsibleEntityCompanyDetailsRequest, GetResponsibleEntityCompanyDetailsResponse>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetResponsibleEntityCompanyDetailsHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetResponsibleEntityCompanyDetailsResponse> Handle(GetResponsibleEntityCompanyDetailsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            return await GetResponsibleEntityCompanyDetails(applicationId);
        }

        private async Task<GetResponsibleEntityCompanyDetailsResponse> GetResponsibleEntityCompanyDetails(Guid applicationId)
        {
            var response = await _connection.QuerySingleOrDefaultAsync<GetResponsibleEntityCompanyDetailsResponse>("GetResponsibleEntityCompanyDetails", new { applicationId });

            return response ?? new GetResponsibleEntityCompanyDetailsResponse();
        }
    }

    public class GetResponsibleEntityCompanyDetailsResponse
    {
        public string CompanyName { get; set; }
        public string CompanyRegistrationNumber { get; set; }
        public bool IsUkBased { get; set; }
    }

    public class GetResponsibleEntityCompanyDetailsRequest : IRequest<GetResponsibleEntityCompanyDetailsResponse>
    {
        private GetResponsibleEntityCompanyDetailsRequest() { }

        public static readonly GetResponsibleEntityCompanyDetailsRequest Request = new();
    }
}
