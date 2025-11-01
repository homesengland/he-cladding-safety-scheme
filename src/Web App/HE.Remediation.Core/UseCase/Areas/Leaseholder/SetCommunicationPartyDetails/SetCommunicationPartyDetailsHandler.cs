using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.SetResponsibleForCommunication;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Leaseholder.SetCommunicationPartyDetails
{
    public class SetCommunicationPartyDetailsHandler : IRequestHandler<SetCommunicationPartyDetailsRequest, Unit>
    {
        private readonly IDbConnectionWrapper _dbConnection;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly ILeaseHolderRepository _leaseHolderRepository;

        public SetCommunicationPartyDetailsHandler(
            IDbConnectionWrapper dbConnection,
            IApplicationDataProvider applicationIdProvider,
            ILeaseHolderRepository leaseHolderRepository)
        {
            _applicationDataProvider = applicationIdProvider;
            _dbConnection = dbConnection;
            _leaseHolderRepository = leaseHolderRepository;
        }

        public async Task<Unit> Handle(SetCommunicationPartyDetailsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var leaseHolderEngagementId = await _leaseHolderRepository.GetLeaseHolderEngagementIdForApplication(applicationId);

            await UpdateCommunicationPartyDetails(request, leaseHolderEngagementId);

            return Unit.Value;
        }

        private async Task UpdateCommunicationPartyDetails(SetCommunicationPartyDetailsRequest request, Guid leaseHolderEngagementId)
        {
            var parameters = new UpdateLeaseHolderCommunicationPartyDetailsParameters()
            {
                ContactName = request.ContactName,
                CompanyName = request.CompanyName,
                CompanyRegistrationNumber = request.CompanyRegistrationNumber,
                EmailAddress = request.EmailAddress,
                ContactNumber = request.ContactNumber,
                ApplicationLeaseHolderEngagementId = leaseHolderEngagementId
            };

            await _leaseHolderRepository.UpdateLeaseHolderCommunicationPartyDetails(parameters);
        }
    }
}
