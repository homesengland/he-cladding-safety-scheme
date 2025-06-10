using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetResponsibleForCommunication;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Leaseholder.GetResponsibleForCommunicationType
{
    public class GetResponsibleForCommunicationTypeHandler : IRequestHandler<GetResponsibleForCommunicationTypeRequest, GetResponsibleForCommunicationTypeResponse>
    {
        private readonly IDbConnectionWrapper _dbConnection;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly ILeaseHolderRepository _leaseHolderRepository;

        public GetResponsibleForCommunicationTypeHandler(IDbConnectionWrapper dbConnection, IApplicationDataProvider applicationDataProvider,
            ILeaseHolderRepository leaseHolderRepository)
        {
            _dbConnection = dbConnection;
            _applicationDataProvider = applicationDataProvider;
            _leaseHolderRepository = leaseHolderRepository;
        }

        public async Task<GetResponsibleForCommunicationTypeResponse> Handle(GetResponsibleForCommunicationTypeRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var leaseHolderEngagementId = await _leaseHolderRepository.GetLeaseHolderEngagementIdForApplication(applicationId);

            var response = await _leaseHolderRepository.GetLeaseHolderResponsibleForCommunicationType(leaseHolderEngagementId);

            return response;

        }
    }
}
