using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class SetResponsibileEntityOrganisationDetailsHandler : IRequestHandler<SetResponsibleEntityOrganisationDetailsRequest>
    {
        private readonly IDbConnectionWrapper _dbConnection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetResponsibileEntityOrganisationDetailsHandler(IDbConnectionWrapper dbConnection, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnection = dbConnection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<Unit> Handle(SetResponsibleEntityOrganisationDetailsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await _dbConnection.ExecuteAsync("SetResponsibleEntityOrganisationDetails", new { applicationId, request.CompanyName, request.RegistrationNumber });

            return Unit.Value;
        }
    }

    public class SetResponsibleEntityOrganisationDetailsRequest: IRequest
    {
        public string CompanyName { get; set; }
        public string RegistrationNumber { get; set; }
    }
}
