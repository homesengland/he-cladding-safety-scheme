using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetResponsibleForCommunication;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Leaseholder.GetCommunicationPartyDetails
{
    public class GetCommunicationPartyDetailsHandler : IRequestHandler<GetCommunicationPartyDetailsRequest, GetCommunicationPartyDetailsResponse>
    {
        private readonly IDbConnectionWrapper _dbConnection;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly ILeaseHolderRepository _leaseHolderRepository;

        public GetCommunicationPartyDetailsHandler(IDbConnectionWrapper dbConnection, IApplicationDataProvider applicationDataProvider,
            ILeaseHolderRepository leaseHolderRepository)
        {
            _dbConnection = dbConnection;
            _applicationDataProvider = applicationDataProvider;
            _leaseHolderRepository = leaseHolderRepository;
        }

        public async ValueTask<GetCommunicationPartyDetailsResponse> Handle(GetCommunicationPartyDetailsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var leaseHolderEngagementId = await _leaseHolderRepository.GetLeaseHolderEngagementIdForApplication(applicationId);

            var response = await _leaseHolderRepository.GetLeaseHolderCommunicationPartyDetails(leaseHolderEngagementId);

            return response?? new GetCommunicationPartyDetailsResponse();
        }
    }
}
