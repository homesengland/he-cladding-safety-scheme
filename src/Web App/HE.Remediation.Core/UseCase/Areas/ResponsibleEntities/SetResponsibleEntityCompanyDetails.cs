using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class SetResponsibleEntityCompanyDetailsHandler : IRequestHandler<SetResponsibleEntityCompanyDetailsRequest>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetResponsibleEntityCompanyDetailsHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<Unit> Handle(SetResponsibleEntityCompanyDetailsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await UpdateResponsibleEntityCompanyDetails(applicationId, request);

            return Unit.Value;
        }

        private async Task UpdateResponsibleEntityCompanyDetails(Guid applicationId, SetResponsibleEntityCompanyDetailsRequest request)
        {
            await _connection.ExecuteAsync("UpdateResponsibleEntityCompanyDetails", new { applicationId, request.CompanyName, request.CompanyRegistrationNumber });
        }
    }

    public class SetResponsibleEntityCompanyDetailsRequest : IRequest
    {
        public string CompanyName { get; set; }
        public string CompanyRegistrationNumber { get; set; }
    }
}
