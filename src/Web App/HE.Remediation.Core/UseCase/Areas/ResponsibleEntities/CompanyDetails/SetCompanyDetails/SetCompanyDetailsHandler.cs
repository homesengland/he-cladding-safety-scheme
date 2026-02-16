using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.CompanyDetails.SetCompanyDetails
{
    public class SetCompanyDetailsHandler : IRequestHandler<SetCompanyDetailsRequest>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetCompanyDetailsHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<Unit> Handle(SetCompanyDetailsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await UpdateCompanyDetails(applicationId, request);

            return Unit.Value;
        }

        private async Task UpdateCompanyDetails(Guid applicationId, SetCompanyDetailsRequest request)
        {
            await _connection.ExecuteAsync("UpdateResponsibleEntityCompanyDetails", new { applicationId, request.CompanyName, request.CompanyRegistrationNumber });
        }
    }
}
