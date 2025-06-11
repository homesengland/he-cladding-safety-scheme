using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingPartOfDevelopment.GetBuildingPartOfDevelopment;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetLeaseHolderEvidence;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Leaseholder.GetResponsibleForCommunication
{
    public class GetResponsibleForCommunicationHandler : IRequestHandler<GetResponsibleForCommunicationRequest, GetResponsibleForCommunicationResponse>
    {
        private readonly IDbConnectionWrapper _dbConnection;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly ILeaseHolderRepository _leaseHolderRepository;

        public GetResponsibleForCommunicationHandler(IDbConnectionWrapper dbConnection, IApplicationDataProvider applicationDataProvider,
            ILeaseHolderRepository leaseHolderRepository)
        {
            _dbConnection = dbConnection;
            _applicationDataProvider = applicationDataProvider;
            _leaseHolderRepository = leaseHolderRepository;
        }

        public async Task<GetResponsibleForCommunicationResponse> Handle(GetResponsibleForCommunicationRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var leaseHolderEngagementId = await _leaseHolderRepository.GetLeaseHolderEngagementIdForApplication(applicationId);

            var response = await _leaseHolderRepository.GetLeaseHolderResponsibleForCommunication(leaseHolderEngagementId);

            return response;

        }
    }
}
