using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class GetFreeholderCompanyDetailsHandler : IRequestHandler<GetFreeholderCompanyDetailsRequest, GetFreeholderCompanyDetailsResponse>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetFreeholderCompanyDetailsHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<GetFreeholderCompanyDetailsResponse> Handle(GetFreeholderCompanyDetailsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await GetFreeholderCompanyDetails(applicationId);

            return response;
        }

        private async ValueTask<GetFreeholderCompanyDetailsResponse> GetFreeholderCompanyDetails(Guid applicationId)
        {
            var response = await _connection.QuerySingleOrDefaultAsync<GetFreeholderCompanyDetailsResponse>("GetFreeholderCompanyDetails", new { applicationId });

            return response ?? new GetFreeholderCompanyDetailsResponse();
        }
    }

    public class GetFreeholderCompanyDetailsRequest : IRequest<GetFreeholderCompanyDetailsResponse>
    {
        private GetFreeholderCompanyDetailsRequest() { }

        public static readonly GetFreeholderCompanyDetailsRequest Request = new();
    }

    public class GetFreeholderCompanyDetailsResponse
    {
        public string CompanyName { get; set; }
        public string CompanyRegistrationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
    }
}
