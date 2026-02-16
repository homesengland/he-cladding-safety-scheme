using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

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

        public async ValueTask<GetResponsibleEntityCompanyDetailsResponse> Handle(GetResponsibleEntityCompanyDetailsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            return await GetResponsibleEntityCompanyDetails(applicationId);
        }

        private async ValueTask<GetResponsibleEntityCompanyDetailsResponse> GetResponsibleEntityCompanyDetails(Guid applicationId)
        {
            var response = await _connection.QuerySingleOrDefaultAsync<GetResponsibleEntityCompanyDetailsResponse>("GetResponsibleEntityCompanyDetails", new { applicationId });

            var applicationScheme = _applicationDataProvider.GetApplicationScheme();

            response ??= new GetResponsibleEntityCompanyDetailsResponse();

            response.ApplicationScheme = applicationScheme;

            return response;
        }
    }

    public class GetResponsibleEntityCompanyDetailsResponse
    {
        public EApplicationScheme ApplicationScheme { get; set; }
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
