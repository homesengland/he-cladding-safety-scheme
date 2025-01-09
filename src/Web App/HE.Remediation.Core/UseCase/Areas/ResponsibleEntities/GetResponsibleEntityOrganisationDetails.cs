using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class GetResponsibleEntityOrganisationDetailsHandler : IRequestHandler<GetResponsibleEntityOrganisationDetailsRequest, GetResponsibleEntityOrganisationDetailsResponse>
    {
        private readonly IDbConnectionWrapper _dbConnection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetResponsibleEntityOrganisationDetailsHandler(IDbConnectionWrapper dbConnection, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnection = dbConnection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetResponsibleEntityOrganisationDetailsResponse> Handle(GetResponsibleEntityOrganisationDetailsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await _dbConnection.QuerySingleOrDefaultAsync<GetResponsibleEntityOrganisationDetailsResponse>("GetResponsibleEntityOrganisationDetails", new { applicationId });

            return response ?? new GetResponsibleEntityOrganisationDetailsResponse();
        }
    }

    public class GetResponsibleEntityOrganisationDetailsResponse
    {
        public string CompanyName { get; set; }
        public string RegistrationNumber { get; set; }
    }

    public class GetResponsibleEntityOrganisationDetailsRequest : IRequest<GetResponsibleEntityOrganisationDetailsResponse>
    {
        private GetResponsibleEntityOrganisationDetailsRequest() { }

        public static readonly GetResponsibleEntityOrganisationDetailsRequest Request = new();
    }
}
