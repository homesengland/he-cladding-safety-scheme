using HE.Remediation.Core.Data;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.SetLeaseholderEvidence;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Leaseholder.SetResponsibleForCommunication
{
    public class SetResponsibleForCommunicationHandler : IRequestHandler<SetResponsibleForCommunicationRequest, Unit>
    {
        private readonly IDbConnectionWrapper _dbConnection;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly ILeaseHolderRepository _leaseHolderRepository;

        public SetResponsibleForCommunicationHandler(
            IDbConnectionWrapper dbConnection,
            IApplicationDataProvider applicationIdProvider,
            ILeaseHolderRepository leaseHolderRepository)
        {
            _applicationDataProvider = applicationIdProvider;
            _dbConnection = dbConnection;
            _leaseHolderRepository = leaseHolderRepository;
        }

        public async Task<Unit> Handle(SetResponsibleForCommunicationRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var leaseHolderEngagementId =  await _leaseHolderRepository.GetLeaseHolderEngagementIdForApplication(applicationId);

            await UpdateResponsibleForCommunication(request, leaseHolderEngagementId);

            return Unit.Value;
        }

        private async Task UpdateResponsibleForCommunication(SetResponsibleForCommunicationRequest request, Guid leaseHolderEngagementId)
        {
            var parameters = new UpdateLeaseHolderResponsibleForCommunicationParameters()
            {
                LeaseHolderEngagementId = leaseHolderEngagementId,
                ResponsibleForCommunication = request.ResponsibleForCommunication
            };

             await _leaseHolderRepository.UpdateLeaseHolderResponsibleForCommunication(parameters);
        }
    }
}
