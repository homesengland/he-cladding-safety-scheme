using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class GetFreeholderIndividualDetailsHandler : IRequestHandler<GetFreeholderIndividualDetailsRequest, GetFreeholderIndividualDetailsResponse>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetFreeholderIndividualDetailsHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<GetFreeholderIndividualDetailsResponse> Handle(GetFreeholderIndividualDetailsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await GetFreeholderIndividualDetails(applicationId);

            return response;
        }

        private async ValueTask<GetFreeholderIndividualDetailsResponse> GetFreeholderIndividualDetails(Guid applicationId)
        {
            var response = await _connection.QuerySingleOrDefaultAsync<GetFreeholderIndividualDetailsResponse>("GetFreeholderIndividualDetails", new { applicationId });

            return response ?? new GetFreeholderIndividualDetailsResponse();
        }
    }

    public class GetFreeholderIndividualDetailsRequest : IRequest<GetFreeholderIndividualDetailsResponse>
    {
        private GetFreeholderIndividualDetailsRequest() { }

        public static readonly GetFreeholderIndividualDetailsRequest Request = new();
    }

    public class GetFreeholderIndividualDetailsResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
    }
}
