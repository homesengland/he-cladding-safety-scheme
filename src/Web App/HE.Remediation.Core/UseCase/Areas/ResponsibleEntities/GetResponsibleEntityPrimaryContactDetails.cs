using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class GetResponsibleEntityPrimaryContactDetailsHandler : IRequestHandler<GetResponsibleEntityPrimaryContactDetailsRequest, GetResponsibleEntityPrimaryContactDetailsResponse>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetResponsibleEntityPrimaryContactDetailsHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetResponsibleEntityPrimaryContactDetailsResponse> Handle(GetResponsibleEntityPrimaryContactDetailsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await GetResponsibleEntityPrimaryContactDetails(applicationId);

            return response;
        }

        private async Task<GetResponsibleEntityPrimaryContactDetailsResponse> GetResponsibleEntityPrimaryContactDetails(Guid applicationId)
        {
            var response = await _connection.QuerySingleOrDefaultAsync<GetResponsibleEntityPrimaryContactDetailsResponse>("GetResponsibleEntityPrimaryContactDetails", new { applicationId });

            return response ?? new GetResponsibleEntityPrimaryContactDetailsResponse();
        }
    }

    public class GetResponsibleEntityPrimaryContactDetailsRequest : IRequest<GetResponsibleEntityPrimaryContactDetailsResponse>
    {
        private GetResponsibleEntityPrimaryContactDetailsRequest() { }

        public static readonly GetResponsibleEntityPrimaryContactDetailsRequest Request = new();
    }

    public class GetResponsibleEntityPrimaryContactDetailsResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
        public bool IsUkBased { get; set; }
        public EApplicationResponsibleEntityOrganisationType? OrganisationType { get; set; }
        public EApplicationResponsibleEntityOrganisationSubType? OrganisationSubType { get; set; }
    }
}
