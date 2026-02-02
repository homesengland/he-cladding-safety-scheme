using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class GetRepresentationCompanyOrIndividualDetailsHandler : IRequestHandler<GetRepresentationCompanyOrIndividualDetailsRequest, GetRepresentationCompanyOrIndividualDetailsResponse>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetRepresentationCompanyOrIndividualDetailsHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<GetRepresentationCompanyOrIndividualDetailsResponse> Handle(GetRepresentationCompanyOrIndividualDetailsRequest request, CancellationToken cancellationToken)
        {
            var result = await _connection.QuerySingleOrDefaultAsync<GetRepresentationCompanyOrIndividualDetailsResponse>(
                "GetRepresentationCompanyOrIndividualDetails",
                new
                {
                    ApplicationId = _applicationDataProvider.GetApplicationId()
                });

            return result;
        }
    }

    public class GetRepresentationCompanyOrIndividualDetailsRequest : IRequest<GetRepresentationCompanyOrIndividualDetailsResponse>
    {
        private GetRepresentationCompanyOrIndividualDetailsRequest()
        {
        }

        public static readonly GetRepresentationCompanyOrIndividualDetailsRequest Request = new();
    }

    public class GetRepresentationCompanyOrIndividualDetailsResponse
    {
        public EResponsibleEntityType? ResponsibleEntityType { get; set; }
        public string CompanyName { get; set; }
        public string CompanyRegistration { get; set; }
        public string NameNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
    }
}
