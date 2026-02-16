using Dapper;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class SetRepresentationCompanyOrIndividualDetailsHandler : IRequestHandler<SetRepresentationCompanyOrIndividualDetailsRequest>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetRepresentationCompanyOrIndividualDetailsHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<Unit> Handle(SetRepresentationCompanyOrIndividualDetailsRequest request, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters(request);
            parameters.Add("@ApplicationId", _applicationDataProvider.GetApplicationId());

            await _connection.ExecuteAsync("UpdateRepresentationCompanyOrIndividualDetails", parameters);

            return Unit.Value;
        }
    }

    public class SetRepresentationCompanyOrIndividualDetailsRequest : IRequest
    {
        public string CompanyName { get; set; }
        public string CompanyRegistration { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
    }
}
