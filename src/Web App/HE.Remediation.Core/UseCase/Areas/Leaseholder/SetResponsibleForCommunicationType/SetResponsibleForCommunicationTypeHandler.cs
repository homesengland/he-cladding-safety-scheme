using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using MediatR;


namespace HE.Remediation.Core.UseCase.Areas.Leaseholder.SetResponsibleForCommunicationType
{
    public class SetResponsibleForCommunicationTypeHandler : IRequestHandler<SetResponsibleForCommunicationTypeRequest, Unit>
    {
        private readonly IDbConnectionWrapper _dbConnection;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly ILeaseHolderRepository _leaseHolderRepository;

        public SetResponsibleForCommunicationTypeHandler(
            IDbConnectionWrapper dbConnection,
            IApplicationDataProvider applicationIdProvider,
            ILeaseHolderRepository leaseHolderRepository)
        {
            _applicationDataProvider = applicationIdProvider;
            _dbConnection = dbConnection;
            _leaseHolderRepository = leaseHolderRepository;
        }

        public async Task<Unit> Handle(SetResponsibleForCommunicationTypeRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var leaseHolderEngagementId = await _leaseHolderRepository.GetLeaseHolderEngagementIdForApplication(applicationId);

            await UpdateLeaseHolderResponsibleForCommunicationType(request, leaseHolderEngagementId);

            return Unit.Value;
        }

        private async Task UpdateLeaseHolderResponsibleForCommunicationType(SetResponsibleForCommunicationTypeRequest request, Guid leaseHolderEngagementId)
        {
            var parameters = new UpdateLeaseHolderResponsibleForCommunicationTypeParameters()
            {
                LeaseHolderEngagementId = leaseHolderEngagementId,
                ResponsibleForCommunicationTypeId = request.ResponsibleForCommunicationTypeId,
                RepresentationOtherText = request.RepresentationOtherText

            };

            await _leaseHolderRepository.UpdateLeaseHolderResponsibleForCommunicationType(parameters);
        }
    }
}
