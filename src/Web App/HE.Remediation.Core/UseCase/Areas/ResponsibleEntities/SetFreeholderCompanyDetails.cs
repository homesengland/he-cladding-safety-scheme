using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class SetFreeholderCompanyDetailsHandler : IRequestHandler<SetFreeholderCompanyDetailsRequest>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetFreeholderCompanyDetailsHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<Unit> Handle(SetFreeholderCompanyDetailsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await UpdateFreeholderCompanyDetails(applicationId, request);

            return Unit.Value;
        }

        private async Task UpdateFreeholderCompanyDetails(Guid applicationId, SetFreeholderCompanyDetailsRequest request)
        {
            await _connection.ExecuteAsync("UpdateFreeholderCompanyDetails",
                new
                {
                    applicationId,
                    request.CompanyName,
                    request.CompanyRegistrationNumber,
                    request.FirstName,
                    request.LastName,
                    request.EmailAddress,
                    request.ContactNumber
                });
        }
    }

    public class SetFreeholderCompanyDetailsRequest : IRequest
    {
        public string CompanyName { get; set; }
        public string CompanyRegistrationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
    }
}
