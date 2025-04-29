using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class SetResponsibileEntityOrganisationDetailsHandler : IRequestHandler<SetResponsibleEntityOrganisationDetailsRequest, SetResponsibleEntityOrganisationDetailsResponse>
    {
        private readonly IDbConnectionWrapper _dbConnection;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IResponsibleEntityRepository _responsibleEntityRepository;

        public SetResponsibileEntityOrganisationDetailsHandler(IDbConnectionWrapper dbConnection, IApplicationDataProvider applicationDataProvider, IResponsibleEntityRepository responsibleEntityRepository)
        {
            _dbConnection = dbConnection;
            _applicationDataProvider = applicationDataProvider;
            _responsibleEntityRepository = responsibleEntityRepository;
        }

        public async Task<SetResponsibleEntityOrganisationDetailsResponse> Handle(SetResponsibleEntityOrganisationDetailsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await _dbConnection.ExecuteAsync("SetResponsibleEntityOrganisationDetails", new { applicationId, request.CompanyName, request.RegistrationNumber });

            var isUkRegistered = await _responsibleEntityRepository.GetResponsibleEntityUkRegistered(applicationId);
            var response = new SetResponsibleEntityOrganisationDetailsResponse
            {
                IsUkRegistered = isUkRegistered
            };
            return response;
        }
    }

    public class SetResponsibleEntityOrganisationDetailsRequest: IRequest<SetResponsibleEntityOrganisationDetailsResponse>
    {
        public string CompanyName { get; set; }
        public string RegistrationNumber { get; set; }
    }

    public class SetResponsibleEntityOrganisationDetailsResponse
    {
        public bool? IsUkRegistered { get; set; }
    }
}
