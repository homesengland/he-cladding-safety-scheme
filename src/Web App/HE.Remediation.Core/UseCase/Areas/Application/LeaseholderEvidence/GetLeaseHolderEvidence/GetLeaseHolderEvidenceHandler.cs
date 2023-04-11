using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.LeaseHolderEvidence.GetLeaseHolderEvidence
{
    public class GetLeaseHolderEvidenceHandler : IRequestHandler<GetLeaseHolderEvidenceRequest, IReadOnlyCollection<GetLeaseHolderEvidenceResponse>>
    {
        private readonly IDbConnectionWrapper _dbConnection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetLeaseHolderEvidenceHandler(IDbConnectionWrapper dbConnection, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnection = dbConnection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<IReadOnlyCollection<GetLeaseHolderEvidenceResponse>> Handle(GetLeaseHolderEvidenceRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var leaseHolderEvidence = await _dbConnection.QueryAsync<GetLeaseHolderEvidenceResponse>("GetLeaseHolderEngagementFilesForApplication", new { applicationId });

            return leaseHolderEvidence;

        }
    }
}
